using Confab.Modules.Agendas.Application.Submissions.Dto;
using Confab.Shared.Abstractions.Queries;

namespace Confab.Modules.Agendas.Application.Submissions.Queries;

public record GetSubmission(Guid Id) : IQuery<SubmissionDto>
{

}