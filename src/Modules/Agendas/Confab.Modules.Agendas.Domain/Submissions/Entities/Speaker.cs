﻿using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities;

public class Speaker : AggregateRoot
{
    public string FullName { get; init; }

    public IEnumerable<Submission> Submissions => _submissions;

    private ICollection<Submission> _submissions;
    public Speaker(AggregateId id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public static Speaker Create(AggregateId id, string fullName)
        => new Speaker(id, fullName);
}