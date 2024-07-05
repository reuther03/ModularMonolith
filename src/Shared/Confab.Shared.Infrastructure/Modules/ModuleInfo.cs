namespace Confab.Shared.Infrastructure.Modules;

public record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);