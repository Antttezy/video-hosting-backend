using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class CreateCommentDto
{
    [JsonPropertyName("video_id")]
    [Required]
    public string VideoId { get; set; } = string.Empty;

    [Required]
    public string Text { get; set; } = string.Empty;
}