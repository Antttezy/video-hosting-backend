using VideoHostingBackend.Core.Models;

namespace VideoHostingBackend.Core.Services;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();

    Task<Category?> GetByName(string name);
}