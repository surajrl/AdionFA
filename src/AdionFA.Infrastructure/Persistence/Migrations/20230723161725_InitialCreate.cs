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
                name: "GlobalConfiguration",
                columns: table => new
                {
                    GlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTotalTradesOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxSuccessRateVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBWinningStrategyUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBWinningStrategyDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBTotalTradesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalConfiguration", x => x.GlobalConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "GlobalScheduleConfiguration",
                columns: table => new
                {
                    GlobalScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "HistoricalData",
                columns: table => new
                {
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTotalTradesOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxSuccessRateVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBWinningStrategyUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBWinningStrategyDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBTotalTradesTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinTotalTradesIS = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectConfiguration", x => x.ProjectConfigurationId);
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
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
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
                table: "GlobalConfiguration",
                columns: new[] { "GlobalConfigurationId", "ABMinImprovePercent", "ABMinTotalTradesIS", "ABWekaMaxRatioTree", "ABWekaNTotalTree", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "ExpertAdvisorHost", "ExpertAdvisorPublisherPort", "ExpertAdvisorResponsePort", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxProgressivenessVariation", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "SBMaxCorrelationPercent", "SBMaxSuccessRateVariation", "SBMinSuccessRatePercentIS", "SBMinSuccessRatePercentOS", "SBMinTotalTradesIS", "SBMinTotalTradesOS", "SBTotalTradesTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 300, 1.5m, 300m, "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1885), 6, null, null, null, 50, null, null, false, false, 2m, 1.5m, 1000000, 100, 300m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1311), false, "Forex", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1513), false, "America", null, null, null, null },
                    { 2, "Europe", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1708), false, "Europe", null, null, null, null },
                    { 3, "Asia", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1833), false, "Asia", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(8688), false, "Culture", null, null, null, "eng" },
                    { 2, "Theme", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(8883), false, "Theme", null, null, null, "Light" },
                    { 3, "Color", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9022), false, "Color", null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9171), false, "DefaultWorkspace", null, null, null, "" },
                    { 5, "Host", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9306), false, "Host", null, null, null, "192.168.50.137" },
                    { 6, "Port", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9446), false, "Port", null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9661), false, "EURUSD", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 202, DateTimeKind.Utc).AddTicks(9980), false, "1 Minute", null, null, null, "1" },
                    { 2, "M5", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(128), false, "5 Minutes", null, null, null, "5" },
                    { 3, "M15", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(258), false, "15 Minutes", null, null, null, "15" },
                    { 4, "M30", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(391), false, "30 Minutes", null, null, null, "30" },
                    { 5, "H1", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(518), false, "1 Hour", null, null, null, "16385" },
                    { 6, "H4", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(712), false, "4 Hours", null, null, null, "16388" },
                    { 7, "D1", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(847), false, "Daily", null, null, null, "16408" },
                    { 8, "W1", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(976), false, "Weekly", null, null, null, "32769" },
                    { 9, "MN1", "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1116), false, "Monthly", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "GlobalScheduleConfiguration",
                columns: new[] { "GlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "FromTimeInSeconds", "GlobalConfigurationId", "IsDeleted", "MarketRegionId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1924), 54000, 1, false, 1, 82800, null, null, null },
                    { 2, "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1927), 32400, 1, false, 2, 64800, null, null, null },
                    { 3, "0", "admin", new DateTime(2023, 7, 23, 16, 17, 25, 203, DateTimeKind.Utc).AddTicks(1929), 3600, 1, false, 3, 32400, null, null, null }
                });

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
