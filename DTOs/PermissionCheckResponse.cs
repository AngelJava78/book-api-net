namespace BooksApiDemo.DTOs;

public class PermissionCheckResponse
{
    public string Permission { get; set; } = string.Empty;
    public bool Allowed { get; set; }
}
