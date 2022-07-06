using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface IPriceFetcherService
    {
        public Task<PriceLogEntry?> GetCurrentPriceAsync(string sourceName, string currency);
    }
}
