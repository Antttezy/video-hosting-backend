using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class UploadDto
{
    [Required]
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
    
    [Required]
    [JsonPropertyName("video_name")]
    public string VideoName { get; set; } = string.Empty;
    
    // [Required]
    // [JsonPropertyName("country")]
    // public string Country { get; set; } = string.Empty;
}