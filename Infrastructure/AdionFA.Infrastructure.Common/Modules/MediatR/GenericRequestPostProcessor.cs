﻿using MediatR.Pipeline;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AdionFA.Infrastructure.Common.MediatR
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly TextWriter _writer;

        public GenericRequestPostProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("- All Done");
        }
    }
}
