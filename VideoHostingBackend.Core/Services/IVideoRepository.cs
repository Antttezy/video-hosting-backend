using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface IVideoRepository
{
    Task<IEnumerable<Video>> GetVideosInCategory(string categoryName);
    Task<Video?> GetByCoverName(string coverName);
    Task<Video?> GetByVideoName(string videoName);
}