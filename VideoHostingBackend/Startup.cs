using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Mapping;
using VideoHostingBackend.Core.Services;
using VideoHostingBackend.Data;
using VideoHostingBackend.Services.Implementations;
using VideoHostingBackend.Util;

namespace VideoHostingBackend;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddDbContext<VideoContext>(builder =>
            builder.UseNpgsql(_configuration.GetConnectionString("VideoContext")));

        services.AddAutoMapper(config =>
        {
            config.AddProfile<VideoMappingProfile>();
        });

        services.AddTransient<MigrationApplier>();
        services.AddTransient<DatabaseSeeder>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IUserService, UserService>();

        services.AddCors(setup =>
        {
            setup.AddDefaultPolicy(policy => policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(builder =>
        {
            builder.RequireHttpsMetadata = true;

            builder.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthenticationOptions.GetSecurityKey()
            };
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }
}