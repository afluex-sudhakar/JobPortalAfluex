using System.Collections.Generic;

namespace Data.Models
{
    public class FilteredSet<TEntity> where TEntity : class
    {
        public FilteredSet(IEnumerable<TEntity> set, int totalCount)
        {
            Set = set;
            TotalCount = totalCount;
        }

        public IEnumerable<TEntity> Set { get; set; }
        public int TotalCount { get; set; }
    }
}
