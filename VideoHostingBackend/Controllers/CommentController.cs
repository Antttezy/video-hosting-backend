using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Models.DataTransfer;
using VideoHostingBackend.Core.Services;

namespace VideoHostingBackend.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IUserService _userService;
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;

    public CommentController(IVideoService videoService, IUserService userService, IVideoRepository videoRepository,
        IMapper mapper)
    {
        _videoService = videoService;
        _userService = userService;
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<CommentDto>?>> LoadComments([FromQuery] string video)
    {
        Video? commVideo = await _videoRepository.GetByVideoName(video);

        if (commVideo is null)
        {
            return Error("Error: Video not found");
        }

        var result = _mapper.ProjectTo<CommentDto>(commVideo.Comments!.AsQueryable());

        if (result is null)
        {
            return Error("Error: Could not load comments");
        }

        return result.ToList();
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<CommentDto?>> CreateComment([FromBody] CreateCommentDto createComment)
    {
        UserData? user = await GetUser(User.Identity);

        if (user is null)
        {
            return Error("Error: User Not Found");
        }

        Video? commVideo = await _videoRepository.GetByVideoName(createComment.VideoId);

        if (commVideo is null)
        {
            return Error("Error: Video not found");
        }

        Comment? comment = await _videoService.AddComment(commVideo, createComment.Text, user);

        if (comment is null)
        {
            return Error("Error: Error while creating a comment");
        }

        return _mapper.Map<CommentDto>(comment);
    }

    private async Task<UserData?> GetUser(IIdentity? identity)
    {
        if (identity is not ClaimsIdentity claimsIdentity)
        {
            return null;
        }

        Claim? claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Id");

        if (claim is null)
        {
            return null;
        }

        var id = int.Parse(claim.Value);
        return await _userService.GetById(id);
    }

    private BadRequestObjectResult Error(string message)
    {
        return BadRequest(new
        {
            Error = message
        });
    }
}