using Microsoft.AspNetCore.Mvc;

namespace VideoHostingBackend.Controllers;

[Route("thumbnail")]
public class ThumbnailController: Controller
{
    private readonly IWebHostEnvironment _environment;

    public ThumbnailController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet("{name}")]
    public IActionResult GetThumbnail([FromRoute] string name)
    {
        var path = Path.Combine(_environment.WebRootPath, "thumbnails", name);
        
        try
        {
            FileStream stream = System.IO.File.OpenRead(path);
            return File(stream, "image/png", enableRangeProcessing: false);
        }
        catch (IOException)
        {
            return NotFound();
        }
    }
}