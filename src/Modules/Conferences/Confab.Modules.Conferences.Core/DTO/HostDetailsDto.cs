namespace Confab.Modules.Conferences.Core.DTO;

public class HostDetailsDto : HostDto
{
    public IEnumerable<ConferenceDto> Conferences { get; set; }
}