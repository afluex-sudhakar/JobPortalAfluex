using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface ITrainingMaterialRepository : IRepositoryBase<TrainingMaterial>
    {
        bool Add(TrainingMaterialDTO req);

        bool Update(UpdateTrainingMaterialDTO req);

        bool Delete(int id, int userId);

        new List<TrainingMaterial> GetAll();

        TrainingMaterial GetDetailById(int Id);

        TrainingMaterialResponseDTO GetTrainingMaterial(TrainingMaterialRequestDTO model);
    }
}
