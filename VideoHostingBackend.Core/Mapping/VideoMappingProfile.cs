using AutoMapper;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Models.DataTransfer;

namespace VideoHostingBackend.Core.Mapping;

public class VideoMappingProfile : Profile
{
    public VideoMappingProfile()
    {
        CreateMap<Country, CountryDto>()
            .ForMember(s => s.Id, o =>
                o.MapFrom(c => c.Id))
            .ForMember(s => s.Name, o =>
                o.MapFrom(c => c.Name));

        CreateMap<Category, CategoryDto>()
            .ForMember(s => s.Id, o =>
                o.MapFrom(c => c.Id))
            .ForMember(s => s.Name, o =>
                o.MapFrom(c => c.LocalizedName))
            .ForMember(s => s.Videos, o =>
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                o.PreCondition(c => c.Videos is { });
                o.MapFrom(c => c.Videos);
            });

        CreateMap<Comment, CommentDto>()
            .ForMember(s => s.UserId, o =>
                o.MapFrom(c => c.UserId))
            .ForMember(s => s.Name, o =>
                o.MapFrom(c => c.User.Username))
            .ForMember(s => s.Text, o =>
                o.MapFrom(c => c.Text));

        CreateMap<UserData, UserDto>()
            .ForMember(s => s.Id, o =>
                o.MapFrom(u => u.Id.ToString()))
            .ForMember(s => s.Sex, o =>
                o.MapFrom(u => u.Sex))
            .ForMember(s => s.User, o =>
                o.MapFrom(u => u.User))
            .ForMember(s => s.Username, o =>
                o.MapFrom(u => u.Username))
            .ForMember(s => s.Avatar, o =>
                o.MapFrom(u => u.Avatar))
            .ForMember(s => s.BackgroundImage, o =>
                o.MapFrom(u => u.BackgroundImage))
            .ForMember(s => s.LikeList, o =>
                o.MapFrom(u => u.LikedVideos.Select(l => l.Video)));

        CreateMap<Video, VideoDto>()
            .ForMember(s => s.Animal, o =>
                o.MapFrom(v => v.Category.LocalizedName))
            .ForMember(s => s.Comments, o =>
            {
                o.MapFrom(v => v.Comments ?? new List<Comment>());
            })
            .ForMember(s => s.Country, o =>
                o.MapFrom(v => v.Country))
            .ForMember(s => s.LikeAmount, o =>
            {
                o.MapFrom(v => v.Likes.Count.ToString());
            })
            .ForMember(s => s.UploaderName, o =>
                o.MapFrom(v => v.Uploader.Username))
            .ForMember(s => s.VideoId, o =>
                o.MapFrom(v => v.VideoId))
            .ForMember(s => s.VideoName, o =>
                o.MapFrom(v => v.VideoName))
            .ForMember(v => v.ViewsAmount, o =>
                o.MapFrom(_ => "15"))
            .ForMember(v => v.CoverImg, o =>
                o.MapFrom(v => v.CoverImg));
    }
}