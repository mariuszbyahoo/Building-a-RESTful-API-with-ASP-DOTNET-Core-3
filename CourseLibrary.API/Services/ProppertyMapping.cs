using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class ProppertyMapping<TSource, TDestination> : IProppertyMapping
    {
        public Dictionary<string, ProppertyMappingValue> _mappingDictionary { get; private set; }
        public ProppertyMapping(Dictionary<string, ProppertyMappingValue> mappingDictionary)
        {
            _mappingDictionary = mappingDictionary ??
                throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
