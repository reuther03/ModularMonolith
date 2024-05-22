using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories;

public interface IConferenceRepository
{
    Task<Conference> GetAsync(Guid id);
    Task<IEnumerable<Conference>> BrowseAsync();
    Task AddAsync(Conference conference);
    Task UpdateAsync(Conference conference);
    Task DeleteAsync(Conference conference);
}