using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface IVideoService
{
    Task<Video?> CreateVideo(UserData uploader, string name, Category category);
    Task<Video?> RevealVideo(Video video);
    Task<Comment?> AddComment(Video video, string text, UserData author);
}