# LiteDB

This branch is current development of new version of LiteDB v6.

# Next steps
- SQL Parser
    - pragma (get/set)

- Implement RandomAccess and SafeHandle


# Needs implementation

## Engine
- Implement RandomAccess and SafeHandle
- Return to async calls in managed memory

## Operations
- Update
- Batch
- Rebuild
- Vaccum? (maybe later)

## Query Engine
- Create IQuery and split Query in Query/GroupByQuery
- IntoPipe
- Distinct Pipe
- Virtual collections (IDocumentStore): $master, $file_json, ...

## Services
- CRC32
- ErrorHandling review
- try/catch/deallocate
- Auto-Rebuid, Flag error

## Exception
- Normalize all exception using ERR_xxx

## BsonExpressions
- MakeDocument spread: { ...$ }
- MakeArray spread: [ ...phones ]
- CoalesceExpression:  a ?? 5 
- ANY: array ANY >= 8 // retorna TRUE se algum item de left (um array) satisfizer a operação com right (single::)

## Tokenizer
- Write a single tokenizer
   


## BsonValue
- DateTimeOffset

## LiteDatabase
- BsonMapper
- LiteCollection, LiteQuery, ...
- LiteStorage

## LiteDB.TestSuite

## SharedMode

## Performance
- Test use of `ref` in pipe context on movenext
- Create extenstion methods for linq
    - from _store.GetIndexes().FirstOrDefault(x => x.Expression == predicate.Left);
    - to _store.GetIndexes().FirstOrDefaultByExpression(predicate.Left);


## Performance boost (future)
- Read stream in extend size 64kb
- Async queue writer
- BsonDocumentReader
- BsonDocumentWriter
- Sort using unsafe (without BsonValue)
- [BsonDocumentGenerate] - map POCO class to BsonDocument and vice-versa
- Transaction MultiThread - a single bulk operation can use multiple threads (using `Parallel`)

## Documentation
- Define docs structure using a menu tree navigation and a single template


# Future

## Query Join
- DataStore Alias to support SELECT p._id FROM products p
- JoinPipeEnumerator (DataStore, PathExpression, Inner/Left)
- Link over _id only
```SQL
SELECT c, p
  FROM customers c
 INNER JOIN products p ON c.id_customer (always use products PK to link)
```
- Convert PipeValue in List<(DataStore, PipeValue)> in enumerators
- JoinDataStore - use only _id as key
- JoinPipeEnumerator keeps IndexNode and navigate in "continue mode"

## SubQuery
- Used in FROM (....)
- Used in SELECT (...) AS col
- Used in WHERE _id IN (...)
