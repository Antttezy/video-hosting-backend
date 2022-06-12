using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class UserDto
{
    public string Username { get; set; } = string.Empty;

    public string User { get; set; } = string.Empty;

    public string Sex { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("backghoundimage")]
    public string BackgroundImage { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("like_list")]
    public IEnumerable<VideoDto> LikeList { get; set; } = new List<VideoDto>();
}