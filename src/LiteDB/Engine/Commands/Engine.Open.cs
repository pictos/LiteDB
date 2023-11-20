﻿namespace LiteDB.Engine;

public partial class LiteEngine : ILiteEngine
{
    public async Task OpenAsync()
    {
        var lockService = _factory.LockService;
        var diskService = _factory.DiskService;
        var logService = _factory.LogService;
        var allocationMapService = _factory.AllocationMapService;
        var masterService = _factory.MasterService;
        var recoveryService = _factory.RecoveryService;

        if (_factory.State != EngineState.Close) throw ERR("must be closed");

        // clean last database exception
        _factory.Exception = null;

        // must run in exclusive mode
        await lockService.EnterExclusiveAsync();

        if (_factory.State != EngineState.Close) throw ERR("must be closed");

        try
        {
            // open/create data file and returns file header
            (_factory.FileHeader, _factory.Pragmas) = await diskService.InitializeAsync();

            // checks if datafile was finish correctly
            if (_factory.Pragmas.IsDirty)
            {
                _factory.State = EngineState.Recovery;

                // do a database recovery
                await recoveryService.DoRecoveryAsync();

                diskService.WritePragmasAsync(_factory.Pragmas);

                _factory.Pragmas.IsDirty = false;
            }

            // initialize log service based on disk
            logService.Initialize();

            // initialize AM service
            allocationMapService.InitializeAsync();

            // read $master
            masterService.InitializeAsync();

            // update header/state
            _factory.State = EngineState.Open;

            // release exclusive
            lockService.ExitExclusive();

        }
        catch (Exception ex)
        {
            ex.HandleError(_factory);
            throw;
        }
    }
}