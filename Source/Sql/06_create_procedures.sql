USE $(dbname)
GO

IF OBJECT_ID(N'[InsertAirports]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [InsertAirports]
END
GO 

IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'AirportType')
BEGIN
    DROP TYPE [AirportType]
END

CREATE TYPE [AirportType] AS TABLE (
    [Identifier] [NVARCHAR](255),
    [Abbr] [NVARCHAR](255),
    [Name] [NVARCHAR](255),
    [City] [NVARCHAR](255),
	[StateCode] [NVARCHAR](255),
    [StateName] [NVARCHAR](255),
    [Country] [NVARCHAR](255),
	[CountryIsoCode] [NVARCHAR](255)
);

GO

CREATE PROCEDURE [InsertAirports]
  @Entities [AirportType] ReadOnly
AS
BEGIN
    
    SET NOCOUNT ON;

    -- Insert missing City Nodes:
    INSERT INTO City
    SELECT DISTINCT e.City
    FROM @Entities e 
	WHERE NOT EXISTS (select * from City c where c.Name = e.City)

    -- Insert missing State Nodes:
    INSERT INTO State
    SELECT DISTINCT e.StateCode, e.StateName
    FROM @Entities e 
	WHERE NOT EXISTS (select * from State s where s.Name = e.StateName and s.Code = e.StateCode)

    -- Insert missing Country Nodes:
    INSERT INTO Country
    SELECT DISTINCT e.Country, e.CountryIsoCode
    FROM @Entities e 
	WHERE NOT EXISTS (select * from Country c where c.Name = e.Country)
    
    -- Build the Temporary Staged Table for Inserts:
    DECLARE @TemporaryAirportTable Table(
        [AirportID] [INTEGER],
		[NodeID] [NVARCHAR](1000),
        [Airport] [NVARCHAR](255),
        [Abbr] [NVARCHAR](255),
        [Name] [NVARCHAR](255),
        [City] [NVARCHAR](255),
        [StateCode] [NVARCHAR](255),
		[StateName] [NVARCHAR](255),
        [Country] [NVARCHAR](255),
		[CountryIsoCode] [NVARCHAR](255)
    );
    
    -- Insert into Temporary Table:
    INSERT INTO Airport(Identifier, Abbr, Name, City, StateCode, StateName, Country, CountryIsoCode)
    OUTPUT INSERTED.AirportID, INSERTED.$NODE_ID, INSERTED.Identifier, INSERTED.Abbr, INSERTED.Name, INSERTED.City, INSERTED.StateCode, INSERTED.StateName, INSERTED.Country, INSERTED.CountryIsoCode
    INTO @TemporaryAirportTable
    SELECT * FROM @Entities;
    
    -- Build Relationships:
    INSERT INTO inCity
    SELECT airport.NodeID, (SELECT $NODE_ID FROM City where Name = airport.City)
    FROM @TemporaryAirportTable airport;

    INSERT INTO inState
    SELECT (SELECT $NODE_ID From Airport where airport.AirportID = AirportID), (SELECT $NODE_ID FROM State where Code = airport.StateCode)
    FROM @TemporaryAirportTable airport;

    INSERT INTO inCountry
    SELECT (SELECT $NODE_ID From Airport where airport.AirportID = AirportID), (SELECT $NODE_ID FROM Country where Name = airport.Country)
    FROM @TemporaryAirportTable airport;

    INSERT INTO inCountry
    SELECT (SELECT $NODE_ID FROM City WHERE Name = airport.City), (SELECT $NODE_ID FROM Country where Name = airport.Country) 
    FROM @TemporaryAirportTable airport;
    
END
GO

IF OBJECT_ID(N'[InsertFlights]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [InsertFlights]
END
GO 

IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'FlightType')
BEGIN
    DROP TYPE [FlightType]
END

CREATE TYPE [FlightType] AS TABLE (
    [Year] [NUMERIC](9, 0),
    [Month] [NUMERIC](9, 0),
    [DayOfMonth] [NUMERIC](9, 0),
    [DayOfWeek] [NUMERIC](9, 0),
    [FlightDate] [DATETIME2],
    [UniqueCarrier] [NVARCHAR](255),
    [TailNumber] [NVARCHAR](255),
    [FlightNumber] [NVARCHAR](255),
    [OriginAirport] [NVARCHAR](55),
    [OriginState] [NVARCHAR](55),
    [DestinationAirport] [NVARCHAR](55),
    [DestinationState] [NVARCHAR](55),
    [DepartureDelay] [NUMERIC](9, 0),
    [TaxiOut] [NUMERIC](9, 0),
    [TaxiIn] [NUMERIC](9, 0),
    [ArrivalDelay] [NUMERIC](9, 0),
    [CancellationCode] [NVARCHAR](255),
    [CarrierDelay] [NUMERIC](9, 0),
    [WeatherDelay] [NUMERIC](9, 0),
    [NasDelay] [NUMERIC](9, 0),
    [SecurityDelay] [NUMERIC](9, 0),
    [LateAircraftDelay] [NUMERIC](9, 0)
);

GO

