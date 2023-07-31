using AdionFA.Infrastructure.IofC;
using AutoMapper;
using Ninject;
using Serilog;

namespace AdionFA.Infrastructure.Services
{
    public class AppServiceBase
    {
        public AppServiceBase()
        {
            Logger = IoC.Kernel.Get<ILogger>();
            Mapper = IoC.Kernel.Get<IMapper>();
        }

        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }
    }
}
