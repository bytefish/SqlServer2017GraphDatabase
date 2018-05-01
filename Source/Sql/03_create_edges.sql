USE $(dbname) 
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[inCountry]') AND type in (N'U'))
BEGIN

    CREATE TABLE inCountry AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[inState]') AND type in (N'U'))
BEGIN

    CREATE TABLE inState AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[inCity]') AND type in (N'U'))
BEGIN

    CREATE TABLE inCity AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[origin]') AND type in (N'U'))
BEGIN

    CREATE TABLE origin (taxi_time integer, dep_delay integer) AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[destination]') AND type in (N'U'))
BEGIN

    CREATE TABLE destination (taxi_time integer, arr_delay integer) AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[carrier]') AND type in (N'U'))
BEGIN

    CREATE TABLE carrier AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[cancelledBy]') AND type in (N'U'))
BEGIN

    CREATE TABLE cancelledBy AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[delayedBy]') AND type in (N'U'))
BEGIN

    CREATE TABLE delayedBy(time INTEGER) AS EDGE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[aircraft]') AND type in (N'U'))
BEGIN

    CREATE TABLE aircraft AS EDGE;

END
GO
