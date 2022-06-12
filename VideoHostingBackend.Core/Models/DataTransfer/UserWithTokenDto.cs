using System.Text.Json.Serialization;

namespace VideoHostingBackend.Core.Models.DataTransfer;

public class UserWithTokenDto
{
    private UserDto UserDto { get; }
    
    private AuthResponse AuthResponse { get; }

    public string Username => UserDto.Username;

    public string User => UserDto.User;

    public string Sex => UserDto.Sex;

    public string Id => UserDto.Id;

    [JsonPropertyName("backghoundimage")]
    public string BackgroundImage => UserDto.BackgroundImage;

    public string Avatar => UserDto.Avatar;

    [JsonPropertyName("like_list")]
    public IEnumerable<VideoDto> LikeList => UserDto.LikeList;

    public string Token => AuthResponse.Token;

    public UserWithTokenDto(AuthResponse authResponse, UserDto userDto)
    {
        UserDto = userDto;
        AuthResponse = authResponse;
    }
}