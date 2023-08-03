using AdionFA.Infrastructure.CrossingBuilder.Model;

namespace AdionFA.Infrastructure.CrossingBuilder.Contracts
{
    public interface ICrossingBuilderService
    {
        CrossingBuilderModel CreateNewCrossingBuilder(string projectName);

        CrossingBuilderModel GetExistingCrossingBuilder(string projectName);
    }
}
