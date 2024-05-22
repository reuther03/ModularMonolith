using Confab.Modules.Conferences.Core.DTO;

namespace Confab.Modules.Conferences.Core.Services;

internal interface IConferenceService
{
    Task<ConferenceDetailsDto> GetAsync(Guid id);
    Task<IEnumerable<ConferenceDto>> BrowseAsync();
    Task AddAsync(ConferenceDetailsDto dto);
    Task UpdateAsync(ConferenceDetailsDto dto);
    Task DeleteAsync(Guid id);
}