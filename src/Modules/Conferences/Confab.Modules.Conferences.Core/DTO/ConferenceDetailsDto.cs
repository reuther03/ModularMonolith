using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Conferences.Core.DTO;

public class ConferenceDetailsDto : ConferenceDto
{
    [Required]
    public string Description { get; set; }
}