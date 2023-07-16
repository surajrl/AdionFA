using AdionFA.Infrastructure.IofC;
using AutoMapper;
using Ninject;

namespace AdionFA.Infrastructure.Services
{
    public class AppServiceBase
    {
        protected const string Username = "admin";
        protected const string Id = "0";

        protected static IMapper Mapper => IoC.Kernel.Get<IMapper>();
    }
}
