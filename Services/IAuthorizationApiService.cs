namespace BooksApiDemo.Services;

public interface IAuthorizationApiService
{
    Task<bool> HasPermissionAsync(string permission, string accessToken);
}
