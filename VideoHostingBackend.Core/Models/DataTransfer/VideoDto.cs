using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class VideoDto
{
    public CountryDto Country { get; set; } = null!;

    public string Animal { get; set; } = string.Empty;

    [JsonPropertyName("video_name")]
    public string VideoName { get; set; } = string.Empty;

    [JsonPropertyName("uploader_name")]
    public string UploaderName { get; set; } = string.Empty;

    [JsonPropertyName("likes_amount")]
    public string LikeAmount { get; set; } = string.Empty;

    [JsonPropertyName("views_amount")]
    public string ViewsAmount { get; set; } = string.Empty;

    [JsonPropertyName("cover_img")]
    public string CoverImg { get; set; } = string.Empty;
    
    [JsonPropertyName("video_id")]
    public string VideoId { get; set; } = string.Empty;

    [JsonPropertyName("coments")]
    public IEnumerable<CommentDto>? Comments { get; set; }
}