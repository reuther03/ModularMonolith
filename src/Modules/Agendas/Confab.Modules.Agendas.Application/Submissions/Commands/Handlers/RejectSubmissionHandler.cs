using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers;

public sealed class RejectSubmissionHandler : ICommandHandler<RejectSubmission>
{
    private readonly ISubmissionRepository _submissionRepository;

    public RejectSubmissionHandler(ISubmissionRepository submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }

    public async Task HandleAsync(RejectSubmission command)
    {
        var submission = await _submissionRepository.GetAsync(command.Id);

        if(submission is null)
        {
            throw new ConfabException("Submission not found.");
        }

        submission.Reject();

        await _submissionRepository.UpdateAsync(submission);
    }
}