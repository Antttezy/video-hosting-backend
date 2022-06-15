using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Services;
using VideoHostingBackend.Data;
using VideoHostingBackend.Util;

namespace VideoHostingBackend.Services.Implementations;

public class UserService : IUserService
{
    private readonly VideoContext _context;

    public UserService(VideoContext context)
    {
        _context = context;
    }

    public async Task<UserData> RegisterUser(string login, string password)
    {
        UserData? search = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == login);

        if (search is { })
        {
            throw new ArgumentException("Login must be unique");
        }

        UserData user = new()
        {
            User = login,
            Username = login,
            Sex = "male",
            Credentials = new()
            {
                Password = PasswordHasher.HashPassword(password)
            },
            Avatar = RandomStringGenerator.GenerateName() + ".png",
            BackgroundImage = RandomStringGenerator.GenerateName() + ".png"
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserData?> LoginUser(string login, string password)
    {
        UserData? user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Credentials)
            .FirstOrDefaultAsync(u => u.Username == login);

        if (user is null)
        {
            return null;
        }

        var expectedHash = PasswordHasher.HashPassword(password);

        return expectedHash == user.Credentials?.Password ? user : null;
    }

    public async Task<UserData?> GetById(int id)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.LikedVideos)
            .Include(u => u.Comments)
            .Include(u => u.Credentials)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserData?> SetUserData(UserData newData)
    {
        try
        {
            _context.Update(newData);
            await _context.SaveChangesAsync();
            return newData;
        }
        catch (DbUpdateException)
        {
            return null;
        }
    }
}