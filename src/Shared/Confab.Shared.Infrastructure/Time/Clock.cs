using Confab.Shared.Abstractions;

namespace Confab.Shared.Infrastructure.Services;

public class Clock : IClock
{
    public DateTime CurrentDate()
    {
        return DateTime.UtcNow;
    }
}