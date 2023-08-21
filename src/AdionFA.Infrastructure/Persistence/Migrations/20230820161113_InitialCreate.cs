using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdionFA.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssemblyBuilderConfiguration",
                columns: table => new
                {
                    AssemblyBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssemblyNodesUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    AssemblyNodesDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalTradesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    MinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinSuccessRateImprovementIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinSuccessRateImprovementOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSuccessRateImprovementIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSuccessRateImprovementOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    WekaNTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaStartDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaEndDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaMaxRatio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyBuilderConfiguration", x => x.AssemblyBuilderConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "CrossingBuilderConfiguration",
                columns: table => new
                {
                    CrossingBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StrategyNodesUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    StrategyNodesDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalTradesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    MinSuccessRateImprovementIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinSuccessRateImprovementOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSuccessRateImprovementIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSuccessRateImprovementOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    WekaNTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaStartDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaEndDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaMaxRatio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossingBuilderConfiguration", x => x.CrossingBuilderConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.MarketId);
                });

            migrationBuilder.CreateTable(
                name: "MarketRegion",
                columns: table => new
                {
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketRegion", x => x.MarketRegionId);
                });

            migrationBuilder.CreateTable(
                name: "NodeBuilderConfiguration",
                columns: table => new
                {
                    NodeBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NodesUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    NodesDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalTradesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    MinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinSuccessRatePercentIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinTotalTradesOS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinSuccessRatePercentOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxSuccessRateVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    WekaNTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaStartDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaEndDepth = table.Column<int>(type: "INTEGER", nullable: false),
                    WekaMaxRatio = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeBuilderConfiguration", x => x.NodeBuilderConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbol", x => x.SymbolId);
                });

            migrationBuilder.CreateTable(
                name: "Timeframe",
                columns: table => new
                {
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeframe", x => x.TimeframeId);
                });

            migrationBuilder.CreateTable(
                name: "GlobalConfiguration",
                columns: table => new
                {
                    GlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NodeBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssemblyBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CrossingBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtractorMinVariation = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpertAdvisorHost = table.Column<string>(type: "TEXT", nullable: true),
                    ExpertAdvisorResponsePort = table.Column<string>(type: "TEXT", nullable: true),
                    ExpertAdvisorPublisherPort = table.Column<string>(type: "TEXT", nullable: true),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxParallelism = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalConfiguration", x => x.GlobalConfigurationId);
                    table.ForeignKey(
                        name: "FK_GlobalConfiguration_AssemblyBuilderConfiguration_AssemblyBuilderConfigurationId",
                        column: x => x.AssemblyBuilderConfigurationId,
                        principalTable: "AssemblyBuilderConfiguration",
                        principalColumn: "AssemblyBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalConfiguration_CrossingBuilderConfiguration_CrossingBuilderConfigurationId",
                        column: x => x.CrossingBuilderConfigurationId,
                        principalTable: "CrossingBuilderConfiguration",
                        principalColumn: "CrossingBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalConfiguration_NodeBuilderConfiguration_NodeBuilderConfigurationId",
                        column: x => x.NodeBuilderConfigurationId,
                        principalTable: "NodeBuilderConfiguration",
                        principalColumn: "NodeBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalData",
                columns: table => new
                {
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalData", x => x.HistoricalDataId);
                    table.ForeignKey(
                        name: "FK_HistoricalData_Market_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Market",
                        principalColumn: "MarketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricalData_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricalData_Timeframe_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "Timeframe",
                        principalColumn: "TimeframeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlobalScheduleConfiguration",
                columns: table => new
                {
                    GlobalScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalScheduleConfiguration", x => x.GlobalScheduleConfigurationId);
                    table.ForeignKey(
                        name: "FK_GlobalScheduleConfiguration_GlobalConfiguration_GlobalConfigurationId",
                        column: x => x.GlobalConfigurationId,
                        principalTable: "GlobalConfiguration",
                        principalColumn: "GlobalConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlobalScheduleConfiguration_MarketRegion_MarketRegionId",
                        column: x => x.MarketRegionId,
                        principalTable: "MarketRegion",
                        principalColumn: "MarketRegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalDataCandle",
                columns: table => new
                {
                    HistoricalDataCandleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<long>(type: "INTEGER", nullable: false),
                    Open = table.Column<double>(type: "REAL", nullable: false),
                    High = table.Column<double>(type: "REAL", nullable: false),
                    Low = table.Column<double>(type: "REAL", nullable: false),
                    Close = table.Column<double>(type: "REAL", nullable: false),
                    Volume = table.Column<double>(type: "REAL", nullable: false),
                    Spread = table.Column<double>(type: "REAL", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalDataCandle", x => x.HistoricalDataCandleId);
                    table.ForeignKey(
                        name: "FK_HistoricalDataCandle_HistoricalData_HistoricalDataId",
                        column: x => x.HistoricalDataId,
                        principalTable: "HistoricalData",
                        principalColumn: "HistoricalDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    WorkspacePath = table.Column<string>(type: "TEXT", nullable: true),
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_HistoricalData_HistoricalDataId",
                        column: x => x.HistoricalDataId,
                        principalTable: "HistoricalData",
                        principalColumn: "HistoricalDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectConfiguration",
                columns: table => new
                {
                    ProjectConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    NodeBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssemblyBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CrossingBuilderConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExtractorMinVariation = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpertAdvisorHost = table.Column<string>(type: "TEXT", nullable: true),
                    ExpertAdvisorResponsePort = table.Column<string>(type: "TEXT", nullable: true),
                    ExpertAdvisorPublisherPort = table.Column<string>(type: "TEXT", nullable: true),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxParallelism = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectConfiguration", x => x.ProjectConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_AssemblyBuilderConfiguration_AssemblyBuilderConfigurationId",
                        column: x => x.AssemblyBuilderConfigurationId,
                        principalTable: "AssemblyBuilderConfiguration",
                        principalColumn: "AssemblyBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_CrossingBuilderConfiguration_CrossingBuilderConfigurationId",
                        column: x => x.CrossingBuilderConfigurationId,
                        principalTable: "CrossingBuilderConfiguration",
                        principalColumn: "CrossingBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_NodeBuilderConfiguration_NodeBuilderConfigurationId",
                        column: x => x.NodeBuilderConfigurationId,
                        principalTable: "NodeBuilderConfiguration",
                        principalColumn: "NodeBuilderConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleConfiguration",
                columns: table => new
                {
                    ProjectScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectScheduleConfiguration", x => x.ProjectScheduleConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleConfiguration_MarketRegion_MarketRegionId",
                        column: x => x.MarketRegionId,
                        principalTable: "MarketRegion",
                        principalColumn: "MarketRegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleConfiguration_ProjectConfiguration_ProjectConfigurationId",
                        column: x => x.ProjectConfigurationId,
                        principalTable: "ProjectConfiguration",
                        principalColumn: "ProjectConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssemblyBuilderConfiguration",
                columns: new[] { "AssemblyBuilderConfigurationId", "AssemblyNodesDOWNTarget", "AssemblyNodesUPTarget", "CreatedOn", "IsDeleted", "MaxSuccessRateImprovementIS", "MaxSuccessRateImprovementOS", "MinSuccessRateImprovementIS", "MinSuccessRateImprovementOS", "MinTotalTradesIS", "TotalTradesTarget", "UpdatedOn", "WekaEndDepth", "WekaMaxRatio", "WekaNTotal", "WekaStartDepth" },
                values: new object[] { 1, 6, 6, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8502), false, 4m, 4m, 2m, 2m, 100, 1000, null, 6, 1.5m, 300, 1 });

            migrationBuilder.InsertData(
                table: "CrossingBuilderConfiguration",
                columns: new[] { "CrossingBuilderConfigurationId", "CreatedOn", "IsDeleted", "MaxSuccessRateImprovementIS", "MaxSuccessRateImprovementOS", "MinSuccessRateImprovementIS", "MinSuccessRateImprovementOS", "StrategyNodesDOWNTarget", "StrategyNodesUPTarget", "TotalTradesTarget", "UpdatedOn", "WekaEndDepth", "WekaMaxRatio", "WekaNTotal", "WekaStartDepth" },
                values: new object[] { 1, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8536), false, 4m, 4m, 2m, 2m, 6, 6, 1000, null, 6, 1.5m, 300, 1 });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedOn", "IsDeleted", "Name", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(7806), false, "Forex", null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedOn", "IsDeleted", "Name", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8052), false, "America", null, null },
                    { 2, "Europe", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8211), false, "Europe", null, null },
                    { 3, "Asia", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8358), false, "Asia", null, null }
                });

            migrationBuilder.InsertData(
                table: "NodeBuilderConfiguration",
                columns: new[] { "NodeBuilderConfigurationId", "CreatedOn", "IsDeleted", "MaxSuccessRateVariation", "MinSuccessRatePercentIS", "MinSuccessRatePercentOS", "MinTotalTradesIS", "MinTotalTradesOS", "NodesDOWNTarget", "NodesUPTarget", "TotalTradesTarget", "UpdatedOn", "WekaEndDepth", "WekaMaxRatio", "WekaNTotal", "WekaStartDepth" },
                values: new object[] { 1, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8454), false, 5m, 40m, 40m, 200, 100, 6, 6, 100, null, 12, 1.5m, 300, 4 });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedOn", "IsDeleted", "Name", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(4720), false, "Culture", null, "eng" },
                    { 2, "Theme", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(4944), false, "Theme", null, "Light" },
                    { 3, "Color", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(5104), false, "Color", null, "Orange" },
                    { 4, "DefaultWorkspace", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(5294), false, "DefaultWorkspace", null, "" },
                    { 5, "Host", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(5456), false, "Host", null, "192.168.50.137" },
                    { 6, "Port", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(5636), false, "Port", null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedOn", "IsDeleted", "Name", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(5901), false, "EURUSD", null, "" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedOn", "IsDeleted", "Name", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(6206), false, "1 Minute", null, "1" },
                    { 2, "M5", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(6378), false, "5 Minutes", null, "5" },
                    { 3, "M15", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(6551), false, "15 Minutes", null, "15" },
                    { 4, "M30", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(6718), false, "30 Minutes", null, "30" },
                    { 5, "H1", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(6873), false, "1 Hour", null, "16385" },
                    { 6, "H4", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(7048), false, "4 Hours", null, "16388" },
                    { 7, "D1", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(7213), false, "Daily", null, "16408" },
                    { 8, "W1", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(7368), false, "Weekly", null, "32769" },
                    { 9, "MN1", new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(7555), false, "Monthly", null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "GlobalConfiguration",
                columns: new[] { "GlobalConfigurationId", "AssemblyBuilderConfigurationId", "CreatedOn", "CrossingBuilderConfigurationId", "ExpertAdvisorHost", "ExpertAdvisorPublisherPort", "ExpertAdvisorResponsePort", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxCorrelationPercent", "MaxParallelism", "MaxProgressivenessVariation", "MaximumSeed", "MinimalSeed", "NodeBuilderConfigurationId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 1, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8407), 1, "192.168.1.35", "5551", "5550", 50, null, null, false, false, 2m, 1, 2m, 1000000, 100, 1, null, null, 5, null, true });

            migrationBuilder.InsertData(
                table: "GlobalScheduleConfiguration",
                columns: new[] { "GlobalScheduleConfigurationId", "CreatedOn", "FromTimeInSeconds", "GlobalConfigurationId", "IsDeleted", "MarketRegionId", "ToTimeInSeconds", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8577), 54000, 1, false, 1, 82800, null },
                    { 2, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8594), 32400, 1, false, 2, 64800, null },
                    { 3, new DateTime(2023, 8, 20, 16, 11, 13, 84, DateTimeKind.Utc).AddTicks(8601), 3600, 1, false, 3, 32400, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalConfiguration_AssemblyBuilderConfigurationId",
                table: "GlobalConfiguration",
                column: "AssemblyBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalConfiguration_CrossingBuilderConfigurationId",
                table: "GlobalConfiguration",
                column: "CrossingBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalConfiguration_NodeBuilderConfigurationId",
                table: "GlobalConfiguration",
                column: "NodeBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalScheduleConfiguration_GlobalConfigurationId",
                table: "GlobalScheduleConfiguration",
                column: "GlobalConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalScheduleConfiguration_MarketRegionId",
                table: "GlobalScheduleConfiguration",
                column: "MarketRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalData_MarketId",
                table: "HistoricalData",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalData_SymbolId",
                table: "HistoricalData",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalData_TimeframeId",
                table: "HistoricalData",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalDataCandle_HistoricalDataId",
                table: "HistoricalDataCandle",
                column: "HistoricalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_HistoricalDataId",
                table: "Project",
                column: "HistoricalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectName",
                table: "Project",
                column: "ProjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_AssemblyBuilderConfigurationId",
                table: "ProjectConfiguration",
                column: "AssemblyBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_CrossingBuilderConfigurationId",
                table: "ProjectConfiguration",
                column: "CrossingBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_NodeBuilderConfigurationId",
                table: "ProjectConfiguration",
                column: "NodeBuilderConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_ProjectId",
                table: "ProjectConfiguration",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleConfiguration_MarketRegionId",
                table: "ProjectScheduleConfiguration",
                column: "MarketRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleConfiguration_ProjectConfigurationId",
                table: "ProjectScheduleConfiguration",
                column: "ProjectConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "HistoricalDataCandle");

            migrationBuilder.DropTable(
                name: "ProjectScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "GlobalConfiguration");

            migrationBuilder.DropTable(
                name: "MarketRegion");

            migrationBuilder.DropTable(
                name: "ProjectConfiguration");

            migrationBuilder.DropTable(
                name: "AssemblyBuilderConfiguration");

            migrationBuilder.DropTable(
                name: "CrossingBuilderConfiguration");

            migrationBuilder.DropTable(
                name: "NodeBuilderConfiguration");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "HistoricalData");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Timeframe");
        }
    }
}
