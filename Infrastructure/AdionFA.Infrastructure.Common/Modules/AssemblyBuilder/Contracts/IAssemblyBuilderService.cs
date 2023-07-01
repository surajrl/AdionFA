using AdionFA.Infrastructure.Common.AssemblyBuilder.Model;

namespace AdionFA.Infrastructure.Common.AssemblyBuilder.Contracts
{
    public interface IAssemblyBuilderService
    {
        AssemblyBuilderModel LoadAssemblyBuilder(string projectName);
    }
}
