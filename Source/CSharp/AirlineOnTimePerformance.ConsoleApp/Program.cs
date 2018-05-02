// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using AirlineOnTimePerformance.Converters;
using AirlineOnTimePerformance.Csv.Parser;
using AirlineOnTimePerformance.Sql.Client;
using TinyCsvParser;

namespace AirlineOnTimePerformance.ConsoleApp
{
    public class Program
    {
        // The ConnectionString used to decide which database to connect to:
        private static readonly string ConnectionString = @"Data Source=.\MSSQLSERVER2017;Integrated Security=true;Initial Catalog=AirlineOnTimePerformance;";

        public static void Main(string[] args)
        {
            var csvCarriersFile = @"D:\github\SqlServer2017GraphDatabase\Resources\UNIQUE_CARRIERS.csv";

            ProcessCarriers(csvCarriersFile);

            var csvAirportsFile = @"D:\github\SqlServer2017GraphDatabase\Resources\56803256_T_MASTER_CORD.csv";

            ProcessAirports(csvAirportsFile);
            
            // Import all hourly weather data from 2014:
            var csvFlightFiles = new[]
            {
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201401.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201402.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201403.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201404.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201405.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201406.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201407.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201408.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201409.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201410.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201411.csv",
                "D:\\datasets\\AOTP\\ZIP\\AirOnTimeCSV_1987_2017\\AirOnTimeCSV\\airOT201412.csv",
            };
            
            foreach (var csvFlightStatisticsFile in csvFlightFiles)
            {
                ProcessFlights(csvFlightStatisticsFile);
            }
        }

        private static void ProcessCarriers(string csvFilePath)
        {
            // Construct the Batch Processor:
            var processor = new CarriersBatchProcessor(ConnectionString);


            // Create the Converter:
            var converter = new CarrierConverter();

            // Access to the List of Parsers:
            Parsers
                // Use the LocalWeatherData Parser:
                .CarrierParser
                // Read the File:
                .ReadFromFile(csvFilePath, Encoding.UTF8)
                // As an Observable:
                .ToObservable()
                // Batch in 80000 Entities / or wait 1 Second:
                .Buffer(TimeSpan.FromSeconds(5), 80000)
                // And subscribe to the Batch
                .Subscribe(records =>
                {
                    var validRecords = records
                        // Get the Valid Results:
                        .Where(x => x.IsValid)
                        // And get the populated Entities:
                        .Select(x => x.Result)
                        // Convert into the Sql Data Model:
                        .Select(x => converter.Convert(x))
                        // Evaluate:
                        .ToList();

                    // Finally write them with the Batch Writer:
                    processor.Write(validRecords);
                });
        }

        private static void ProcessAirports(string csvFilePath)
        {
            // Construct the Batch Processor:
            var processor = new AirportsBatchProcessor(ConnectionString);


            // Create the Converter:
            var converter = new AirportConverter();

            // Access to the List of Parsers:
            Parsers
                // Use the LocalWeatherData Parser:
                .AirportParser
                // Read the File:
                .ReadFromFile(csvFilePath, Encoding.UTF8)
                // As an Observable:
                .ToObservable()
                // Batch in 80000 Entities / or wait 1 Second:
                .Buffer(TimeSpan.FromSeconds(1), 80000)
                // And subscribe to the Batch
                .Subscribe(records =>
                {
                    var validRecords = records
                        // Get the Valid Results:
                        .Where(x => x.IsValid)
                        // And get the populated Entities:
                        .Select(x => x.Result)
                        // Only use latest Airports:
                        .Where(x => x.AirportIsLatest)
                        // Convert into the Sql Data Model:
                        .Select(x => converter.Convert(x))
                        // Evaluate:
                        .ToList();

                    // Finally write them with the Batch Writer:
                    processor.Write(validRecords);
                });
        }

        private static void ProcessFlights(string csvFilePath)
        {
            // Construct the Batch Processor:
            var processor = new FlightsBatchProcessor(ConnectionString);


            // Create the Converter:
            var converter = new FlightConverter();

            // Access to the List of Parsers:
            Parsers
                // Use the LocalWeatherData Parser:
                .FlightStatisticsParser
                // Read the File:
                .ReadFromFile(csvFilePath, Encoding.UTF8)
                // As an Observable:
                .ToObservable()
                // Batch in 80000 Entities / or wait 1 Second:
                .Buffer(TimeSpan.FromSeconds(5), 80000)
                // And subscribe to the Batch
                .Subscribe(records =>
                {
                    var validRecords = records
                        // Get the Valid Results:
                        .Where(x => x.IsValid)
                        // And get the populated Entities:
                        .Select(x => x.Result)
                        // Group by WBAN, Date and Time to avoid duplicates for this batch:
                        .GroupBy(x => new { x.UniqueCarrier, x.FlightNumber, x.FlightDate })
                        // If there are duplicates then make a guess and select the first one:
                        .Select(x => x.First())
                        // Convert into the Sql Data Model:
                        .Select(x => converter.Convert(x))
                        // Evaluate:
                        .ToList();

                    // Finally write them with the Batch Writer:
                    processor.Write(validRecords);
                });
        }
    }
}
