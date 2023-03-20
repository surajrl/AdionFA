using MediatR;

namespace Adion.FA.Infrastructure.Common.MediatR
{
    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }
}
