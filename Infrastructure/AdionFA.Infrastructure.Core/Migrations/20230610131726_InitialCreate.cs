using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdionFA.Infrastructure.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "EntityType",
                columns: table => new
                {
                    EntityTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.EntityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
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
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketRegion", x => x.MarketRegionId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
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
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
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
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeframe", x => x.TimeframeId);
                });

            migrationBuilder.CreateTable(
                name: "EntityServiceHost",
                columns: table => new
                {
                    EntityServiceHostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcessId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityServiceHost", x => x.EntityServiceHostId);
                    table.ForeignKey(
                        name: "FK_EntityServiceHost_EntityType_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityType",
                        principalColumn: "EntityTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertAdvisor",
                columns: table => new
                {
                    ExpertAdvisorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MagicNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Host = table.Column<string>(type: "TEXT", nullable: true),
                    ResponsePort = table.Column<string>(type: "TEXT", nullable: true),
                    PublisherPort = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertAdvisor", x => x.ExpertAdvisorId);
                    table.ForeignKey(
                        name: "FK_ExpertAdvisor_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtractorMinVariation = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsIS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxSuccessRateVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBWinningStrategyUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBWinningStrategyDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBTransactionsTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABTransactionsTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.ConfigurationId);
                    table.ForeignKey(
                        name: "FK_Configuration_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Configuration_Timeframe_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "Timeframe",
                        principalColumn: "TimeframeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalData",
                columns: table => new
                {
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "ScheduleConfiguration",
                columns: table => new
                {
                    ScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleConfiguration", x => x.ScheduleConfigurationId);
                    table.ForeignKey(
                        name: "FK_ScheduleConfiguration_Configuration_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleConfiguration_MarketRegion_MarketRegionId",
                        column: x => x.MarketRegionId,
                        principalTable: "MarketRegion",
                        principalColumn: "MarketRegionId");
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
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                name: "ProjectConfiguration",
                columns: table => new
                {
                    ProjectConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    WorkspacePath = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtractorMinVariation = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsIS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinSuccessRatePercentOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxSuccessRateVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxProgressivenessVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxCorrelationPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBWinningStrategyUPTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBWinningStrategyDOWNTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    SBTransactionsTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABTransactionsTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    ABWekaNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectConfiguration", x => x.ProjectConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_HistoricalData_HistoricalDataId",
                        column: x => x.HistoricalDataId,
                        principalTable: "HistoricalData",
                        principalColumn: "HistoricalDataId");
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_Timeframe_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "Timeframe",
                        principalColumn: "TimeframeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleConfiguration",
                columns: table => new
                {
                    ProjectScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedById = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectScheduleConfiguration", x => x.ProjectScheduleConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleConfiguration_MarketRegion_MarketRegionId",
                        column: x => x.MarketRegionId,
                        principalTable: "MarketRegion",
                        principalColumn: "MarketRegionId");
                    table.ForeignKey(
                        name: "FK_ProjectScheduleConfiguration_ProjectConfiguration_ProjectConfigurationId",
                        column: x => x.ProjectConfigurationId,
                        principalTable: "ProjectConfiguration",
                        principalColumn: "ProjectConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "AFN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 60, DateTimeKind.Utc).AddTicks(4560), "", false, "", null, null, null, "" },
                    { 2, "AFA", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 61, DateTimeKind.Utc).AddTicks(498), "", false, "", null, null, null, "" },
                    { 3, "ALL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 61, DateTimeKind.Utc).AddTicks(6520), "", false, "", null, null, null, "" },
                    { 4, "DZD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 62, DateTimeKind.Utc).AddTicks(3380), "", false, "", null, null, null, "" },
                    { 5, "ADF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 63, DateTimeKind.Utc).AddTicks(1445), "", false, "", null, null, null, "" },
                    { 6, "ADP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 63, DateTimeKind.Utc).AddTicks(9377), "", false, "", null, null, null, "" },
                    { 7, "AOA", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 64, DateTimeKind.Utc).AddTicks(5145), "", false, "", null, null, null, "" },
                    { 8, "AON", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 65, DateTimeKind.Utc).AddTicks(574), "", false, "", null, null, null, "" },
                    { 9, "ARS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 65, DateTimeKind.Utc).AddTicks(5977), "", false, "", null, null, null, "" },
                    { 10, "AMD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 66, DateTimeKind.Utc).AddTicks(2833), "", false, "", null, null, null, "" },
                    { 11, "AWG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 67, DateTimeKind.Utc).AddTicks(388), "", false, "", null, null, null, "" },
                    { 12, "AUD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 67, DateTimeKind.Utc).AddTicks(9809), "", false, "", null, null, null, "" },
                    { 13, "ATS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 70, DateTimeKind.Utc).AddTicks(2718), "", false, "", null, null, null, "" },
                    { 14, "AZM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 71, DateTimeKind.Utc).AddTicks(6927), "", false, "", null, null, null, "" },
                    { 15, "AZN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 72, DateTimeKind.Utc).AddTicks(4982), "", false, "", null, null, null, "" },
                    { 16, "BSD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 73, DateTimeKind.Utc).AddTicks(5773), "", false, "", null, null, null, "" },
                    { 17, "BHD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 74, DateTimeKind.Utc).AddTicks(4924), "", false, "", null, null, null, "" },
                    { 18, "BDT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 75, DateTimeKind.Utc).AddTicks(803), "", false, "", null, null, null, "" },
                    { 19, "BBD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 75, DateTimeKind.Utc).AddTicks(6095), "", false, "", null, null, null, "" },
                    { 20, "BEF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 76, DateTimeKind.Utc).AddTicks(1541), "", false, "", null, null, null, "" },
                    { 21, "BZD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 78, DateTimeKind.Utc).AddTicks(6985), "", false, "", null, null, null, "" },
                    { 22, "BMD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 79, DateTimeKind.Utc).AddTicks(2706), "", false, "", null, null, null, "" },
                    { 23, "BTN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 79, DateTimeKind.Utc).AddTicks(7847), "", false, "", null, null, null, "" },
                    { 24, "BOB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 80, DateTimeKind.Utc).AddTicks(2910), "", false, "", null, null, null, "" },
                    { 25, "BAM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 80, DateTimeKind.Utc).AddTicks(9396), "", false, "", null, null, null, "" },
                    { 26, "BWP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 81, DateTimeKind.Utc).AddTicks(6748), "", false, "", null, null, null, "" },
                    { 27, "BRL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 82, DateTimeKind.Utc).AddTicks(3776), "", false, "", null, null, null, "" },
                    { 28, "GBP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 82, DateTimeKind.Utc).AddTicks(9197), "", false, "", null, null, null, "" },
                    { 29, "BND", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 83, DateTimeKind.Utc).AddTicks(4453), "", false, "", null, null, null, "" },
                    { 30, "BGN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 83, DateTimeKind.Utc).AddTicks(9986), "", false, "", null, null, null, "" },
                    { 31, "BGL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 85, DateTimeKind.Utc).AddTicks(1151), "", false, "", null, null, null, "" },
                    { 32, "BIF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 86, DateTimeKind.Utc).AddTicks(1073), "", false, "", null, null, null, "" },
                    { 33, "BYR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 87, DateTimeKind.Utc).AddTicks(1484), "", false, "", null, null, null, "" },
                    { 34, "XOF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 88, DateTimeKind.Utc).AddTicks(909), "", false, "", null, null, null, "" },
                    { 35, "XAF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 88, DateTimeKind.Utc).AddTicks(6979), "", false, "", null, null, null, "" },
                    { 36, "XPF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 89, DateTimeKind.Utc).AddTicks(3752), "", false, "", null, null, null, "" },
                    { 37, "KHR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 90, DateTimeKind.Utc).AddTicks(700), "", false, "", null, null, null, "" },
                    { 38, "CAD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 90, DateTimeKind.Utc).AddTicks(8415), "", false, "", null, null, null, "" },
                    { 39, "CVE", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 91, DateTimeKind.Utc).AddTicks(6043), "", false, "", null, null, null, "" },
                    { 40, "KYD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 92, DateTimeKind.Utc).AddTicks(2529), "", false, "", null, null, null, "" },
                    { 41, "CLP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 92, DateTimeKind.Utc).AddTicks(8023), "", false, "", null, null, null, "" },
                    { 42, "CNY", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 93, DateTimeKind.Utc).AddTicks(3187), "", false, "", null, null, null, "" },
                    { 43, "COP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 93, DateTimeKind.Utc).AddTicks(8359), "", false, "", null, null, null, "" },
                    { 44, "KMF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 94, DateTimeKind.Utc).AddTicks(4743), "", false, "", null, null, null, "" },
                    { 45, "CDF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 95, DateTimeKind.Utc).AddTicks(2007), "", false, "", null, null, null, "" },
                    { 46, "CRC", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 95, DateTimeKind.Utc).AddTicks(8275), "", false, "", null, null, null, "" },
                    { 47, "HRK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 96, DateTimeKind.Utc).AddTicks(3662), "", false, "", null, null, null, "" },
                    { 48, "CUC", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 96, DateTimeKind.Utc).AddTicks(8859), "", false, "", null, null, null, "" },
                    { 49, "CUP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 97, DateTimeKind.Utc).AddTicks(4140), "", false, "", null, null, null, "" },
                    { 50, "CYP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 97, DateTimeKind.Utc).AddTicks(9871), "", false, "", null, null, null, "" },
                    { 51, "CZK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 98, DateTimeKind.Utc).AddTicks(7380), "", false, "", null, null, null, "" },
                    { 52, "DKK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 99, DateTimeKind.Utc).AddTicks(4634), "", false, "", null, null, null, "" },
                    { 53, "DJF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 100, DateTimeKind.Utc).AddTicks(175), "", false, "", null, null, null, "" },
                    { 54, "DOP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 100, DateTimeKind.Utc).AddTicks(5548), "", false, "", null, null, null, "" },
                    { 55, "NLG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 101, DateTimeKind.Utc).AddTicks(4603), "", false, "", null, null, null, "" },
                    { 56, "XCD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 102, DateTimeKind.Utc).AddTicks(7459), "", false, "", null, null, null, "" },
                    { 57, "XEU", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 103, DateTimeKind.Utc).AddTicks(9545), "", false, "", null, null, null, "" },
                    { 58, "ECS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 105, DateTimeKind.Utc).AddTicks(382), "", false, "", null, null, null, "" },
                    { 59, "EGP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 105, DateTimeKind.Utc).AddTicks(7242), "", false, "", null, null, null, "" },
                    { 60, "SVC", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 106, DateTimeKind.Utc).AddTicks(5102), "", false, "", null, null, null, "" },
                    { 61, "EEK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 107, DateTimeKind.Utc).AddTicks(2044), "", false, "", null, null, null, "" },
                    { 62, "ETB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 107, DateTimeKind.Utc).AddTicks(9069), "", false, "", null, null, null, "" },
                    { 63, "EUR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 108, DateTimeKind.Utc).AddTicks(5701), "", false, "", null, null, null, "" },
                    { 64, "FKP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 109, DateTimeKind.Utc).AddTicks(2868), "", false, "", null, null, null, "" },
                    { 65, "FJD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 109, DateTimeKind.Utc).AddTicks(9650), "", false, "", null, null, null, "" },
                    { 66, "FIM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 110, DateTimeKind.Utc).AddTicks(5703), "", false, "", null, null, null, "" },
                    { 67, "FRF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 111, DateTimeKind.Utc).AddTicks(1053), "", false, "", null, null, null, "" },
                    { 68, "GMD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 111, DateTimeKind.Utc).AddTicks(6184), "", false, "", null, null, null, "" },
                    { 69, "GEL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 112, DateTimeKind.Utc).AddTicks(1176), "", false, "", null, null, null, "" },
                    { 70, "DEM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 112, DateTimeKind.Utc).AddTicks(7595), "", false, "", null, null, null, "" },
                    { 71, "GHC", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 113, DateTimeKind.Utc).AddTicks(4518), "", false, "", null, null, null, "" },
                    { 72, "GHS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 114, DateTimeKind.Utc).AddTicks(1166), "", false, "", null, null, null, "" },
                    { 73, "GIP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 114, DateTimeKind.Utc).AddTicks(6573), "", false, "", null, null, null, "" },
                    { 74, "XAU", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 115, DateTimeKind.Utc).AddTicks(1849), "", false, "", null, null, null, "" },
                    { 75, "GRD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 115, DateTimeKind.Utc).AddTicks(6949), "", false, "", null, null, null, "" },
                    { 76, "GTQ", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 116, DateTimeKind.Utc).AddTicks(3006), "", false, "", null, null, null, "" },
                    { 77, "GNF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 117, DateTimeKind.Utc).AddTicks(39), "", false, "", null, null, null, "" },
                    { 78, "GYD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 117, DateTimeKind.Utc).AddTicks(8684), "", false, "", null, null, null, "" },
                    { 79, "HTG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 118, DateTimeKind.Utc).AddTicks(8960), "", false, "", null, null, null, "" },
                    { 80, "HNL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 120, DateTimeKind.Utc).AddTicks(1100), "", false, "", null, null, null, "" },
                    { 81, "HKD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 121, DateTimeKind.Utc).AddTicks(2506), "", false, "", null, null, null, "" },
                    { 82, "HUF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 122, DateTimeKind.Utc).AddTicks(1021), "", false, "", null, null, null, "" },
                    { 83, "ISK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 123, DateTimeKind.Utc).AddTicks(779), "", false, "", null, null, null, "" },
                    { 84, "INR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 123, DateTimeKind.Utc).AddTicks(8247), "", false, "", null, null, null, "" },
                    { 85, "IDR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 124, DateTimeKind.Utc).AddTicks(4412), "", false, "", null, null, null, "" },
                    { 86, "IRR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 124, DateTimeKind.Utc).AddTicks(9563), "", false, "", null, null, null, "" },
                    { 87, "IQD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 125, DateTimeKind.Utc).AddTicks(4474), "", false, "", null, null, null, "" },
                    { 88, "IEP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 125, DateTimeKind.Utc).AddTicks(9452), "", false, "", null, null, null, "" },
                    { 89, "ILS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 126, DateTimeKind.Utc).AddTicks(5632), "", false, "", null, null, null, "" },
                    { 90, "ITL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 127, DateTimeKind.Utc).AddTicks(3057), "", false, "", null, null, null, "" },
                    { 91, "JMD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 127, DateTimeKind.Utc).AddTicks(9824), "", false, "", null, null, null, "" },
                    { 92, "JPY", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 128, DateTimeKind.Utc).AddTicks(5258), "", false, "", null, null, null, "" },
                    { 93, "JOD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 129, DateTimeKind.Utc).AddTicks(433), "", false, "", null, null, null, "" },
                    { 94, "KHR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 129, DateTimeKind.Utc).AddTicks(5408), "", false, "", null, null, null, "" },
                    { 95, "KZT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 130, DateTimeKind.Utc).AddTicks(890), "", false, "", null, null, null, "" },
                    { 96, "KES", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 130, DateTimeKind.Utc).AddTicks(8186), "", false, "", null, null, null, "" },
                    { 97, "KRW", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 131, DateTimeKind.Utc).AddTicks(5501), "", false, "", null, null, null, "" },
                    { 98, "KWD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 132, DateTimeKind.Utc).AddTicks(967), "", false, "", null, null, null, "" },
                    { 99, "KGS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 132, DateTimeKind.Utc).AddTicks(6422), "", false, "", null, null, null, "" },
                    { 100, "LAK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 133, DateTimeKind.Utc).AddTicks(1494), "", false, "", null, null, null, "" },
                    { 101, "LVL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 133, DateTimeKind.Utc).AddTicks(6470), "", false, "", null, null, null, "" },
                    { 102, "LBP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 134, DateTimeKind.Utc).AddTicks(5230), "", false, "", null, null, null, "" },
                    { 103, "LSL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 135, DateTimeKind.Utc).AddTicks(8646), "", false, "", null, null, null, "" },
                    { 104, "LRD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 136, DateTimeKind.Utc).AddTicks(8985), "", false, "", null, null, null, "" },
                    { 105, "LYD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 137, DateTimeKind.Utc).AddTicks(9431), "", false, "", null, null, null, "" },
                    { 106, "LTL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 138, DateTimeKind.Utc).AddTicks(8392), "", false, "", null, null, null, "" },
                    { 107, "LUF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 139, DateTimeKind.Utc).AddTicks(7134), "", false, "", null, null, null, "" },
                    { 108, "MOP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 140, DateTimeKind.Utc).AddTicks(5820), "", false, "", null, null, null, "" },
                    { 109, "MKD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 141, DateTimeKind.Utc).AddTicks(2295), "", false, "", null, null, null, "" },
                    { 110, "MGF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 142, DateTimeKind.Utc).AddTicks(159), "", false, "", null, null, null, "" },
                    { 111, "MWK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 142, DateTimeKind.Utc).AddTicks(7728), "", false, "", null, null, null, "" },
                    { 112, "MGA", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 143, DateTimeKind.Utc).AddTicks(3874), "", false, "", null, null, null, "" },
                    { 113, "MYR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 143, DateTimeKind.Utc).AddTicks(9726), "", false, "", null, null, null, "" },
                    { 114, "MVR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 144, DateTimeKind.Utc).AddTicks(5248), "", false, "", null, null, null, "" },
                    { 115, "MTL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 145, DateTimeKind.Utc).AddTicks(722), "", false, "", null, null, null, "" },
                    { 116, "MRO", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 145, DateTimeKind.Utc).AddTicks(6159), "", false, "", null, null, null, "" },
                    { 117, "MUR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 146, DateTimeKind.Utc).AddTicks(3252), "", false, "", null, null, null, "" },
                    { 118, "MXN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 147, DateTimeKind.Utc).AddTicks(467), "", false, "", null, null, null, "" },
                    { 119, "MDL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 147, DateTimeKind.Utc).AddTicks(7094), "", false, "", null, null, null, "" },
                    { 120, "MNT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 148, DateTimeKind.Utc).AddTicks(2637), "", false, "", null, null, null, "" },
                    { 121, "MAD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 148, DateTimeKind.Utc).AddTicks(8382), "", false, "", null, null, null, "" },
                    { 122, "MZM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 149, DateTimeKind.Utc).AddTicks(4046), "", false, "", null, null, null, "" },
                    { 123, "MZN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 149, DateTimeKind.Utc).AddTicks(9874), "", false, "", null, null, null, "" },
                    { 124, "MMK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 150, DateTimeKind.Utc).AddTicks(6809), "", false, "", null, null, null, "" },
                    { 125, "ANG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 152, DateTimeKind.Utc).AddTicks(941), "", false, "", null, null, null, "" },
                    { 126, "NAD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 153, DateTimeKind.Utc).AddTicks(2393), "", false, "", null, null, null, "" },
                    { 127, "NPR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 154, DateTimeKind.Utc).AddTicks(4334), "", false, "", null, null, null, "" },
                    { 128, "NLG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 155, DateTimeKind.Utc).AddTicks(5809), "", false, "", null, null, null, "" },
                    { 129, "NZD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 156, DateTimeKind.Utc).AddTicks(6257), "", false, "", null, null, null, "" },
                    { 130, "NIO", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 157, DateTimeKind.Utc).AddTicks(6009), "", false, "", null, null, null, "" },
                    { 131, "NGN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 158, DateTimeKind.Utc).AddTicks(2651), "", false, "", null, null, null, "" },
                    { 132, "KPW", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 159, DateTimeKind.Utc).AddTicks(411), "", false, "", null, null, null, "" },
                    { 133, "NOK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 159, DateTimeKind.Utc).AddTicks(7833), "", false, "", null, null, null, "" },
                    { 134, "OMR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 160, DateTimeKind.Utc).AddTicks(4599), "", false, "", null, null, null, "" },
                    { 135, "PKR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 161, DateTimeKind.Utc).AddTicks(738), "", false, "", null, null, null, "" },
                    { 136, "XPD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 161, DateTimeKind.Utc).AddTicks(6782), "", false, "", null, null, null, "" },
                    { 137, "PAB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 162, DateTimeKind.Utc).AddTicks(2902), "", false, "", null, null, null, "" },
                    { 138, "PGK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 162, DateTimeKind.Utc).AddTicks(8953), "", false, "", null, null, null, "" },
                    { 139, "PYG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 163, DateTimeKind.Utc).AddTicks(5919), "", false, "", null, null, null, "" },
                    { 140, "PEN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 164, DateTimeKind.Utc).AddTicks(3544), "", false, "", null, null, null, "" },
                    { 141, "PHP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 165, DateTimeKind.Utc).AddTicks(1208), "", false, "", null, null, null, "" },
                    { 142, "XPT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 165, DateTimeKind.Utc).AddTicks(9303), "", false, "", null, null, null, "" },
                    { 143, "Mexico", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 166, DateTimeKind.Utc).AddTicks(9575), "", false, "", null, null, null, "" },
                    { 144, "PLN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 168, DateTimeKind.Utc).AddTicks(215), "", false, "", null, null, null, "" },
                    { 145, "PTE", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 169, DateTimeKind.Utc).AddTicks(4739), "", false, "", null, null, null, "" },
                    { 146, "GBP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 171, DateTimeKind.Utc).AddTicks(494), "", false, "", null, null, null, "" },
                    { 147, "ROL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 172, DateTimeKind.Utc).AddTicks(364), "", false, "", null, null, null, "" },
                    { 148, "RON", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 172, DateTimeKind.Utc).AddTicks(9861), "", false, "", null, null, null, "" },
                    { 149, "RUB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 174, DateTimeKind.Utc).AddTicks(7082), "", false, "", null, null, null, "" },
                    { 150, "RWF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 175, DateTimeKind.Utc).AddTicks(4182), "", false, "", null, null, null, "" },
                    { 151, "WST", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 176, DateTimeKind.Utc).AddTicks(232), "", false, "", null, null, null, "" },
                    { 152, "STD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 176, DateTimeKind.Utc).AddTicks(7760), "", false, "", null, null, null, "" },
                    { 153, "SAR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 177, DateTimeKind.Utc).AddTicks(5916), "", false, "", null, null, null, "" },
                    { 154, "RSD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 178, DateTimeKind.Utc).AddTicks(3777), "", false, "", null, null, null, "" },
                    { 155, "SCR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 178, DateTimeKind.Utc).AddTicks(9972), "", false, "", null, null, null, "" },
                    { 156, "SLL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 179, DateTimeKind.Utc).AddTicks(5712), "", false, "", null, null, null, "" },
                    { 157, "XAG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 180, DateTimeKind.Utc).AddTicks(2157), "", false, "", null, null, null, "" },
                    { 158, "SGD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 180, DateTimeKind.Utc).AddTicks(9539), "", false, "", null, null, null, "" },
                    { 159, "SKK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 181, DateTimeKind.Utc).AddTicks(8067), "", false, "", null, null, null, "" },
                    { 160, "SIT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 182, DateTimeKind.Utc).AddTicks(4664), "", false, "", null, null, null, "" },
                    { 161, "SBD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 183, DateTimeKind.Utc).AddTicks(915), "", false, "", null, null, null, "" },
                    { 162, "SOS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 183, DateTimeKind.Utc).AddTicks(6275), "", false, "", null, null, null, "" },
                    { 163, "ZAR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 184, DateTimeKind.Utc).AddTicks(3097), "", false, "", null, null, null, "" },
                    { 164, "ESP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 185, DateTimeKind.Utc).AddTicks(5971), "", false, "", null, null, null, "" },
                    { 165, "LKR", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 186, DateTimeKind.Utc).AddTicks(8331), "", false, "", null, null, null, "" },
                    { 166, "SHP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 188, DateTimeKind.Utc).AddTicks(136), "", false, "", null, null, null, "" },
                    { 167, "SDD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 188, DateTimeKind.Utc).AddTicks(8233), "", false, "", null, null, null, "" },
                    { 168, "SDG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 189, DateTimeKind.Utc).AddTicks(7119), "", false, "", null, null, null, "" },
                    { 169, "SDP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 190, DateTimeKind.Utc).AddTicks(6251), "", false, "", null, null, null, "" },
                    { 170, "SRD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 191, DateTimeKind.Utc).AddTicks(2209), "", false, "", null, null, null, "" },
                    { 171, "SRG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 191, DateTimeKind.Utc).AddTicks(7371), "", false, "", null, null, null, "" },
                    { 172, "SZL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 192, DateTimeKind.Utc).AddTicks(3504), "", false, "", null, null, null, "" },
                    { 173, "SEK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 198, DateTimeKind.Utc).AddTicks(2131), "", false, "", null, null, null, "" },
                    { 174, "CHF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 199, DateTimeKind.Utc).AddTicks(962), "", false, "", null, null, null, "" },
                    { 175, "SYP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 199, DateTimeKind.Utc).AddTicks(6993), "", false, "", null, null, null, "" },
                    { 176, "TWD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 200, DateTimeKind.Utc).AddTicks(2522), "", false, "", null, null, null, "" },
                    { 177, "TJS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 200, DateTimeKind.Utc).AddTicks(9189), "", false, "", null, null, null, "" },
                    { 178, "TZS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 203, DateTimeKind.Utc).AddTicks(9692), "", false, "", null, null, null, "" },
                    { 179, "THB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 205, DateTimeKind.Utc).AddTicks(4429), "", false, "", null, null, null, "" },
                    { 180, "TMM", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 206, DateTimeKind.Utc).AddTicks(5363), "", false, "", null, null, null, "" },
                    { 181, "TMT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 207, DateTimeKind.Utc).AddTicks(2812), "", false, "", null, null, null, "" },
                    { 182, "TOP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 207, DateTimeKind.Utc).AddTicks(9161), "", false, "", null, null, null, "" },
                    { 183, "TTD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 208, DateTimeKind.Utc).AddTicks(5048), "", false, "", null, null, null, "" },
                    { 184, "TND", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 209, DateTimeKind.Utc).AddTicks(1157), "", false, "", null, null, null, "" },
                    { 185, "TRL", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 209, DateTimeKind.Utc).AddTicks(7703), "", false, "", null, null, null, "" },
                    { 186, "TRY", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 210, DateTimeKind.Utc).AddTicks(5423), "", false, "", null, null, null, "" },
                    { 187, "UGX", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 211, DateTimeKind.Utc).AddTicks(2837), "", false, "", null, null, null, "" },
                    { 188, "UAH", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 211, DateTimeKind.Utc).AddTicks(9674), "", false, "", null, null, null, "" },
                    { 189, "UAH", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 212, DateTimeKind.Utc).AddTicks(5514), "", false, "", null, null, null, "" },
                    { 190, "GBP", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 213, DateTimeKind.Utc).AddTicks(1182), "", false, "", null, null, null, "" },
                    { 191, "USD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 213, DateTimeKind.Utc).AddTicks(6780), "", false, "", null, null, null, "" },
                    { 192, "UYU", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 214, DateTimeKind.Utc).AddTicks(2455), "", false, "", null, null, null, "" },
                    { 193, "UZS", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 214, DateTimeKind.Utc).AddTicks(8894), "", false, "", null, null, null, "" },
                    { 194, "AED", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 215, DateTimeKind.Utc).AddTicks(6545), "", false, "", null, null, null, "" },
                    { 195, "VUV", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 216, DateTimeKind.Utc).AddTicks(3905), "", false, "", null, null, null, "" },
                    { 196, "VEB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 217, DateTimeKind.Utc).AddTicks(1089), "", false, "", null, null, null, "" },
                    { 197, "VEF", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 218, DateTimeKind.Utc).AddTicks(356), "", false, "", null, null, null, "" },
                    { 198, "VND", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 219, DateTimeKind.Utc).AddTicks(2500), "", false, "", null, null, null, "" },
                    { 199, "YER", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 220, DateTimeKind.Utc).AddTicks(2094), "", false, "", null, null, null, "" },
                    { 200, "YUN", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 221, DateTimeKind.Utc).AddTicks(2313), "", false, "", null, null, null, "" },
                    { 201, "ZMK", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 222, DateTimeKind.Utc).AddTicks(9745), "", false, "", null, null, null, "" },
                    { 202, "ZMW", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 224, DateTimeKind.Utc).AddTicks(3581), "", false, "", null, null, null, "" },
                    { 203, "ZWD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 225, DateTimeKind.Utc).AddTicks(692), "", false, "", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 35, DateTimeKind.Utc).AddTicks(2757), "Setting", false, "Setting", null, null, null, null },
                    { 400, "MKDT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 36, DateTimeKind.Utc).AddTicks(6789), "Market Data", false, "MarketData", null, null, null, null },
                    { 500, "PROJ", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 38, DateTimeKind.Utc).AddTicks(241), "Project", false, "Project", null, null, null, null },
                    { 501, "PROJCONFIG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 38, DateTimeKind.Utc).AddTicks(9335), "Project Configuration", false, "ProjectConfiguration", null, null, null, null },
                    { 502, "CONFIG", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 40, DateTimeKind.Utc).AddTicks(262), "Configuration", false, "Configuration", null, null, null, null },
                    { 503, "EXT", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 40, DateTimeKind.Utc).AddTicks(9754), "Extractor", false, "Extractor", null, null, null, null },
                    { 504, "SB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 41, DateTimeKind.Utc).AddTicks(8718), "Strategy Builder", false, "StrategyBuilder", null, null, null, null },
                    { 505, "AB", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 42, DateTimeKind.Utc).AddTicks(7511), "Assembled Builder", false, "AssembledBuilder", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 57, DateTimeKind.Utc).AddTicks(2635), "", false, "Forex", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 58, DateTimeKind.Utc).AddTicks(1126), "", false, "America", null, null, null, null },
                    { 2, "Europe", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 58, DateTimeKind.Utc).AddTicks(9038), "", false, "Europe", null, null, null, null },
                    { 3, "Asia", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5348), "", false, "Asia", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 43, DateTimeKind.Utc).AddTicks(5160), null, false, null, null, null, null, "eng" },
                    { 2, "Theme", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 44, DateTimeKind.Utc).AddTicks(2564), null, false, null, null, null, null, "Light" },
                    { 3, "Color", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 45, DateTimeKind.Utc).AddTicks(336), null, false, null, null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 45, DateTimeKind.Utc).AddTicks(9287), null, false, null, null, null, null, "" },
                    { 5, "Host", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 46, DateTimeKind.Utc).AddTicks(8298), null, false, null, null, null, null, "192.168.50.137" },
                    { 6, "Port", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 47, DateTimeKind.Utc).AddTicks(6709), null, false, null, null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 47, DateTimeKind.Utc).AddTicks(6802), "Euro vs US Dollar", false, "EURUSD", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 48, DateTimeKind.Utc).AddTicks(5217), "", false, "1 Minute", null, null, null, "1" },
                    { 2, "M5", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 49, DateTimeKind.Utc).AddTicks(3381), "", false, "5 Minute", null, null, null, "5" },
                    { 3, "M15", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 50, DateTimeKind.Utc).AddTicks(1098), "", false, "15 Minute", null, null, null, "15" },
                    { 4, "M30", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 51, DateTimeKind.Utc).AddTicks(2886), "", false, "30 Minute", null, null, null, "30" },
                    { 5, "H1", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 52, DateTimeKind.Utc).AddTicks(8372), "", false, "1 Hour", null, null, null, "16385" },
                    { 6, "H4", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 54, DateTimeKind.Utc).AddTicks(1956), "", false, "4 Hour", null, null, null, "16388" },
                    { 7, "D1", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 55, DateTimeKind.Utc).AddTicks(1293), "", false, "Daily", null, null, null, "16408" },
                    { 8, "W1", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 55, DateTimeKind.Utc).AddTicks(8164), "", false, "Weekly", null, null, null, "32769" },
                    { 9, "MN1", "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 56, DateTimeKind.Utc).AddTicks(5438), "", false, "Monthly", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "ConfigurationId", "ABMinImprovePercent", "ABTransactionsTarget", "ABWekaMaxRatioTree", "ABWekaNTotalTree", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "Description", "EndDate", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxProgressivenessVariation", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "SBMaxCorrelationPercent", "SBMaxSuccessRateVariation", "SBMinSuccessRatePercentIS", "SBMinSuccessRatePercentOS", "SBMinTransactionsIS", "SBMinTransactionsOS", "SBTransactionsTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "StartDate", "SymbolId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 600, 2m, 500m, "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5450), 6, "Default Configuration", null, 50, null, null, false, false, 2m, 1.5m, 1000000, 100, 300m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5446), 1, 5, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "ScheduleConfiguration",
                columns: new[] { "ScheduleConfigurationId", "ConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "IsDeleted", "MarketRegionId", "StartDate", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 1, "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5613), "Default Schedule America", null, 54000, false, 1, new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5611), 82800, null, null, null },
                    { 2, 1, "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5636), "Default Schedule Europe", null, 32400, false, 2, new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5635), 64800, null, null, null },
                    { 3, 1, "0000", "admin", new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5640), "Default Schedule Asia", null, 3600, false, 3, new DateTime(2023, 6, 10, 13, 17, 26, 59, DateTimeKind.Utc).AddTicks(5639), 32400, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_SymbolId",
                table: "Configuration",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_TimeframeId",
                table: "Configuration",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityServiceHost_EntityTypeId",
                table: "EntityServiceHost",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_MagicNumber",
                table: "ExpertAdvisor",
                column: "MagicNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_Name",
                table: "ExpertAdvisor",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_ProjectId",
                table: "ExpertAdvisor",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_PublisherPort",
                table: "ExpertAdvisor",
                column: "PublisherPort",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_ResponsePort",
                table: "ExpertAdvisor",
                column: "ResponsePort",
                unique: true);

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
                name: "IX_Project_ProjectName",
                table: "Project",
                column: "ProjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_HistoricalDataId",
                table: "ProjectConfiguration",
                column: "HistoricalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_ProjectId",
                table: "ProjectConfiguration",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_SymbolId",
                table: "ProjectConfiguration",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_TimeframeId",
                table: "ProjectConfiguration",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleConfiguration_MarketRegionId",
                table: "ProjectScheduleConfiguration",
                column: "MarketRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleConfiguration_ProjectConfigurationId",
                table: "ProjectScheduleConfiguration",
                column: "ProjectConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleConfiguration_ConfigurationId",
                table: "ScheduleConfiguration",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleConfiguration_MarketRegionId",
                table: "ScheduleConfiguration",
                column: "MarketRegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "EntityServiceHost");

            migrationBuilder.DropTable(
                name: "ExpertAdvisor");

            migrationBuilder.DropTable(
                name: "HistoricalDataCandle");

            migrationBuilder.DropTable(
                name: "ProjectScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "ScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.DropTable(
                name: "ProjectConfiguration");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "MarketRegion");

            migrationBuilder.DropTable(
                name: "HistoricalData");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "Timeframe");
        }
    }
}
