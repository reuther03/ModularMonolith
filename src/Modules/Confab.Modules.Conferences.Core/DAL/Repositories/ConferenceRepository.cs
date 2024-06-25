using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;

internal class ConferenceRepository : IConferenceRepository
{
    private readonly ConferencesDbContext _context;

    public ConferenceRepository(ConferencesDbContext context)
    {
        _context = context;
    }

    public Task<Conference> GetAsync(Guid id) => _context.Conferences.Include(x => x.Host).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Conference>> BrowseAsync() => await _context.Conferences.ToListAsync();

    public async Task AddAsync(Conference conference)
    {
        await _context.Conferences.AddAsync(conference);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Conference conference)
    {
        _context.Conferences.Update(conference);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        _context.Conferences.Remove(conference);
        await _context.SaveChangesAsync();
    }
}