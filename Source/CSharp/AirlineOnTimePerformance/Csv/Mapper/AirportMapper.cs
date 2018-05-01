// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using AirlineOnTimePerformance.Csv.Model;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace AirlineOnTimePerformance.Csv.Mapper
{
    public class AirportMapper : CsvMapping<Airport>
    {
        public AirportMapper()
        {
            MapProperty(1, x => x.AirportId);
            MapProperty(2, x => x.AirportAbbr);
            MapProperty(3, x => x.AirportName);
            MapProperty(4, x => x.AirportCityName);
            MapProperty(6, x => x.AirportWac);
            MapProperty(7, x => x.AirportCountryName);
            MapProperty(8, x => x.AirportCountryCodeISO);
            MapProperty(9, x => x.AirportStateName);
            MapProperty(10, x => x.AirportStateCode);
            MapProperty(28, x => x.AirportIsLatest, new BoolConverter("1", "0", StringComparison.OrdinalIgnoreCase));
        }
    }
}
