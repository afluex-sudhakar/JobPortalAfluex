using System.Collections.Generic;

namespace Data.DTOs
{
    public class StateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public string StateName { get; set; }
        public List<State> lst { get; set; }
    }

    public class UpdateStateDTO : StateDTO
    {
    }
}
