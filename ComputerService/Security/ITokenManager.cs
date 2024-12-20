﻿namespace ComputerService.Security;

public interface ITokenManager
{
    Task<bool> IsCurrentActiveToken();
    Task DeactivateCurrentAsync();
    Task<bool> IsActiveAsync(string token);
    Task DeactivateAsync(string token);
    public Guid GetCurrentUserId();
}