IF OBJECT_ID(N'[InsertFlights]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [InsertFlights]
END
GO 

CREATE PROCEDURE [InsertFlights]
  @Entities [FlightType] ReadOnly
AS
BEGIN
    
    SET NOCOUNT ON;

    -- Temporary Table for Inserts:
    DECLARE @TemporaryFlightTable TABLE(
        [FlightID] [INTEGER],
        [NodeID] [NVARCHAR](1000),
        [Year] [NUMERIC](9, 0),
        [Month] [NUMERIC](9, 0),
        [DayOfMonth] [NUMERIC](9, 0),
        [DayOfWeek] [NUMERIC](9, 0),
        [FlightDate] [DATETIME2],
        [UniqueCarrier] [NVARCHAR](255),
        [TailNumber] [NVARCHAR](255),
        [FlightNumber] [NVARCHAR](255),
        [OriginAirport] [NVARCHAR](55),
        [OriginState] [NVARCHAR](55),
        [DestinationAirport] [NVARCHAR](55),
        [DestinationState] [NVARCHAR](55),
        [DepartureDelay] [NUMERIC](9, 0),
        [TaxiOut] [NUMERIC](9, 0),
        [TaxiIn] [NUMERIC](9, 0),
        [ArrivalDelay] [NUMERIC](9, 0),
        [CancellationCode] [NVARCHAR](255),
        [CarrierDelay] [NUMERIC](9, 0),
        [WeatherDelay] [NUMERIC](9, 0),
        [NasDelay] [NUMERIC](9, 0),
        [SecurityDelay] [NUMERIC](9, 0),
        [LateAircraftDelay] [NUMERIC](9, 0)
    );

    -- Insert into Temporary Table:
    INSERT INTO Flight(Year, Month, DayOfMonth, DayOfWeek, FlightDate, UniqueCarrier, TailNumber, FlightNumber, OriginAirport, OriginState, DestinationAirport, DestinationState, DepartureDelay, TaxiOut, TaxiIn, ArrivalDelay, CancellationCode, CarrierDelay, WeatherDelay, NasDelay, SecurityDelay, LateAircraftDelay)
    OUTPUT INSERTED.FlightID, INSERTED.$NODE_ID, INSERTED.Year, INSERTED.Month, INSERTED.DayOfMonth, INSERTED.DayOfWeek, INSERTED.FlightDate, INSERTED.UniqueCarrier, INSERTED.TailNumber, INSERTED.FlightNumber, INSERTED.OriginAirport, INSERTED.OriginState, INSERTED.DestinationAirport, INSERTED.DestinationState, INSERTED.DepartureDelay, INSERTED.TaxiOut, INSERTED.TaxiIn, INSERTED.ArrivalDelay, INSERTED.CancellationCode, INSERTED.CarrierDelay, INSERTED.WeatherDelay, INSERTED.NasDelay, INSERTED.SecurityDelay, INSERTED.LateAircraftDelay
    INTO @TemporaryFlightTable
    SELECT * FROM @Entities;
    
    -- Insert Origins:
    INSERT INTO origin
    SELECT flight.NodeID, airport.$NODE_ID, flight.TaxiOut, flight.DepartureDelay
    FROM @TemporaryFlightTable flight
        INNER JOIN Airport airport on airport.Identifier = flight.OriginAirport;

    -- Insert Destinations:
    INSERT INTO destination
	SELECT flight.NodeID, airport.$NODE_ID, flight.TaxiIn, flight.ArrivalDelay
    FROM @TemporaryFlightTable flight
    INNER JOIN Airport airport on airport.Identifier = flight.DestinationAirport;

    -- INSERT Delays:
    INSERT INTO delayedBy
    SELECT flight.NodeID, (SELECT $NODE_ID FROM Reason where Code = 'A'), flight.CarrierDelay
    FROM @TemporaryFlightTable flight
    WHERE flight.CarrierDelay > 0;

    INSERT INTO delayedBy
    SELECT flight.NodeID, (SELECT $NODE_ID FROM Reason where Code = 'B'), flight.WeatherDelay
    FROM @TemporaryFlightTable flight
    WHERE flight.WeatherDelay > 0;

    INSERT INTO delayedBy
    SELECT flight.NodeID, (SELECT $NODE_ID FROM Reason where Code = 'C'), flight.NasDelay
    FROM @TemporaryFlightTable flight
    WHERE flight.NasDelay > 0;

    INSERT INTO delayedBy
    SELECT flight.NodeID, (SELECT $NODE_ID FROM Reason where Code = 'D'), flight.SecurityDelay
    FROM @TemporaryFlightTable flight
    WHERE flight.SecurityDelay > 0;

    INSERT INTO delayedBy
    SELECT flight.NodeID, (SELECT $NODE_ID FROM Reason where Code = 'Z'), flight.LateAircraftDelay
    FROM @TemporaryFlightTable flight
    WHERE flight.LateAircraftDelay > 0;
    
    -- Insert Cancelled Flights:
    INSERT INTO cancelledBy
    SELECT flight.NodeID, reason.$NODE_ID
    FROM @TemporaryFlightTable flight
        INNER JOIN Reason reason on flight.CancellationCode = reason.Code;
    
END
GO

IF OBJECT_ID(N'[InsertCarriers]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [InsertCarriers]
END
GO 

IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'CarrierType')
BEGIN
    DROP TYPE [CarrierType]
END

CREATE TYPE [CarrierType] AS TABLE (
	Code [NVARCHAR](255),
	Description [NVARCHAR](255)
);

GO

CREATE PROCEDURE [InsertCarriers]
  @Entities [CarrierType] ReadOnly
AS
BEGIN
    
    SET NOCOUNT ON;

    -- Insert missing City Nodes:
    INSERT INTO Carrier(Code, Description)
    SELECT e.Code, e.Description
    FROM @Entities e 
	WHERE NOT EXISTS (select * from Carrier c where c.Code = e.Code)

END
GO
