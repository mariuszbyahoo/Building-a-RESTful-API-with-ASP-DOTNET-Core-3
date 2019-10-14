using CourseLibrary.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string, ProppertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            // the orderBy string is separated by ",", so we split it.
            var orderByAfterSplit = orderBy.Split(',');

            // apply each orderby clause in reverse order - otherwise, the 
            // IQueryable will be ordered the wrong order
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                // trim the orderBy clause, as it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var trimmedOrderByClause = orderByClause.Trim();

                // if the sort option ends with with " desc", we order
                // descending, otherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // remove " asc" or " desc" from the orderByClause, so we
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // finding the matching property
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                // get the PropertyMappingValue
                var proppertyMappingValue = mappingDictionary[propertyName];

                if (proppertyMappingValue == null)
                {
                    throw new ArgumentNullException("proppertyMappingValue");
                }

                // Run through the property names in reverse
                // so the orderby clauses are applied in the correct order
                foreach (var destinationPropperty in proppertyMappingValue.DestinationProperties.Reverse())
                {
                    // revert sort order if necessary
                    if (proppertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    source = source.OrderBy(destinationPropperty +
                        (orderDescending ? " descending" : " ascending"));
                }
            }

            return source;
        }
    }
}
