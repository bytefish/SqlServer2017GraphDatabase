// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AirlineOnTimePerformance.Sql.Model;
using Microsoft.SqlServer.Server;

namespace AirlineOnTimePerformance.Sql.Client
{
    public class AirportsBatchProcessor
    {
        private readonly string connectionString;

        public AirportsBatchProcessor(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Write(IList<Airport> items)
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
                    cmd.CommandText = "[dbo].[InsertAirports]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Create the TVP:
                    SqlParameter parameter = new SqlParameter();

                    parameter.ParameterName = "@Entities";
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "[dbo].[AirportType]";
                    parameter.Value = ToSqlDataRecords(items);

                    // Add it as a Parameter:
                    cmd.Parameters.Add(parameter);

                    // And execute it:
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IEnumerable<SqlDataRecord> ToSqlDataRecords(IEnumerable<Airport> items)
        {
        // Construct the Data Record with the MetaData:
        SqlDataRecord sdr = new SqlDataRecord(
                new SqlMetaData("Identifier", SqlDbType.NVarChar, 55),
                new SqlMetaData("Abbr", SqlDbType.NVarChar, 55),
                new SqlMetaData("Name", SqlDbType.NVarChar, 55),
                new SqlMetaData("City", SqlDbType.NVarChar, 55),
                new SqlMetaData("State", SqlDbType.NVarChar, 55),
                new SqlMetaData("Country", SqlDbType.NVarChar, 55)
            );
            
            // Now yield the Measurements in the Data Record:
            foreach (var item in items)
            {
                sdr.SetString(0, item.AirportId);
                sdr.SetString(1, item.Abbr);
                sdr.SetString(2, item.Name);
                sdr.SetString(3, item.City);
                sdr.SetString(4, item.State);
                sdr.SetString(5, item.Country);

                yield return sdr;
            }
        }
    }
}