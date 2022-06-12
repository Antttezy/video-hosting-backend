using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Data;

public class VideoContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<UserData> Users { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<VideoLike> Likes { get; set; } = null!;
    public DbSet<UserCredentials> Credentials { get; set; } = null!;

    public VideoContext(DbContextOptions options) : base(options)
    {
    }
}