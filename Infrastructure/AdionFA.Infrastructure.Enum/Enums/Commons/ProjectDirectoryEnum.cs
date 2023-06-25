using System;
using System.ComponentModel;

namespace AdionFA.Infrastructure.Enums
{
    public enum ProjectDirectoryEnum
    {
        [Description(@"AdionFA.Workspace")]
        DefaultWorkspace = Environment.SpecialFolder.MyDocuments,

        [Description(@"Projects")]
        Projects = 110,

        // Extractor

        [Description(@"{0}\ExtractorTemplates")]
        ExtractorTemplate = 120,

        // Strategy Builder

        [Description(@"{0}\StrategyBuilder")]
        StrategyBuilder = 210,

        [Description(@"{0}\StrategyBuilder\Nodes")]
        StrategyBuilderNodes = 220,

        [Description(@"{0}\StrategyBuilder\Nodes\UP")]
        StrategyBuilderNodesUP = 230,

        [Description(@"{0}\StrategyBuilder\Nodes\DOWN")]
        StrategyBuilderNodesDOWN = 240,

        [Description(@"{0}\StrategyBuilder\Extractions\{1}")]
        StrategyBuilderExtractorMarket = 250,

        [Description(@"{0}\StrategyBuilder\Extractions\WithoutSchedule")]
        StrategyBuilderExtractorWithoutSchedule = 260,

        // Assembly Builder

        [Description(@"{0}\AssemblyBuilder")]
        AssemblyBuilder = 310,

        [Description(@"{0}\AssembltBuilder\Extractions\UP")]
        AssemblyBuilderExtractorUP = 320,

        [Description(@"{0}\AssemblyBuilder\Extractions\DOWN")]
        AssemblyBuilderExtractorDOWN = 330,

        [Description(@"{0}\AssemblyBuilder\Extractions\{1}\{2}")]
        AssemblyBuilderExtractorMarket = 340,

        [Description(@"{0}\AssemblyBuilder\Extractions\{1}\WithoutSchedule")]
        AssemblyBuilderExtractorWithoutSchedule = 350,

        [Description(@"{0}\AssemblyBuilder\Nodes")]
        AssemblyBuilderNodes = 360,

        [Description(@"{0}\AssemblyBuilder\Nodes\UP")]
        AssemblyBuilderNodesUP = 370,

        [Description(@"{0}\AssemblyBuilder\Nodes\DOWN")]
        AssemblyBuilderNodesDOWN = 380,

        // Crossing Builder

        [Description(@"{0}\CrossingBuilder")]
        CrossingBuilder = 410,

        [Description(@"{0}\CrossingBuilder\Extractions\UP")]
        CrossingBuilderExtractorUP = 420,

        [Description(@"{0}\CrossingBuilder\Extractions\DOWN")]
        CrossingBuilderExtractorDOWN = 430,

        [Description(@"{0}\CrossingBuilder\Extractions\{1}\{2}")]
        CrossingBuilderExtractorMarket = 440,

        [Description(@"{0}\CrossingBuilder\Extractions\{1}\WithoutSchedule")]
        CrossingBuilderExtractorWithoutSchedule = 450,

        [Description(@"{0}\CrossingBuilder\Nodes")]
        CrossingBuilderNodes = 460,

        [Description(@"{0}\CrossingBuilder\Nodes\UP\{1}")]
        CrossingBuilderNodesUP = 470,

        [Description(@"{0}\CrossingBuilder\Nodes\DOWN\{1}")]
        CrossingBuilderNodesDOWN = 480,

    }
}