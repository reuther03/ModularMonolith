using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
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
        var host = await _hostRepository.GetAsync(dto.Id);
        if (host is null)
        {
            throw new HostNotFoundException(dto.Id);
        }

        host.Name = dto.Name;
        host.Description = dto.Description;
        await _hostRepository.UpdateAsync(host);
    }

    public async Task DeleteAsync(Guid id)
    {
        var host = await _hostRepository.GetAsync(id);
        if (host is null)
        {
            throw new HostNotFoundException(id);
        }

        await _hostRepository.DeleteAsync(host);
    }
}