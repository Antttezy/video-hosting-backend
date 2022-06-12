using Microsoft.AspNetCore.Http;

namespace VideoHostingBackend.Core.Services;

public interface IFileUploadService
{
    Task UploadImage(IFormFile file, string imageName);
    Task UploadVideo(IFormFile file, string videoName);
}