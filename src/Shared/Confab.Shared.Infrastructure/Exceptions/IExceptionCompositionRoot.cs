using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}