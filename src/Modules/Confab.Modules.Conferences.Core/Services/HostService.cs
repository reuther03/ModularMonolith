using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;

namespace Confab.Modules.Conferences.Core.Services;

internal class HostService : IHostService
{
    private readonly IHostRepository _hostRepository;

    public HostService(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task AddAsync(HostDto dto)
    {
        dto.Id = Guid.NewGuid();
        await _hostRepository.AddAsync(new Host
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        });
    }

    public async Task<HostDetailsDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<HostDto>> BrowseAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(HostDetailsDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}