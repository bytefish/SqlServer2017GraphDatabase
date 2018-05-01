// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AirlineOnTimePerformance.Sql.Extensions;
using AirlineOnTimePerformance.Sql.Model;
using Microsoft.SqlServer.Server;

namespace AirlineOnTimePerformance.Sql.Client
{
    public class FlightsBatchProcessor
    {
        private readonly string connectionString;

        public FlightsBatchProcessor(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Write(IList<Flight> items)
        {
            if (items == null)
            {
                return;
            }

            if (items.Count == 0)
            {
                return;
            }
            
            using (var connection = new SqlConnection(connectionString))
            {
                // Open the Connection:
                connection.Open();

                // Execute the Batch Write Command:
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    // Build the Stored Procedure Command:
                    cmd.CommandText = "[dbo].[InsertFlights]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Create the TVP:
                    SqlParameter parameter = new SqlParameter();

                    parameter.ParameterName = "@Entities";
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "[dbo].[FlightType]";
                    parameter.Value = ToSqlDataRecords(items);

                    // Add it as a Parameter:
                    cmd.Parameters.Add(parameter);

                    // And execute it:
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<SqlDataRecord> ToSqlDataRecords(IEnumerable<Flight> items)
        {
        // Construct the Data Record with the MetaData:
        SqlDataRecord sdr = new SqlDataRecord(
                new SqlMetaData("Year", SqlDbType.Int),
                new SqlMetaData("Month", SqlDbType.Int),
                new SqlMetaData("DayOfMonth", SqlDbType.Int),
                new SqlMetaData("DayOfWeek", SqlDbType.Int),
                new SqlMetaData("FlightDate", SqlDbType.DateTime2),
                new SqlMetaData("UniqueCarrier", SqlDbType.NVarChar, 55),
                new SqlMetaData("TailNumber", SqlDbType.NVarChar, 55),
                new SqlMetaData("FlightNumber", SqlDbType.NVarChar, 55),
                new SqlMetaData("OriginAirport", SqlDbType.NVarChar, 55),
                new SqlMetaData("OriginState", SqlDbType.NVarChar, 55),
                new SqlMetaData("DestinationAirport", SqlDbType.NVarChar, 55),
                new SqlMetaData("DestinationState", SqlDbType.NVarChar, 55),
                new SqlMetaData("DepartureDelay", SqlDbType.Int),
                new SqlMetaData("TaxiOut", SqlDbType.Int),
                new SqlMetaData("TaxiIn", SqlDbType.Int),
                new SqlMetaData("ArrivalDelay", SqlDbType.Int),
                new SqlMetaData("CancellationCode", SqlDbType.NVarChar, 55),
                new SqlMetaData("CarrierDelay", SqlDbType.Int),
                new SqlMetaData("WeatherDelay", SqlDbType.Int),
                new SqlMetaData("NasDelay", SqlDbType.Int),
                new SqlMetaData("SecurityDelay", SqlDbType.Int),
                new SqlMetaData("LateAircraftDelay", SqlDbType.Int)
            );
            
            // Now yield the Measurements in the Data Record:
            foreach (var item in items)
            {
                sdr.SetInt32(0, item.Year);
                sdr.SetInt32(1, item.Month);
                sdr.SetInt32(2, item.DayOfMonth);
                sdr.SetInt32(3, item.DayOfWeek);
                sdr.SetDateTime(4, item.FlightDate);
                sdr.SetString(5, item.UniqueCarrier);
                sdr.SetString(6, item.TailNumber);
                sdr.SetString(7, item.FlightNumber);
                sdr.SetString(8, item.OriginAirport);
                sdr.SetString(9, item.OriginState);
                sdr.SetString(10, item.DestinationAirport);
                sdr.SetString(11, item.DestinationState);
                sdr.SetNullableInt32(12, item.DepartureDelay);
                sdr.SetNullableInt32(13, item.TaxiOut);
                sdr.SetNullableInt32(14, item.TaxiIn);
                sdr.SetNullableInt32(15, item.ArrivalDelay);
                sdr.SetString(16, item.CancellationCode);
                sdr.SetNullableInt32(17, item.CarrierDelay);
                sdr.SetNullableInt32(18, item.WeatherDelay);
                sdr.SetNullableInt32(19, item.NasDelay);
                sdr.SetNullableInt32(20, item.SecurityDelay);
                sdr.SetNullableInt32(21, item.LateAircraftDelay);

                yield return sdr;
            }
        }
    }
}