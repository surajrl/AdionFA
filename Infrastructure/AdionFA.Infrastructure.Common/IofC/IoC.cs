using AdionFA.Infrastructure.Common.Security.Helper;
using AdionFA.Infrastructure.Common.Security.Model;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System.Linq;

namespace AdionFA.Infrastructure.Common.IofC
{
    public sealed class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public void Load(NinjectModule module)
        {
            Kernel.Load(module);
        }

        public static T Get<T>(string ownerId = null, string owner = null)
        {
            AdionIdentity Identity = SecurityHelper.Identity;

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
            var pr = context?.Request?.ParentRequest;
            while (pr != null)
            {
                var param = pr.Parameters.Any(x => x.Name == name) ? pr.Parameters.Single(x => x.Name == name) : null;
                if (param != null)
                {
                    context?.Request?.Parameters.Add(param);
                    tvalue = param.GetValue(context, context.Request.Target)?.ToString();
                }

                if (string.IsNullOrEmpty(tvalue))
                    pr = pr.ParentRequest;
                else
                    break;
            }

            if (tvalue == null)
                SecurityHelper.IdentityToArguments().TryGetValue(name.ToLower(), out tvalue);

            return tvalue;
        }
    }
}
