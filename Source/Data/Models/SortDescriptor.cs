using System.ComponentModel;

namespace Data.Models
{
    public class SortDescriptor
    {
        public SortDescriptor(string member, ListSortDirection sortDirection)
        {
            Member = member;
            SortDirection = sortDirection;
        }

        public string Member { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}