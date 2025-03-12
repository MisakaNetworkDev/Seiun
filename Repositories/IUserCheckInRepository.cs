using Seiun.Entities;

namespace Seiun.Repositories;

public interface IUserCheckInRepository : IBaseRepository<UserCheckInEntity>
{
    Task<bool> CheckInTodayAsync(Guid userId);
    Task<List<UserCheckInEntity>> GetUserCheckInsAsync(Guid userId);
}