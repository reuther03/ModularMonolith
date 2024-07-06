namespace Confab.Shared.Abstractions.Contexts;

public interface IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    string Role { get; }
    Dictionary<string, IEnumerable<string>> Claims { get; }
}