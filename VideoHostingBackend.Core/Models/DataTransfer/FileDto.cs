using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class FileDto
{
    [Required]
    public IFormFile File { get; set; } = null!;
    
    [Required]
    [JsonPropertyName("video_file")]
    public string VideoFile { get; set; } = string.Empty;
}