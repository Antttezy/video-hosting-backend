using System.Security.Claims;
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

    public UserController(IUserService userService, ITokenGenerator tokenGenerator, IMapper mapper)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
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
}