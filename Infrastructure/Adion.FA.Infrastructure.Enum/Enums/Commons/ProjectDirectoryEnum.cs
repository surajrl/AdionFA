using System;
using System.ComponentModel;

namespace Adion.FA.Infrastructure.Enums
{
    public enum ProjectDirectoryEnum
    {
        [Description(@"FA.Workspace")]
        DefaultWorkspace = Environment.SpecialFolder.MyDocuments,

        [Description(@"Projects")]
        Projects = 110,

        #region Extractor
        [Description(@"{0}\Extractions")]
        Extractor = 120,

        [Description(@"{0}\Extractions\{1}")]
        ExtractorMarket = 130,

        [Description(@"{0}\Extractions\WithoutSchedule")]
        ExtractorWithoutSchedule = 140,

        [Description(@"{0}\Extractions\Templates")]
        ExtractorTemplate = 150,
        #endregion

        #region Strategy Builder
        [Description(@"{0}\StrategyBuilder")]
        StrategyBuilder = 210,

        [Description(@"{0}\StrategyBuilder\Nodes")]
        StrategyBuilderNodes = 220,

        [Description(@"{0}\StrategyBuilder\Nodes\UP")]
        StrategyBuilderNodesUP = 230,

        [Description(@"{0}\StrategyBuilder\Nodes\DOWN")]
        StrategyBuilderNodesDOWN = 240,
        #endregion

        #region Assembled Builder
        [Description(@"{0}\AssembledBuilder")]
        AssembledBuilder = 310,

        [Description(@"{0}\AssembledBuilder\Extractions\UP")]
        AssembledBuilderExtractorUP = 320,

        [Description(@"{0}\AssembledBuilder\Extractions\DOWN")]
        AssembledBuilderExtractorDOWN = 330,

        [Description(@"{0}\AssembledBuilder\Extractions\{1}\{2}")]
        AssembledBuilderExtractorMarket = 340,

        [Description(@"{0}\AssembledBuilder\Extractions\{1}\WithoutSchedule")]
        AssembledBuilderExtractorWithoutSchedule = 350,

        [Description(@"{0}\AssembledBuilder\Nodes")]
        AssembledBuilderNodes = 360,

        [Description(@"{0}\AssembledBuilder\Nodes\UP")]
        AssembledBuilderNodesUP = 370,

        [Description(@"{0}\AssembledBuilder\Nodes\DOWN")]
        AssembledBuilderNodesDOWN = 380,
        #endregion
    }
}
