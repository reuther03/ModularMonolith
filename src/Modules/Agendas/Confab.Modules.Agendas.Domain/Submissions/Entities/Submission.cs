using Confab.Modules.Agendas.Domain.Submissions.Consts;
using Confab.Modules.Agendas.Domain.Submissions.Events;
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
        var submission = new Submission(id, conferenceId);
    }

    public static Submission Create(AggregateId id, ConferenceId conferenceId, string title, string description,
        int level, IEnumerable<string> tags, ICollection<Speaker> speakers)
    {
        var submission = new Submission(id, conferenceId);
        submission.ChangeTitle(title);
        submission.ChangeDescription(description);
        submission.ChangeLevel(level);
        submission.Status = SubmissionStatus.Pending;
        submission.ChangeSpeakers(speakers);
        submission.Tags = tags;
        submission.ClearEvents();
        submission.Version = 0;

        submission.AddEvent(new SubmissionAdded(submission));

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

    public void ChangeSpeakers(IEnumerable<Speaker> speakers)
    {
        if (speakers is null || !speakers.Any())
        {
            throw new ConfabException("Speakers cannot be empty.");
        }

        _speakers = speakers.ToList();
        IncrementVersion();
    }

    public void Approve()
        => ChangeStatus(SubmissionStatus.Approved, SubmissionStatus.Rejected);

    public void Reject()
        => ChangeStatus(SubmissionStatus.Rejected, SubmissionStatus.Approved);

    private void ChangeStatus(string status, string invalidStatus)
    {
        if (Status == invalidStatus)
        {
            throw new ConfabException($"Submission is already {invalidStatus}.");
        }

        Status = status;
        AddEvent(new SubmissionStatusChanged(this, status));
    }
}