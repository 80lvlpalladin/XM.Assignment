using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Models.Enums;

namespace XM.Assignment.Domain.Models
{
    public class PriceHistoryRequestModel
    {
        public string FilterValue { get; set; }

        public string Currency { get; set; }

        public FilteringOptions FilteringOptions { get; set; }

        public OrderingOptions OrderingOptions { get; set; }
    }
}
