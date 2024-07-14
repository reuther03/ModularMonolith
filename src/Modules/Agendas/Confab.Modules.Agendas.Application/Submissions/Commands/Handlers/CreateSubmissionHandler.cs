﻿using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers;

public sealed class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;
    private readonly ISpeakerRepository _speakerRepository;

    public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository)
    {
        _submissionRepository = submissionRepository;
        _speakerRepository = speakerRepository;
    }

    public async Task HandleAsync(CreateSubmission command)
    {
        var speakerIds = command.SpeakerIds.Select(x => new AggregateId(x));
        var speakers = await _speakerRepository.BrowseAsync(speakerIds);

        if (speakers.Count() != speakerIds.Count())
        {
            throw new ConfabException("Speakers not found.");
        }

        var submission = Submission.Create(
            command.Id,
            command.ConferenceId,
            command.Title,
            command.Description,
            command.Level,
            command.Tags,
            speakers.ToList());

        await _submissionRepository.AddAsync(submission);
    }
}