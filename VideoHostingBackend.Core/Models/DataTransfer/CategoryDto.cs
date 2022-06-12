namespace VideoHostingBackend.Core.Models.DataTransfer;

public class CategoryDto
{
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public IEnumerable<VideoDto>? Videos { get; set; }
}