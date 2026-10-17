using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BooksApiDemo.DTOs;

namespace BooksApiDemo.Services;

public class AuthorizationApiService : IAuthorizationApiService
{
    private readonly HttpClient _httpClient;

    public AuthorizationApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> HasPermissionAsync(string permission, string accessToken)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/authorization/check?permission={Uri.EscapeDataString(permission)}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using var response = await _httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized ||
            response.StatusCode == HttpStatusCode.Forbidden)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PermissionCheckResponse>();

        return result?.Allowed == true;
    }
}
