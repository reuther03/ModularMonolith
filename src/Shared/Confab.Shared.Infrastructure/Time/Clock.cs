using Confab.Shared.Abstractions;

namespace Confab.Shared.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime CurrentDate()
    {
        return DateTime.UtcNow;
    }
}