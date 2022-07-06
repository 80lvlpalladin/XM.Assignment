using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.Assignment.Domain.Deserializers
{
    public interface IDeserializerProvider
    {
        public IDeserializer GetBySourceName(string sourceName);
    }
}
