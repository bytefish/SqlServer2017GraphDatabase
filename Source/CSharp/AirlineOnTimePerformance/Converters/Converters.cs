// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

using CsvFlightType = AirlineOnTimePerformance.Csv.Model.Flight;
using SqlFlightType = AirlineOnTimePerformance.Sql.Model.Flight;

using CsvAirportType = AirlineOnTimePerformance.Csv.Model.Airport;
using SqlAirportType = AirlineOnTimePerformance.Sql.Model.Airport;

using CsvCarrierType = AirlineOnTimePerformance.Csv.Model.Carrier;
using SqlCarrierType = AirlineOnTimePerformance.Sql.Model.Carrier;


namespace AirlineOnTimePerformance.Converters
{
    public interface IConverter<in TSourceType, out TTargetType>
    {
        TTargetType Convert(TSourceType source);
    }

    public abstract class BaseConverter<TSourceType, TTargetType> : IConverter<TSourceType, TTargetType>
        where TSourceType : class
        where TTargetType : class, new()
    {
        public TTargetType Convert(TSourceType source)
        {
            if (source == null)
            {
                return null;
            }

            TTargetType target = new TTargetType();

            InternalConvert(source, target);

            return target;
        }

        protected abstract void InternalConvert(TSourceType source, TTargetType target);
    }

    public class FlightConverter : BaseConverter<CsvFlightType, SqlFlightType>
    {
        protected override void InternalConvert(CsvFlightType source, SqlFlightType target)
        {
            target.Year = source.Year;
            target.Month = source.Month;
            target.DayOfMonth = source.DayOfMonth;
            target.DayOfWeek = source.DayOfWeek;
            target.FlightDate = source.FlightDate;
            target.UniqueCarrier = source.UniqueCarrier;
            target.TailNumber = source.TailNumber;
            target.FlightNumber = source.FlightNumber;
            target.OriginAirport = source.OriginAirport;
            target.OriginState = source.OriginState;
            target.DestinationAirport = source.DestinationAirport;
            target.DestinationState = source.DestinationState;
            target.DepartureDelay = source.DepartureDelay;
            target.TaxiOut = source.TaxiOut;
            target.TaxiIn = source.TaxiIn;
            target.ArrivalDelay = source.ArrivalDelay;
            target.CancellationCode = source.CancellationCode;
            target.CarrierDelay = source.CarrierDelay;
            target.WeatherDelay = source.WeatherDelay;
            target.NasDelay = source.NasDelay;
            target.SecurityDelay = source.SecurityDelay;
            target.LateAircraftDelay = source.LateAircraftDelay;
        }
    }
    
    public class AirportConverter : BaseConverter<CsvAirportType, SqlAirportType>
    {
        protected override void InternalConvert(CsvAirportType source, SqlAirportType target)
        {
            target.AirportId = source.AirportId;
            target.Abbr = source.AirportAbbr;
            target.Name = source.AirportName;
            target.City = source.AirportCityName;
            target.State = source.AirportStateCode;
            target.Country = source.AirportCountryName;
        }
    }

    public class CarrierConverter : BaseConverter<CsvCarrierType, SqlCarrierType>
    {
        protected override void InternalConvert(CsvCarrierType source, SqlCarrierType target)
        {
            target.Code = source.Code;
            target.Description = source.Description;
        }
    }
}
