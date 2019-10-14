using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Services
{
    public class ProppertyMappingService : IProppertyMappingService
    {
        private Dictionary<string, ProppertyMappingValue> _authorProppertyMapping =
            new Dictionary<string, ProppertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id", new ProppertyMappingValue(new List<string>() {"Id"}) },
                {"MainCategory", new ProppertyMappingValue(new List<string>() { "MainCategory"}) },
                {"Age", new ProppertyMappingValue(new List<string>() {"DateOfBirth"}, true) },
                {"Name", new ProppertyMappingValue(new List<string>() {"FirstName", "LastName"}) }
            };

        private IList<IProppertyMapping> _proppertyMappings = new List<IProppertyMapping>();

        public ProppertyMappingService()
        {
            _proppertyMappings.Add(new ProppertyMapping<AuthorDto, Author>(_authorProppertyMapping));
        }

        public Dictionary<string, ProppertyMappingValue> GetProppertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _proppertyMappings
                .OfType<ProppertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)}, {typeof(TDestination)}");
        }
    }
}
