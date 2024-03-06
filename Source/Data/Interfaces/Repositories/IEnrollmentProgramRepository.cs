using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IEnrollmentProgramRepository : IRepositoryBase<EnrollmentProgram>
    {
        bool Add(EnrollmentProgramDTO req);

        bool Update(UpdateEnrollmentProgramDTO req);

        bool Delete(int id, int userId);

        new List<EnrollmentProgram> GetAll();


        EnrollmentProgram GetDetailById(int Id);

        EnrollmentProgramResponseDTO GetEnrollmentProgram(EnrollmentProgramRequestDTO model);

        EnrollmentProgramResponseDTOWeb GetEnrollmentProgramWeb(EnrollmentProgramRequestDTO model, int UserId);

        int ApplyEnrollmentProgram(EnrollmentApplyDTO model);
    }
}
