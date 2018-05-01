// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using TinyCsvParser.TypeConverter;

namespace AirlineOnTimePerformance.Csv.Converter
{
    public class FlightDurationConverter : ITypeConverter<TimeSpan?>
    {
        private readonly NullableTimeSpanConverter converter;

        public FlightDurationConverter()
            : this(new NullableTimeSpanConverter("hhmm"))
        {
        }

        public FlightDurationConverter(NullableTimeSpanConverter converter)
        {
            this.converter = converter;
        }

        public bool TryConvert(string value, out TimeSpan? result)
        {
            result = default(TimeSpan?);

            // There are yy:mm times in the CSV with 24:00, which is not a valid TimeSpan.
            // If we encounter a "2400" in the data, we are going to convert it into a TimeSpan 
            // of 1 day:
            if (string.Equals(value, "2400", StringComparison.CurrentCultureIgnoreCase))
            {
                result = TimeSpan.FromDays(1).Subtract(TimeSpan.FromSeconds(-1));

                return true;
            }

            // Try to convert the TimeSpan:
            return converter.TryConvert(value, out result);
        }

        public Type TargetType
        {
            get { return typeof(TimeSpan?); }
        }
    }
}
