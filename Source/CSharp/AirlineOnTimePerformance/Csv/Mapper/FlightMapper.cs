// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using AirlineOnTimePerformance.Csv.Converter;
using AirlineOnTimePerformance.Csv.Model;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace AirlineOnTimePerformance.Csv.Mapper
{
    public class FlightMapper : CsvMapping<Flight>
    {
        public FlightMapper()
        {
            MapProperty(0, x => x.Year);
            MapProperty(1, x => x.Month);
            MapProperty(2, x => x.DayOfMonth);
            MapProperty(3, x => x.DayOfWeek);
            MapProperty(4, x => x.FlightDate, new DateTimeConverter("yyyy-MM-dd"));
            MapProperty(5, x => x.UniqueCarrier);
            MapProperty(6, x => x.TailNumber);
            MapProperty(7, x => x.FlightNumber);
            MapProperty(8, x => x.OriginAirport);
            MapProperty(9, x => x.Origin);
            MapProperty(10, x => x.OriginState);
            MapProperty(11, x => x.DestinationAirport);
            MapProperty(12, x => x.Destination);
            MapProperty(13, x => x.DestinationState);
            MapProperty(14, x => x.ScheduledDepatureTime, new FlightDurationConverter());
            MapProperty(15, x => x.ActualDepartureTime, new FlightDurationConverter());
            MapProperty(16, x => x.DepartureDelay, new FlightMinutesConverter());
            MapProperty(17, x => x.DepartureDelayNew, new FlightMinutesConverter());
            MapProperty(18, x => x.DepartureDelayIndicator, new NullableBoolConverter("1.00", "0.00", StringComparison.OrdinalIgnoreCase));
            MapProperty(19, x => x.DepartureDelayGroup);
            MapProperty(20, x => x.TaxiOut, new FlightMinutesConverter());
            MapProperty(21, x => x.WheelsOff, new FlightDurationConverter());
            MapProperty(22, x => x.WheelsOn, new FlightDurationConverter());
            MapProperty(23, x => x.TaxiIn, new FlightMinutesConverter());
            MapProperty(24, x => x.ScheduledArrivalTime, new FlightDurationConverter());
            MapProperty(25, x => x.ActualArrivalTime, new FlightDurationConverter());
            MapProperty(26, x => x.ArrivalDelay, new FlightMinutesConverter());
            MapProperty(27, x => x.ArrivalDelayNew, new FlightMinutesConverter());
            MapProperty(28, x => x.ArrivalDelayIndicator, new NullableBoolConverter("1.00", "0.00", StringComparison.OrdinalIgnoreCase));
            MapProperty(29, x => x.ArrivalDelayGroup);
            MapProperty(30, x => x.CancelledFlight, new NullableBoolConverter("1.00", "0.00", StringComparison.OrdinalIgnoreCase));
            MapProperty(31, x => x.CancellationCode);
            MapProperty(32, x => x.DivertedFlight, new NullableBoolConverter("1.00", "0.00", StringComparison.OrdinalIgnoreCase));
            MapProperty(33, x => x.ScheduledElapsedTimeOfFlight, new FlightMinutesConverter());
            MapProperty(34, x => x.ActualElapsedTimeOfFlight, new FlightMinutesConverter());
            MapProperty(35, x => x.AirTime, new FlightMinutesConverter());
            MapProperty(36, x => x.NumberOfFlights, new FlightMinutesConverter());
            MapProperty(37, x => x.Distance);
            MapProperty(38, x => x.DistanceGroup);
            MapProperty(39, x => x.CarrierDelay, new FlightMinutesConverter());
            MapProperty(40, x => x.WeatherDelay, new FlightMinutesConverter());
            MapProperty(41, x => x.NasDelay, new FlightMinutesConverter());
            MapProperty(42, x => x.SecurityDelay, new FlightMinutesConverter());
            MapProperty(43, x => x.LateAircraftDelay, new FlightMinutesConverter());
        }
    }
}
