using Data.DTOs;

namespace Data.Interfaces.Repositories
{
    public interface IFeedbackRepository : IRepositoryBase<Feedback>
    {
        bool Add(FeedbackDTO req);

        bool AddForWeb(UpdateCMSDTO req);
    }
}
