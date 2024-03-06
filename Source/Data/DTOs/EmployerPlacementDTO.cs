using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class EmployerPlacementDTO
    {
        public int EmployerId { get; set; }
        public int AppliedCandidates { get;  set; }
        public string Name { get;  set; }
        public int PlacedCandidates { get;  set; }
        public object TotalJobPosted { get;  set; }
        public List<EmployerPlacementDTO> lst { get; set; }
        public List<User> lstEmployers { get; set; }
    }
}
