using AdionFA.Infrastructure.Common.Security.Helper;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System.Linq;

namespace AdionFA.Infrastructure.Common.IofC
{
    public sealed class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Load(NinjectModule module)
        {
            Kernel.Load(module);
        }

        public static T Get<T>(string ownerId = null, string owner = null)
        {
            var Identity = SecurityHelper.Identity;

            var parameters = new Ninject.Parameters.ConstructorArgument[2]
            {
                new Ninject.Parameters.ConstructorArgument("ownerId", ownerId ?? Identity?.OwnerId),
                new Ninject.Parameters.ConstructorArgument("owner", owner ?? Identity?.Owner)
            };

            return Kernel.Get<T>(parameters);
        }

        public static string GetArgument(IContext context, string name)
        {
            string tvalue = null;

            var parentRequest = context?.Request?.ParentRequest;
            while (parentRequest != null)
            {
                var param = parentRequest.Parameters
                    .Any(x => x.Name == name)
                    ? parentRequest.Parameters.Single(x => x.Name == name)
                    : null;

                if (param != null)
                {
                    context?.Request?.Parameters.Add(param);
                    tvalue = param.GetValue(context, context.Request.Target)?.ToString();
                }

                if (string.IsNullOrEmpty(tvalue))
                {
                    parentRequest = parentRequest.ParentRequest;
                }
                else
                {
                    break;
                }
            }

            if (tvalue == null)
            {
                SecurityHelper.IdentityToArguments().TryGetValue(name.ToLower(), out tvalue);
            }

            return tvalue;
        }
    }
}
