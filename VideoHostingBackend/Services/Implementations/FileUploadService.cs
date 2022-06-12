using VideoHostingBackend.Core.Services;

namespace VideoHostingBackend.Services.Implementations;

internal class FileUploadService: IFileUploadService
{
    private readonly IWebHostEnvironment _environment;

    public FileUploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public async Task UploadImage(IFormFile file, string imageName)
    {
        var coverPath = Path.Combine(_environment.WebRootPath, "thumbnails", imageName);
        await using Stream stream = new FileStream(coverPath, FileMode.OpenOrCreate);
        await file.CopyToAsync(stream);
    }

    public async Task UploadVideo(IFormFile file, string videoName)
    {
        var videoPath = Path.Combine(_environment.WebRootPath, "videos", videoName);
        await using Stream stream = new FileStream(videoPath, FileMode.OpenOrCreate);
        await file.CopyToAsync(stream);
    }
}