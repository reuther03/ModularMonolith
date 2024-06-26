﻿using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories;

internal class ConferenceInMemoryRepository : IConferenceRepository
{
    private readonly List<Conference> _conferences = [];

    public Task<Conference> GetAsync(Guid id)
        => Task.FromResult(_conferences.SingleOrDefault(x => x.Id == id));

    public async Task<IReadOnlyList<Conference>> BrowseAsync()
    {
        await Task.CompletedTask;
        return _conferences;
    }

    public Task AddAsync(Conference host)
    {
        _conferences.Add(host);
        return Task.CompletedTask;

    }
    public Task UpdateAsync(Conference host)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Conference host)
    {
        _conferences.Remove(host);
        return Task.CompletedTask;
    }
}