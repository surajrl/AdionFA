using AdionFA.Infrastructure.Enums.Attributes;
using AdionFA.Infrastructure.I18n.Resources;

namespace AdionFA.Infrastructure.Enums
{
    public enum EntityTypeEnum
    {
        #region Common 1 - 99

        [Metadata("SETT", "Setting", "Setting", resourceType: typeof(EnumResources))]
        Setting = 1,

        #endregion  

        #region Core 100 - 199

        #endregion

        #region Contact 200 - 299

        #endregion

        #region Organization 300 - 399

        #endregion

        #region Market 400 - 499

        [Metadata("MKDT", "MarketData", "Market Data", resourceType: typeof(EnumResources))]
        MarketData = 400,

        #endregion

        #region Project 500 - 599

        [Metadata("PROJ", "Project", "Project", resourceType: typeof(EnumResources))]
        Project = 500,

        [Metadata("PROJCONFIG", "ProjectConfiguration", "Project Configuration", resourceType: typeof(EnumResources))]
        ProjectConfiguration = 501,

        [Metadata("CONFIG", "ProjectGlobalConfiguration", "Project Global Configuration", resourceType: typeof(EnumResources))]
        ProjectGlobalConfiguration = 502,
        
        [Metadata("EXT", "Extractor", "Extractor", resourceType: typeof(EnumResources))]
        Extractor = 503,

        [Metadata("STRBUILD", "StrategyBuilder", "Strategy Builder", resourceType: typeof(EnumResources))]
        StrategyBuilder = 504,

        [Metadata("ASSBUILD", "AssembledBuilder", "Assembled Builder", resourceType: typeof(EnumResources))]
        AssembledBuilder = 505,

        #endregion
    }
}
