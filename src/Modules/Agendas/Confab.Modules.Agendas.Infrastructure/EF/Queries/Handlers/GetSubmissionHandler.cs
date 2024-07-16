using Confab.Modules.Agendas.Application.Submissions.Dto;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers;

internal class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
{
    private readonly DbSet<Submission> _submissions;

    public GetSubmissionHandler(AgendasDbContext context)
    {
        _submissions = context.Submissions;
    }

    public Task<SubmissionDto> HandleAsync(GetSubmission query)
        => _submissions
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Include(x => x.Speakers)
            .Select(x => new SubmissionDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Level = x.Level,
                Tags = x.Tags,
                Status = x.Status,
                Speakers = x.Speakers.Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                })
            })
            .SingleOrDefaultAsync();

}