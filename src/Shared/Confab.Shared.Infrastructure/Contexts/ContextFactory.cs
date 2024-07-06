using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace Confab.Shared.Infrastructure.Contexts;

internal class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public IContext Create()
    {
        var context = _httpContextAccessor.HttpContext;

        return context is null ? Context.Empty : new Context(context);
    }
}