using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Models.DataTransfer;
using VideoHostingBackend.Core.Services;

namespace VideoHostingBackend.Controllers;

[Route("video")]
public class VideoController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IUserService _userService;
    private readonly IVideoService _videoService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IVideoRepository _videoRepository;
    private readonly IFileUploadService _fileUploadService;

    public VideoController(IWebHostEnvironment environment, IUserService userService, IVideoService videoService,
        ICategoryRepository categoryRepository, IMapper mapper, IVideoRepository videoRepository,
        IFileUploadService fileUploadService)
    {
        _environment = environment;
        _userService = userService;
        _videoService = videoService;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _videoRepository = videoRepository;
        _fileUploadService = fileUploadService;
    }

    [HttpGet("{name}")]
    public IActionResult GetVideoById([FromRoute] string name)
    {
        var path = Path.Combine(_environment.WebRootPath, "videos", name);

        try
        {
            FileStream stream = System.IO.File.OpenRead(path);
            return File(stream, "application/octet-stream", enableRangeProcessing: true);
        }
        catch (IOException)
        {
            return NotFound();
        }
    }

    [HttpPost("upload")]
    [Authorize]
    public async Task<ActionResult<VideoDto?>> UploadVideo([FromBody] UploadDto upload)
    {
        Category? category = await _categoryRepository.GetByName(upload.Category);
        UserData? user = await GetUser(User.Identity);
        var name = upload.VideoName;

        if (category is null)
        {
            return Error("Category not found");
        }

        if (user is null)
        {
            return Error("User not found");
        }

        Video? video = await _videoService.CreateVideo(user, name, category);

        if (video is null)
        {
            return Error("Video not created");
        }

        return _mapper.Map<VideoDto>(video);
    }

    [HttpPost("setCover")]
    [Authorize]
    public async Task<ActionResult<VideoDto?>> UploadVideoCover([FromForm] FileDto upload)
    {
        UserData? user = await GetUser(User.Identity);
        Video? video = await _videoRepository.GetByCoverName(upload.VideoFile);

        if (user is null)
        {
            return Error("User not found");
        }

        if (video is null)
        {
            return Error("Video not found");
        }

        if (video.Uploader.Id != user.Id)
        {
            return Error("Video is not owned by user");
        }

        try
        {
            await _fileUploadService.UploadImage(upload.File, video.CoverImg);
            return _mapper.Map<VideoDto>(video);
        }
        catch (Exception)
        {
            return Error("Error while uploading photo");
        }
    }

    [HttpPost("setVideo")]
    [RequestFormLimits(MultipartBodyLengthLimit = 1073741824L)]
    [RequestSizeLimit(1073741824L)]
    [Authorize]
    public async Task<ActionResult<VideoDto?>> UploadVideoFile([FromForm] FileDto upload)
    {
        UserData? user = await GetUser(User.Identity);
        Video? video = await _videoRepository.GetByVideoName(upload.VideoFile);

        if (user is null)
        {
            return Error("User not found");
        }

        if (video is null)
        {
            return Error("Video not found");
        }

        if (video.Uploader.Id != user.Id)
        {
            return Error("Video is not owned by user");
        }

        try
        {
            await _fileUploadService.UploadVideo(upload.File, video.VideoId);
            return _mapper.Map<VideoDto>(video);
        }
        catch (Exception)
        {
            return Error("Error while uploading photo");
        }
    }

    [HttpPost("reveal/{videoId}")]
    [Authorize]
    public async Task<ActionResult<VideoDto?>> RevealVideo([FromRoute] string videoId)
    {
        UserData? user = await GetUser(User.Identity);
        Video? video = await _videoRepository.GetByVideoName(videoId);

        if (user is null)
        {
            return Error("User not found");
        }

        if (video is null)
        {
            return Error("Video not found");
        }

        if (video.Uploader.Id != user.Id)
        {
            return Error("Video is not owned by user");
        }

        Video? revealed = await _videoService.RevealVideo(video);

        if (revealed is null)
        {
            return Error("Could not reveal the video");
        }

        return _mapper.Map<VideoDto>(revealed);
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