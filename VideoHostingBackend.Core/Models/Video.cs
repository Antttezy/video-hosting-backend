using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VideoHostingBackend.Core.Models;

[Index(nameof(CountryId))]
[Index(nameof(UploaderId))]
[Index(nameof(CategoryId))]
public class Video
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Category))]
    [Required]
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    [Required]
    public string VideoName { get; set; } = string.Empty;
    
    [Required]
    public string VideoId { get; set; } = string.Empty;

    [Required]
    public string CoverImg { get; set; } = string.Empty;

    [ForeignKey(nameof(Country))]
    [Required]
    public int CountryId { get; set; }

    [Required]
    public Country Country { get; set; } = null!;
    
    [ForeignKey(nameof(Uploader))]
    [Required]
    public int UploaderId { get; set; }

    [Required]
    public UserData Uploader { get; set; } = null!;

    public ICollection<VideoLike> Likes { get; set; } = new List<VideoLike>();

    public ICollection<Comment>? Comments { get; set; }

    [Required]
    public bool Uploaded { get; set; } = true;
}