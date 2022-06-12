using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoHostingBackend.Core.Models.DataTransfer;
using VideoHostingBackend.Core.Services;

namespace VideoHostingBackend.Controllers;

[ApiController]
[Route("content")]
public class ContentController: ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;

    public ContentController(ICategoryRepository categoryRepository, IVideoRepository videoRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    [HttpGet("categories")]
    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        var categories = await _categoryRepository.GetCategories();
        return _mapper.ProjectTo<CategoryDto>(categories.AsQueryable());
    }
    
    [HttpGet("videos")]
    public async Task<IEnumerable<VideoDto>> GetVideos()
    {
        var categories = await _videoRepository.GetVideosInCategory("Млекопитающие");
        return _mapper.ProjectTo<VideoDto>(categories.AsQueryable());
    }
}