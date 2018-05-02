USE $(dbname) 
GO

IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='IX_Flight_FlightNumber')
BEGIN
    DROP INDEX [IX_Flight_FlightNumber] ON Flight;
END

CREATE NONCLUSTERED INDEX IX_Flight_FlightNumber
    ON Flight (FlightNumber); 
GO

IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='IX_Country_Name')
BEGIN
    DROP INDEX [IX_Country_Name] ON [Country];
END

CREATE NONCLUSTERED INDEX IX_Country_Name
    ON Country (Name); 
GO

IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='IX_State_Name')
BEGIN
    DROP INDEX [IX_State_Name] ON [State];
END

CREATE NONCLUSTERED INDEX IX_State_Name
    ON State (Name); 
GO

IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='IX_City_Name')
BEGIN
    DROP INDEX [IX_City_Name] ON [City];
END

CREATE NONCLUSTERED INDEX IX_City_Name
    ON City (Name); 
GO

IF EXISTS (SELECT *  FROM sys.indexes  WHERE name='IX_Reason_Code')
BEGIN
    DROP INDEX [IX_Reason_Code] ON [Reason];
END

CREATE NONCLUSTERED INDEX IX_Reason_Code
    ON Reason (Code); 
GO