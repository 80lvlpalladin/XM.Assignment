using System.Text.Json.Serialization;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public class PriceLogEntry
    {
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; }
        public Currency Currency { get; set; }
    }
}
