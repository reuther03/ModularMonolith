using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Messages.Events;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Modules.Conferences.Core.Services;

internal class ConferenceService : IConferenceService
{
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;
    private readonly IMessageBroker _messageBroker;

    public ConferenceService(IConferenceRepository conferenceRepository, IHostRepository hostRepository, IConferenceDeletionPolicy conferenceDeletionPolicy, IMessageBroker messageBroker)
    {
        _conferenceRepository = conferenceRepository;
        _hostRepository = hostRepository;
        _conferenceDeletionPolicy = conferenceDeletionPolicy;
        _messageBroker = messageBroker;
    }

    public async Task<ConferenceDetailsDto> GetAsync(Guid id)
    {
        var conference = await _conferenceRepository.GetAsync(id);
        if (conference is null)
        {
            return null;
        }

        var dto = new ConferenceDetailsDto
        {
            Id = conference.Id,
            HostId = conference.HostId,
            Name = conference.Name,
            Description = conference.Description,
            Location = conference.Location,
            LogoUrl = conference.LogoUrl,
            ParticipantsLimit = conference.ParticipantsLimit,
            From = conference.From,
            To = conference.To
        };

        return dto;
    }

    public async Task<IEnumerable<ConferenceDto>> BrowseAsync()
    {
        var conferences = await _conferenceRepository.BrowseAsync();
        return conferences.Select(x => new ConferenceDto
        {
            Id = x.Id,
            HostId = x.HostId,
            Name = x.Name,
            Location = x.Location,
            LogoUrl = x.LogoUrl,
            ParticipantsLimit = x.ParticipantsLimit,
            From = x.From,
            To = x.To
        }).ToList();
    }

    public async Task AddAsync(ConferenceDetailsDto dto)
    {
        if (await _hostRepository.GetAsync(dto.HostId) is null)
        {
            throw new HostNotFoundException(dto.HostId);
        }

        dto.Id = Guid.NewGuid();
        await _conferenceRepository.AddAsync(new Conference(dto.Id,
            dto.HostId,
            dto.Name,
            dto.Description,
            dto.Location,
            dto.LogoUrl,
            dto.ParticipantsLimit,
            dto.From, dto.To
        ));

        await _messageBroker.PublishAsync(new ConferenceCreated(dto.Id, dto.Name, dto.ParticipantsLimit, dto.From, dto.To));
    }

    public async Task UpdateAsync(ConferenceDetailsDto dto)
    {
        var conference = await _conferenceRepository.GetAsync(dto.Id);
        if (conference is null)
        {
            throw new Exception($"Conference with ID: '{dto.Id}' was not found.");
        }

        conference.Name = dto.Name;
        conference.Description = dto.Description;
        conference.Location = dto.Location;
        conference.LogoUrl = dto.LogoUrl;
        conference.From = dto.From;
        conference.To = dto.To;
        conference.ParticipantsLimit = dto.ParticipantsLimit;
        await _conferenceRepository.UpdateAsync(conference);
    }

    public async Task DeleteAsync(Guid id)
    {
        var conference = await _conferenceRepository.GetAsync(id);
        if (conference is null)
        {
            throw new Exception($"Conference with ID: '{id}' was not found.");
        }

        if (await _conferenceDeletionPolicy.CanDeleteAsync(conference))
        {
            throw new Exception($"Conference with ID: '{id}' cannot be deleted.");
        }


        await _conferenceRepository.DeleteAsync(conference);
    }

    private static T Map<T>(Conference conference) where T : ConferenceDto, new()
    {
        return new T
        {
            Id = conference.Id,
            HostId = conference.HostId,
            Name = conference.Name,
            Location = conference.Location,
            LogoUrl = conference.LogoUrl,
            ParticipantsLimit = conference.ParticipantsLimit,
            From = conference.From,
            To = conference.To
        };
    }
}