using ECommerceAPI.Application.Services;
using ECommerceAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services;

public class FileService : IFileService
{
    readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    string FileRenameAsync(string fileName)
    {
        return fileName;
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024,
                useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<(string fileName, string path)> UplaodAsync(string path, IFormFile file)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        string fileNewName = FileRenameAsync(file.FileName);

        bool isSuccess = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

        if (isSuccess) return (fileNewName, $"{uploadPath}\\{fileNewName}");

        return ("", "");
    }
}