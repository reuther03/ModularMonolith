using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;

internal class HostRepository : IHostRepository
{
    private readonly ConferencesDbContext _context;

    public HostRepository(ConferencesDbContext context)
    {
        _context = context;
    }

    public Task<Host?> GetAsync(Guid id) => _context.Hosts.Include(x => x.Conferences).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Host>> BrowseAsync() => await _context.Hosts.ToListAsync();

    public async Task AddAsync(Host host)
    {
        await _context.Hosts.AddAsync(host);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Host host)
    {
        _context.Hosts.Update(host);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Host host)
    {
        _context.Hosts.Remove(host);
        await _context.SaveChangesAsync();
    }
}