using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities;

public sealed class Submission : AggregateRoot
{
    public ConferenceId ConferenceId { get; private set; }
    public string title { get; private set; }
    public string Description { get; private set; }
    public int Level { get; private set; }
    public string Status { get; private set; }
    public IEnumerable<string> Tags { get; private set; }
}