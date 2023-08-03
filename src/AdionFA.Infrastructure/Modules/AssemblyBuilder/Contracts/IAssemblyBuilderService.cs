using AdionFA.Infrastructure.AssemblyBuilder.Model;

namespace AdionFA.Infrastructure.AssemblyBuilder.Contracts
{
    public interface IAssemblyBuilderService
    {
        /// <summary>
        /// Creates a new Assembly Builder and gets the child nodes from the Strategy Builder.
        /// </summary>
        /// <param name="projectName"></param>
        AssemblyBuilderModel CreateNewAssemblyBuilder(string projectName);

        /// <summary>
        /// Gets the Assembly Builder winning nodes found on the directory and the child nodes from the Strategy Builder.
        /// </summary>
        /// <param name="projectName"></param>
        AssemblyBuilderModel GetExistingAssemblyBuilder(string projectName);
    }
}
