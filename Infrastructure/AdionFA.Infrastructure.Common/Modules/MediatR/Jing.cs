using MediatR;

namespace AdionFA.Infrastructure.Common.MediatR
{
    public class Jing : IRequest
    {
        public string Message { get; set; }
    }
}
