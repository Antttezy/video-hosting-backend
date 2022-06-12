using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VideoHostingBackend.Core.Models;

[Index(nameof(VideoId), nameof(UserId), IsUnique = true)]
public class VideoLike
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Video))]
    [Required]
    public int VideoId { get; set; }

    [Required]
    public Video Video { get; set; } = null!;

    [ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; set; }

    [Required]
    public UserData User { get; set; } = null!;
}