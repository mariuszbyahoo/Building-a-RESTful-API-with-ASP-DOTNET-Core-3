using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface IProppertyMappingService
    {
        Dictionary<string, ProppertyMappingValue> GetProppertyMapping<TSource, TDestination>();
    }
}