using AdionFA.Infrastructure.Common.CrossingBuilder.Model;

namespace AdionFA.Infrastructure.Common.CrossingBuilder.Contracts
{
    public interface ICrossingBuilderService
    {
        void LoadNewCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder);
        void LoadExistingCrossingBuilder(string projectName, CrossingBuilderModel crossingBuilder);
    }
}
