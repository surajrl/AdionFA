
namespace Adion.FA.Infrastructure.Common.MediatR.ExceptionHandler
{
    public class PingResource : Ping { }

    public class PingNewResource : Ping { }

    public class PingResourceTimeout : PingResource { }

    public class PingProtectedResource : PingResource { }
}
