using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    internal class Bitcoin
    {
        internal decimal Price { get; set; }
        internal DateTime TimeStamp { get; set; }
        internal Currency Currency { get; set; }
    }
}
