using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace Confab.Shared.Infrastructure.Contexts;

public class Context : IContext
{
    public string RequestId { get; } = Guid.NewGuid().ToString("N");
    public string TraceId { get; }
    public IIdentityContext Identity { get; }

    public Context()
    {

    }

    public Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
    {
    }

    internal Context(string traceId, IIdentityContext identity)
    {
        TraceId = traceId;
        Identity = identity;
    }

    public static IContext Empty => new Context();
}