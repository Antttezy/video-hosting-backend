using Microsoft.EntityFrameworkCore;
using VideoHostingBackend.Data;

namespace VideoHostingBackend.Util;

internal class MigrationApplier
{
    private readonly VideoContext _context;

    public MigrationApplier(VideoContext context)
    {
        _context = context;
    }

    public void ApplyPending()
    {
        if (_context.Database.GetPendingMigrations().Any())
        {
            _context.Database.Migrate();
        }
    }
}