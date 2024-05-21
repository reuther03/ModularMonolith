using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

public class HostController : BaseController
{
    private readonly IHostService _hostService;

    public HostController(IHostService hostService)
    {
        _hostService = hostService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HostDto>>> GetAsync()
    {
        var hosts = await _hostService.BrowseAsync();
        return Ok(hosts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HostDto>> GetAsync(Guid id)
    {
        var host = await _hostService.GetAsync(id);
        return host is null ? NotFound() : Ok(host);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(HostDto dto)
    {
        await _hostService.AddAsync(dto);
        return CreatedAtAction(nameof(GetAsync), new {id = dto.Id}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> PutAsync(Guid id, HostDetailsDto dto)
    {
        dto.Id = id;
        await _hostService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _hostService.DeleteAsync(id);
        return NoContent();
    }
}