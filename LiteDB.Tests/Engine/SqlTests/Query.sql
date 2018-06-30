﻿--
-- SQL Tests
--
-- All command will be run at once, so always use ; after each command
-- If want test command result use "SET @par = [command]" to get result into "par" output parameter

-- Order By With LIMIT/OFFSET
SET @nameAll[] = SELECT name FROM person ORDER BY name;
SET @name100   = SELECT name FROM person  ORDER BY name LIMIT 1 OFFSET 100;


-- Query with GROUP BY and HAVING
SET @groupBy1[] = 
	SELECT { state, count: COUNT($) }
	  FROM person
	 WHERE state IN ["FL", "CA", "MN", "TX", "MS"]
	 GROUP BY state
	HAVING count > 5

