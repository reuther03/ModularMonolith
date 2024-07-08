using System.Threading.Tasks;
using Confab.Modules.Conferences.Messages.Events;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Tickets.Core.Events.External.Handlers;

internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
{
    private readonly IConferenceRepository _conferenceRepository;

    public ConferenceCreatedHandler(IConferenceRepository conferenceRepository)
    {
        _conferenceRepository = conferenceRepository;
    }

    public async Task HandleAsync(ConferenceCreated @event)
    {
        var conference = new Conference
        {
            Id = @event.Id,
            Name = @event.Name,
            ParticipantsLimit = @event.ParticipantsLimit,
            From = @event.From,
            To = @event.To
        };

        await _conferenceRepository.AddAsync(conference);
    }
}