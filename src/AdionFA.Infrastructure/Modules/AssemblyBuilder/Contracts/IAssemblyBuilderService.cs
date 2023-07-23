using AdionFA.Infrastructure.AssemblyBuilder.Model;

namespace AdionFA.Infrastructure.AssemblyBuilder.Contracts
{
    public interface IAssemblyBuilderService
    {
        /// <summary>
        /// Resets the Assembly Builder Winning Nodes and gets the Child Nodes from the Strategy Builder.
        /// </summary>
        /// <param name="projectName"></param>
        AssemblyBuilderModel LoadNewAssemblyBuilder(string projectName);

        /// <summary>
        /// Gets the Assembly Builder Winning Nodes found on the directory and the Child Nodes from the Strategy Builder.
        /// </summary>
        /// <param name="projectName"></param>
        AssemblyBuilderModel LoadExistingAssemblyBuilder(string projectName);
    }
}
