// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AirlineOnTimePerformance.Sql.Model
{
    public class Airport
    {
        public string AirportId { get; set; }

        public string Abbr { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        public string Country { get; set; }
    }
}
