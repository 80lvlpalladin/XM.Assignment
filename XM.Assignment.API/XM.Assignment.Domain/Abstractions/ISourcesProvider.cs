using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XM.Assignment.Domain.Models;

namespace XM.Assignment.Domain.Abstractions
{
    public interface ISourcesProvider
    {
        public IEnumerable<Source> GetAll();
    }
}
