using MediatR;

namespace AdionFA.Infrastructure.Common.MediatR
{
    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }
}
