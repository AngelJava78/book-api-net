using BooksApiDemo.Services;
using Microsoft.AspNetCore.Authorization;

namespace BooksApiDemo.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationApiService _authorizationApiService;

    public PermissionAuthorizationHandler(
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationApiService authorizationApiService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authorizationApiService = authorizationApiService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        var authorizationHeader = httpContext.Request.Headers.Authorization.ToString();

        if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return;

        var accessToken = authorizationHeader["Bearer ".Length..].Trim();

        if (string.IsNullOrWhiteSpace(accessToken))
            return;

        var allowed = await _authorizationApiService.HasPermissionAsync(
            requirement.Permission,
            accessToken);

        if (allowed)
            context.Succeed(requirement);
    }
}
