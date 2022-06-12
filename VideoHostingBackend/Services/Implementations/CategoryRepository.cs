using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Core.Services;
using VideoHostingBackend.Data;

namespace VideoHostingBackend.Services.Implementations;

internal class CategoryRepository : ICategoryRepository
{
    private readonly VideoContext _context;

    public CategoryRepository(VideoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Videos)
            .ThenInclude(v => v.Country)
            .Include(c => c.Videos)
            .ThenInclude(v => v.Uploader)
            .OrderBy(c => c.Id)
            .ToListAsync();

        foreach (Category category in categories)
        {
            category.Videos = category.Videos.Where(v => v.Uploaded).ToList();
        }

        return categories;
    }

    public async Task<Category?> GetByName(string name)
    {
        Category? category = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Videos)
            .ThenInclude(v => v.Country)
            .Include(c => c.Videos)
            .ThenInclude(v => v.Uploader)
            .FirstOrDefaultAsync(c => c.Name == name);

        if (category is { })
        {
            category.Videos = category.Videos.Where(v => v.Uploaded).ToList();
        }

        return category;
    }
}