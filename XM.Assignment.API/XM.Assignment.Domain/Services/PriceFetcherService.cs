using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Services
{
    internal class BitcoinPriceFetcherService
    {
        private readonly HttpClient _httpClient;
        private readonly IEnumerable<Source> _sources;

        public BitcoinPriceFetcherService(IHttpClientFactory httpClientFactory, IEnumerable<Source> sources)
        {
            _httpClient = httpClientFactory.CreateClient();
            this._sources = sources;
        }
    }
}
