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
[Route("api/auth")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileUploadService;

    public UserController(IUserService userService, ITokenGenerator tokenGenerator, IMapper mapper,
        IFileUploadService fileUploadService)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
    }

    [HttpPost("signup")]
    public async Task<UserWithTokenDto?> SignUp([FromBody] LoginDto login)
    {
        try
        {
            UserData user = await _userService.RegisterUser(login.Login, login.Password);
            var token = await _tokenGenerator.GenerateToken(user.Credentials!);

            AuthResponse auth = new()
            {
                Token = token
            };

            UserDto userDto = _mapper.Map<UserDto>(user);
            return new(auth, userDto);
        }
        catch (Exception)
        {
            HttpContext.Response.StatusCode = 400;
            return null;
        }
    }

    [HttpPost("signin")]
    public async Task<UserWithTokenDto?> SignIn([FromBody] LoginDto login)
    {
        try
        {
            UserData? user = await _userService.LoginUser(login.Login, login.Password);

            if (user is null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }

            var token = await _tokenGenerator.GenerateToken(user.Credentials!);

            AuthResponse auth = new()
            {
                Token = token
            };

            UserDto userDto = _mapper.Map<UserDto>(user);
            return new(auth, userDto);
        }
        catch (Exception)
        {
            HttpContext.Response.StatusCode = 400;
            return null;
        }
    }

    [HttpGet]
    [Authorize]
    [Route("/api/user")]
    public async Task<UserDto?> GetUser()
    {
        try
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }

            Claim? claim = identity.Claims.FirstOrDefault(c => c.Type == "Id");

            if (claim is null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }

            var id = int.Parse(claim.Value);
            UserData? user = await _userService.GetById(id);

            if (user is null)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            HttpContext.Response.StatusCode = 400;
            return null;
        }
    }

    [HttpPost]
    [Authorize]
    [Route("/api/user/setAvatar")]
    public async Task<ActionResult<UserDto?>> SetAvatar([FromForm] FileDto avatar)
    {
        UserData? user = await GetUser(User.Identity);

        if (user is null)
        {
            return Error("Error: User not found");
        }

        try
        {
            await _fileUploadService.UploadImage(avatar.File, user.Avatar);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            return Error("Error while uploading photo");
        }
    }

    [HttpPost]
    [Authorize]
    [Route("/api/user/setBackground")]
    public async Task<ActionResult<UserDto?>> SetBackground([FromForm] FileDto background)
    {
        UserData? user = await GetUser(User.Identity);

        if (user is null)
        {
            return Error("Error: User not found");
        }

        try
        {
            await _fileUploadService.UploadImage(background.File, user.BackgroundImage);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception)
        {
            return Error("Error while uploading photo");
        }
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