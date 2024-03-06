using Data.DTOs;
using System.Collections.Generic;
using Utility.Enums;

namespace Data.Interfaces.Repositories
{
    public interface ISkillRepository : IRepositoryBase<Skill>
    {
        bool Add(SkillDTO req);

        bool Update(UpdateSkillDTO req);

        bool Delete(int id, int userId);

        new List<Skill> GetAll();


        List<SkillDTO> GetSkill(Language lang);

        List<Skill> GetDetailById(int Id);

        List<SkillResponseDTO> GetSkillsMobile(LanguageFilterDTO req);

        Skill GetSkillById(int Id);
    }
}
