﻿using System.Collections.Concurrent;
using System.Net;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new();


    public ExceptionResponse Map(Exception exception)
        => exception switch
        {
            ConfabException ex => new ExceptionResponse(new ErrorsResponse(new Error("code", ex.Message)), HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error")), HttpStatusCode.InternalServerError)
        };

    private record Error(string Code, string Message);

    private record ErrorsResponse(params Error[] Error);
}