using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface IProppertyMappingService
    {
        Dictionary<string, ProppertyMappingValue> GetProppertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<T1, T2>(string fields);
    }
}