using AdionFA.Infrastructure.CrossingBuilder.Model;

namespace AdionFA.Infrastructure.CrossingBuilder.Contracts
{
    public interface ICrossingBuilderService
    {
        CrossingBuilderModel GetExistingCrossingBuilder(string projectName);
    }
}
