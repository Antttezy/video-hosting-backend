using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Services;
using VideoHostingBackend.Data;

namespace VideoHostingBackend.Services.Implementations;

internal class VideoRepository : IVideoRepository
{
    private readonly VideoContext _context;

    public VideoRepository(VideoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Video>> GetVideosInCategory(string categoryName)
    {
        var videos = await _context.Videos
            .AsNoTracking()
            .Include(v => v.Category)
            .Include(v => v.Country)
            .Include(v => v.Uploader)
            .Include(v => v.Likes)
            .Include(v => v.Comments)
            .Where(v => v.Category.Name == categoryName)
            .Where(v => v.Uploaded)
            .ToListAsync();
        
        return videos;
    }

    public async Task<Video?> GetByCoverName(string coverName)
    {
        Video? video = await _context.Videos
            .AsNoTracking()
            .Include(v => v.Category)
            .Include(v => v.Country)
            .Include(v => v.Uploader)
            .Include(v => v.Likes)
            .Include(v => v.Comments)
            .FirstOrDefaultAsync(v => v.CoverImg == coverName);

        return video;
    }

    public async Task<Video?> GetByVideoName(string videoName)
    {
        Video? video = await _context.Videos
            .AsNoTracking()
            .Include(v => v.Category)
            .Include(v => v.Country)
            .Include(v => v.Uploader)
            .Include(v => v.Likes)
            .Include(v => v.Comments)
            .FirstOrDefaultAsync(v => v.VideoId == videoName);

        return video;
    }
}