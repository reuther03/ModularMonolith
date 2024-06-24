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

    public Task<Host> GetAsync(Guid id) => _context.Hosts.Include(x=> x.Conferences).SingleOrDefaultAsync(x => x.Id == id);

    public Task<IReadOnlyList<Host>> BrowseAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Host host)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Host host)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Host host)
    {
        throw new NotImplementedException();
    }
}