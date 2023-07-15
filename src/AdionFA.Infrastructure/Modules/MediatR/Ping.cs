using MediatR;

namespace AdionFA.Infrastructure.MediatR
{
    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }
}
