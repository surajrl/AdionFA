using MediatR;

namespace AdionFA.Infrastructure.MediatR
{
    public class Jing : IRequest
    {
        public string Message { get; set; }
    }
}
