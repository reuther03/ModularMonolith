using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions;

namespace Confab.Modules.Conferences.Core.Policies;

internal class ConferenceDeletionPolicy : IConferenceDeletionPolicy
{
    private readonly IClock _clock;

    public ConferenceDeletionPolicy(IClock clock)
    {
        _clock = clock;
    }

    public async Task<bool> CanDeleteAsync(Conference conference)
    {
        var canDelete = _clock.CurrentDate().Date.AddDays(7) < conference.From;

        return canDelete;
    }
}