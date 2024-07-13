using Confab.Modules.Agendas.Domain.Submissions.Consts;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities;

public sealed class Submission : AggregateRoot
{
    public ConferenceId ConferenceId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Level { get; private set; }
    public string Status { get; private set; }
    public IEnumerable<string> Tags { get; private set; }
    public IEnumerable<Speaker> Speakers => _speakers;

    private ICollection<Speaker> _speakers = new List<Speaker>();

    public Submission(AggregateId id, ConferenceId conferenceId, string title, string description, int level, string status,
        IEnumerable<string> tags, ICollection<Speaker> speakers, int version = 0) : this(id, conferenceId)
    {
        Id = id;
        ConferenceId = conferenceId;
        Title = title;
        Description = description;
        Level = level;
        Status = status;
        Tags = tags;
        _speakers = speakers;
        Version = version;
    }

    public Submission(AggregateId id, ConferenceId conferenceId)
    {
        Id = id;
        ConferenceId = conferenceId;
    }

    public static Submission Create(AggregateId id, ConferenceId conferenceId, string title, string description, int level,
        string status, IEnumerable<string> tags, ICollection<Speaker> speakers)
    {
        var submission = new Submission(id, conferenceId);

        return submission;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ConfabException("Title cannot be empty.");
        }

        Title = title;
        IncrementVersion();
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ConfabException("Description cannot be empty.");
        }

        Description = description;
        IncrementVersion();
    }

    public void ChangeLevel(int level)
    {
        if (IsNotInRage())
        {
            throw new ConfabException("Level must be between 0 and 6.");
        }

        Level = level;
        IncrementVersion();
        return;

        bool IsNotInRage() => level is < 0 or > 6;
    }

    public void Approve()
    {
        if (Status is SubmissionStatus.Rejected)
        {
            throw new ConfabException("Submission is already rejected.");
        }

        Status = SubmissionStatus.Approved;
        IncrementVersion();
    }

    public void Reject()
    {
        if (Status is SubmissionStatus.Approved)
        {
            throw new ConfabException("Submission is already approved.");
        }

        Status = SubmissionStatus.Rejected;
        IncrementVersion();
    }


}