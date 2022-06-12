using System.ComponentModel.DataAnnotations;

namespace VideoHostingBackend.Core.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string LocalizedName { get; set; } = string.Empty;

    public ICollection<Video> Videos { get; set; } = null!;
}