﻿namespace Confab.Modules.Agendas.Domain.Submissions.Consts;

public static class SubmissionStatus
{
    public const string Pending = nameof(Pending);
    public const string Approved = nameof(Approved);
    public const string Rejected = nameof(Rejected);

    public static bool IsValid(string status)
        => status is Pending or Approved or Rejected;
}