﻿using System;

namespace AdionFA.Infrastructure.Common.MediatR.ExceptionHandler
{
    public class ConnectionException : Exception { }

    public class ForbiddenException : ConnectionException { }

    public class ResourceNotFoundException : ConnectionException { }

    public class ServerException : Exception { }
}
