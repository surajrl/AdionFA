﻿using MediatR.Pipeline;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.MediatR
{
    public class ConstrainedRequestPostProcessor<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : Ping
    {
        private readonly TextWriter _writer;

        public ConstrainedRequestPostProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("- All Done with Ping");
        }
    }
}
