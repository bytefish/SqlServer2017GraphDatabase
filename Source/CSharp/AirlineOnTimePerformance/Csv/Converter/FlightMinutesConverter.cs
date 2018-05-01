// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using TinyCsvParser.TypeConverter;

namespace AirlineOnTimePerformance.Csv.Converter
{
    public class FlightMinutesConverter : ITypeConverter<int?>
    {
        private readonly NullableSingleConverter converter;

        public FlightMinutesConverter()
            : this(new NullableSingleConverter())
        {
        }

        public FlightMinutesConverter(NullableSingleConverter converter)
        {
            this.converter = converter;
        }

        public bool TryConvert(string value, out int? result)
        {
            result = default(int?);

            Single? singleValue;

            if (!converter.TryConvert(value, out singleValue))
            {
                return false;
            }

            if (!singleValue.HasValue)
            {
                return true;
            }

            try
            {
                result = Convert.ToInt32(singleValue.Value);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Type TargetType
        {
            get { return typeof(int?); }
        }
    }
}
