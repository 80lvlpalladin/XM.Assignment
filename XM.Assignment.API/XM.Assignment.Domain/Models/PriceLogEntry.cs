﻿using System.Text.Json.Serialization;
using XM.Assignment.Domain.Deserialiazation.Converters;

namespace XM.Assignment.Domain.Models;

public class PriceLogEntry
{
    [JsonConverter(typeof(DecimalConverter))]
    public decimal Price { get; set; }
    public uint TimeStamp { get; set; }
};
