using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Services;

public interface IFileService
{
    Task<(string fileName, string path)> UplaodAsync(string path, IFormFile file);
    Task<bool> CopyFileAsync(string path, IFormFile file);
}