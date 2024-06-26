
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Conferences.Api.Controllers;

public class ConferenceController : BaseController
{
    private readonly IConferenceService _conferenceService;

    public ConferenceController(IConferenceService conferenceService)
    {
        _conferenceService = conferenceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConferenceDetailsDto>>> GetAsync()
    {
        var hosts = await _conferenceService.BrowseAsync();
        return Ok(hosts);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConferenceDto>> GetAsync(Guid id)
    {
        var host = await _conferenceService.GetAsync(id);
        return host is null ? NotFound() : Ok(host);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(ConferenceDetailsDto dto)
    {
        await _conferenceService.AddAsync(dto);
        return CreatedAtAction(nameof(GetAsync), new {id = dto.Id}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> PutAsync(Guid id, ConferenceDetailsDto dto)
    {
        dto.Id = id;
        await _conferenceService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _conferenceService.DeleteAsync(id);
        return NoContent();
    }
}