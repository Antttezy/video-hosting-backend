using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Services;
using VideoHostingBackend.Data;
using VideoHostingBackend.Util;

namespace VideoHostingBackend.Services.Implementations;

internal class VideoService : IVideoService
{
    private readonly VideoContext _context;

    public VideoService(VideoContext context)
    {
        _context = context;
    }

    public async Task<Video?> CreateVideo(UserData uploader, string name, Category category)
    {
        Country? country = await _context.Countries
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();

        if (country is null)
        {
            return null;
        }

        var link = RandomStringGenerator.GenerateName();

        Video video = new()
        {
            CategoryId = category.Id,
            CountryId = country.Id,
            UploaderId = uploader.Id,
            VideoName = name,
            Uploaded = false,
            VideoId = link + ".mp4",
            CoverImg = link + ".png"
        };

        try
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();

            video.Country = country;
            video.Uploader = uploader;
            video.Category = category;
            return video;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Video?> RevealVideo(Video video)
    {
        if (!video.Uploaded)
        {
            video.Uploaded = true;

            try
            {
                _context.Videos.Update(video);
                await _context.SaveChangesAsync();
                return video;
            }
            catch (Exception)
            {
                return null;
            }
        }

        return video;
    }
}