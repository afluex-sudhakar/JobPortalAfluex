using System.Collections.Generic;
namespace Data.DTOs
{
    public class SkillDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public List<Skill> lst { get; set; }
        public Skill skill { get; set; }
    }

    public class UpdateSkillDTO : SkillDTO
    {
    }


    public class SkillResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SkillsResponseDTO
    {
        public List<SkillResponseDTO> Skills { get; set; }
    }
}
