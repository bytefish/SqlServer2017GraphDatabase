﻿using System;

namespace AirlineOnTimePerformance.Sql.Model
{
    public class Flight
    {
        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Day of Month.
        /// </summary>
        public int DayOfMonth { get; set; }

        /// <summary>
        /// Day of Week.
        /// </summary>
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Flight Date (yyyy-MM-dd)
        /// </summary>
        public DateTime FlightDate { get; set; }

        /// <summary>
        /// Unique Carrier Code. When the same code has been used by multiple carriers, a numeric suffix is 
        /// used for earlier users, for example, PA, PA(1), PA(2). Use this field for analysis across a range 
        /// of years.
        /// </summary>
        public string UniqueCarrier { get; set; }

        /// <summary>
        /// Tail Number.
        /// </summary>
        public string TailNumber { get; set; }

        /// <summary>
        /// Flight Number.
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Origin Airport, Airport ID. An identification number assigned by US DOT to identify a unique airport. Use 
        /// this field for airport analysis across a range of years because an airport can change its airport code 
        /// and airport codes can be reused.
        /// </summary>
        public string OriginAirport { get; set; }

        /// <summary>
        /// Origin Airport, State Code.
        /// </summary>
        public string OriginState { get; set; }

        /// <summary>
        /// Destination Airport, Airport ID. An identification number assigned by US DOT to identify a unique airport. Use 
        /// this field for airport analysis across a range of years because an airport can change its airport code 
        /// and airport codes can be reused.
        /// </summary>
        public string DestinationAirport { get; set; }
        
        /// <summary>
        /// Destination Airport, State Code.
        /// </summary>
        public string DestinationState { get; set; }

        /// <summary>
        /// Difference in minutes between scheduled and actual departure time. Early departures show negative numbers.
        /// </summary>
        public int? DepartureDelay { get; set; }

        /// <summary>
        /// Taxi Out Time, in Minutes.
        /// </summary>
        public int? TaxiOut { get; set; }

        /// <summary>
        /// Taxi In Time, in Minutes.
        /// </summary>
        public int? TaxiIn { get; set; }

        /// <summary>
        /// Difference in minutes between scheduled and actual arrival time. Early arrivals show negative numbers.
        /// </summary>
        public int? ArrivalDelay { get; set; }

        /// <summary>
        /// Specifies The Reason For Cancellation.
        /// </summary>
        public string CancellationCode { get; set; }

        /// <summary>
        /// Carrier Delay, in Minutes.
        /// </summary>
        public int? CarrierDelay { get; set; }

        /// <summary>
        /// Weather Delay, in Minutes.
        /// </summary>
        public int? WeatherDelay { get; set; }

        /// <summary>
        /// National Air System Delay, in Minutes.
        /// </summary>
        public int? NasDelay { get; set; }

        /// <summary>
        /// Security Delay, in Minutes.
        /// </summary>
        public int? SecurityDelay { get; set; }

        /// <summary>
        /// Late Aircraft Delay, in Minutes.
        /// </summary>
        public int? LateAircraftDelay { get; set; }
    }
}