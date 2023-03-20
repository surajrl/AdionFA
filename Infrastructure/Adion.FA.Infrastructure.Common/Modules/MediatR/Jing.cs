using MediatR;

namespace Adion.FA.Infrastructure.Common.MediatR
{
    public class Jing : IRequest
    {
        public string Message { get; set; }
    }
}
