using AdionFA.Infrastructure.CrossingBuilder.Model;

namespace AdionFA.Infrastructure.CrossingBuilder.Contracts
{
    public interface ICrossingBuilderService
    {
        void LoadNewCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder);
        void LoadExistingCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder);
    }
}
