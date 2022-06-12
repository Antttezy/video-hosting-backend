using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class CommentDto
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}