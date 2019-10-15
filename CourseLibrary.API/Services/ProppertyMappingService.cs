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

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var proppertyMapping = GetProppertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }
            //the string is separated by "," so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var proppertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching propperty
                if (!proppertyMapping.ContainsKey(proppertyName))
                {
                    return false;
                }
            }
            return true;
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
