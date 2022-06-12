using System.ComponentModel.DataAnnotations;

namespace VideoHostingBackend.Core.Models;

public class Country
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}