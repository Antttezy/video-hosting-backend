using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VideoHostingBackend.Core.Models;

[Index(nameof(UserId), IsUnique = true)]
public class UserCredentials
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; set; }

    [Required]
    public UserData User { get; set; } = null!;

    [Required]
    public string Password { get; set; } = string.Empty;
}