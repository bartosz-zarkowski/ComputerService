using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.Security;
using FluentValidation;
using static System.String;

namespace ComputerService.Services;
public class UserTrackingService : BaseEntityService<UserTracking>, IUserTrackingService
{
    private readonly IUserService _userService;
    private readonly ITokenManager _tokenManager;
    public UserTrackingService(ComputerServiceContext context, IValidator<UserTracking> validator, IMapper mapper, IUserService userService, ITokenManager tokenManager) : base(context, validator, mapper)
    {
        _userService = userService;
        _tokenManager = tokenManager;
    }

    public IQueryable<UserTracking> GetAllUserTrackingsAsync(ParametersModel parameters, UserTrackingSortEnum? sortOrder)
    {
        var userTrackings = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            userTrackings = sortOrder switch
            {
                UserTrackingSortEnum.LastName => asc
                    ? userTrackings.OrderBy(userTracking => userTracking.LastName)
                    : userTrackings.OrderByDescending(userTracking => userTracking.LastName),
                UserTrackingSortEnum.Type => asc
                    ? userTrackings.OrderBy(userTracking => userTracking.TrackingActionType)
                    : userTrackings.OrderByDescending(userTracking => userTracking.TrackingActionType),
                UserTrackingSortEnum.Date => asc
                    ? userTrackings.OrderBy(userTracking => userTracking.Date)
                    : userTrackings.OrderByDescending(userTracking => userTracking.Date),
                _ => throw new ArgumentException()
            };
        }

        if (!IsNullOrEmpty(parameters.searchString))
        {
            userTrackings = userTrackings.Where(userTracking => userTracking.FirstName.Contains(parameters.searchString) ||
                                                                userTracking.FirstName.Contains(parameters.searchString) ||
                                                                userTracking.Description.Contains(parameters.searchString));
        }

        return userTrackings;
    }

    public async Task<PagedList<UserTracking>> GetPagedUserTrackingsAsync(ParametersModel parameters, UserTrackingSortEnum? sortOrder)
    {
        return await PagedList<UserTracking>.ToPagedListAsync(GetAllUserTrackingsAsync(parameters, sortOrder), parameters);
    }

    public async Task AddUserTrackingAsync(TrackingActionTypeEnum trackingActionType, string trackingTargetId, string description)
    {
        var user = await _userService.GetUserAsync(_tokenManager.GetCurrentUserId());
        var userTracking = new UserTracking(
            user.FirstName,
            user.LastName,
            trackingActionType,
            trackingTargetId,
            DateTimeOffset.Now,
            description
        );
        await ValidateEntityAsync(userTracking);
        await CreateAsync(userTracking);
    }
}