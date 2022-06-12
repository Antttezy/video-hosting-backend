using System.ComponentModel.DataAnnotations;

namespace VideoHostingBackend.Core.Models;

public class UserData
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string User { get; set; } = string.Empty;

    [Required]
    public string Sex { get; set; } = string.Empty;

    public ICollection<VideoLike> LikedVideos { get; set; } = new List<VideoLike>();
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public UserCredentials? Credentials { get; set; }

    [Required]
    public string Avatar { get; set; } = string.Empty;
    
    [Required]
    public string BackgroundImage { get; set; } = string.Empty;
}