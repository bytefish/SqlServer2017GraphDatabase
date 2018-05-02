USE $(dbname) 
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Country]') AND type in (N'U'))
BEGIN

    CREATE TABLE [Country] (
        [CountryID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [Name] [NVARCHAR](255),
		[IsoCode] [NVARCHAR](255)
    ) AS NODE;
    
END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[State]') AND type in (N'U'))
BEGIN

    CREATE TABLE [State] (
        [StateID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
		[Code] [NVARCHAR](255),
        [Name] [NVARCHAR](255)
    ) AS NODE;
    
END
GO


IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[City]') AND type in (N'U'))
BEGIN

    CREATE TABLE [City] (
        [CityID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [Name] [NVARCHAR](255)
    ) AS NODE;
    
END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Airport]') AND type in (N'U'))
BEGIN

    CREATE TABLE [Airport](
        [AirportID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [Identifier] NVARCHAR(255) NOT NULL,
        [Abbr] NVARCHAR(55),
        [Name] NVARCHAR(255),
        [City] NVARCHAR(255),
		[StateCode] NVARCHAR(255),
        [StateName] NVARCHAR(255),
        [Country] NVARCHAR(255),
		[CountryIsoCode] NVARCHAR(255),
    ) AS NODE;
    
END
GO


IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Aircraft]') AND type in (N'U'))
BEGIN

    CREATE TABLE [Aircraft](
        [AircraftID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [TailNumber] [NVARCHAR](255) NOT NULL
    ) AS NODE;
    
END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Carrier]') AND type in (N'U'))
BEGIN

    CREATE TABLE [Carrier](
        [CarrierID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [Code] [NVARCHAR](255),
        [Description] [NVARCHAR](255)
    ) AS NODE;
    
END
GO


IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Flight]') AND type in (N'U'))
BEGIN
     
    CREATE TABLE [Flight](
        [FlightID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
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
    ) AS NODE;

END
GO

IF  NOT EXISTS 
    (SELECT * FROM sys.objects 
     WHERE object_id = OBJECT_ID(N'[Reason]') AND type in (N'U'))
BEGIN

    CREATE TABLE [Reason] (
        [ReasonID] [INTEGER] IDENTITY(1,1) PRIMARY KEY,
        [Code] [NVARCHAR](55) NOT NULL,
        [Description] [NVARCHAR](255) NOT NULL
    ) AS NODE;
    
END
GO