using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IUserTrackingService
{
    public IQueryable<UserTracking> GetAllUserTrackingsAsync(ParametersModel parameters, UserTrackingSortEnum? sortOrder);
    Task<PagedList<UserTracking>> GetPagedUserTrackingsAsync(ParametersModel parameters, UserTrackingSortEnum? sortOrder);
    Task AddUserTrackingAsync(TrackingActionTypeEnum trackingActionType, string trackingTargetId, string description);
}