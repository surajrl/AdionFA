using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum ProjectStepEnum
    {
        [Metadata("InitialConfiguration", "InitialConfiguration", resourceType:typeof(ProjectStepMetadataResources))]
        InitialConfiguration = 1,

        [Metadata("DataExtractor", "DataExtractor", resourceType: typeof(ProjectStepMetadataResources))]
        DataExtractor = 2,

        [Metadata("MacroTransformation", "MacroTransformation", resourceType: typeof(ProjectStepMetadataResources))]
        MacroTransformation = 3,

        [Metadata("ChileanTrees", "ChileanTrees", resourceType: typeof(ProjectStepMetadataResources))]
        ChileanTrees = 4,
    }
}
