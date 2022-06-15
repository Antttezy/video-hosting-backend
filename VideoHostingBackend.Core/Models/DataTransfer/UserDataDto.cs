using System.ComponentModel.DataAnnotations;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class UserDataDto
{
    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    public string Sex { get; set; } = string.Empty;
}