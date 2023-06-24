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
                    { 1, "AFN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 298, DateTimeKind.Utc).AddTicks(1514), "", false, "", null, null, null, "" },
                    { 2, "AFA", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 298, DateTimeKind.Utc).AddTicks(5486), "", false, "", null, null, null, "" },
                    { 3, "ALL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 298, DateTimeKind.Utc).AddTicks(9502), "", false, "", null, null, null, "" },
                    { 4, "DZD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 299, DateTimeKind.Utc).AddTicks(3464), "", false, "", null, null, null, "" },
                    { 5, "ADF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 299, DateTimeKind.Utc).AddTicks(7540), "", false, "", null, null, null, "" },
                    { 6, "ADP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 300, DateTimeKind.Utc).AddTicks(1617), "", false, "", null, null, null, "" },
                    { 7, "AOA", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 301, DateTimeKind.Utc).AddTicks(5623), "", false, "", null, null, null, "" },
                    { 8, "AON", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 302, DateTimeKind.Utc).AddTicks(1620), "", false, "", null, null, null, "" },
                    { 9, "ARS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 302, DateTimeKind.Utc).AddTicks(5572), "", false, "", null, null, null, "" },
                    { 10, "AMD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 302, DateTimeKind.Utc).AddTicks(9262), "", false, "", null, null, null, "" },
                    { 11, "AWG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 303, DateTimeKind.Utc).AddTicks(2904), "", false, "", null, null, null, "" },
                    { 12, "AUD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 303, DateTimeKind.Utc).AddTicks(6886), "", false, "", null, null, null, "" },
                    { 13, "ATS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 304, DateTimeKind.Utc).AddTicks(1420), "", false, "", null, null, null, "" },
                    { 14, "AZM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 304, DateTimeKind.Utc).AddTicks(5293), "", false, "", null, null, null, "" },
                    { 15, "AZN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 304, DateTimeKind.Utc).AddTicks(8932), "", false, "", null, null, null, "" },
                    { 16, "BSD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 305, DateTimeKind.Utc).AddTicks(2607), "", false, "", null, null, null, "" },
                    { 17, "BHD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 305, DateTimeKind.Utc).AddTicks(6827), "", false, "", null, null, null, "" },
                    { 18, "BDT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 306, DateTimeKind.Utc).AddTicks(1178), "", false, "", null, null, null, "" },
                    { 19, "BBD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 306, DateTimeKind.Utc).AddTicks(5328), "", false, "", null, null, null, "" },
                    { 20, "BEF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 306, DateTimeKind.Utc).AddTicks(9112), "", false, "", null, null, null, "" },
                    { 21, "BZD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 307, DateTimeKind.Utc).AddTicks(2753), "", false, "", null, null, null, "" },
                    { 22, "BMD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 307, DateTimeKind.Utc).AddTicks(6628), "", false, "", null, null, null, "" },
                    { 23, "BTN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 308, DateTimeKind.Utc).AddTicks(351), "", false, "", null, null, null, "" },
                    { 24, "BOB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 308, DateTimeKind.Utc).AddTicks(4032), "", false, "", null, null, null, "" },
                    { 25, "BAM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 309, DateTimeKind.Utc).AddTicks(910), "", false, "", null, null, null, "" },
                    { 26, "BWP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 309, DateTimeKind.Utc).AddTicks(5897), "", false, "", null, null, null, "" },
                    { 27, "BRL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 310, DateTimeKind.Utc).AddTicks(685), "", false, "", null, null, null, "" },
                    { 28, "GBP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 310, DateTimeKind.Utc).AddTicks(5609), "", false, "", null, null, null, "" },
                    { 29, "BND", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 311, DateTimeKind.Utc).AddTicks(233), "", false, "", null, null, null, "" },
                    { 30, "BGN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 311, DateTimeKind.Utc).AddTicks(4700), "", false, "", null, null, null, "" },
                    { 31, "BGL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 311, DateTimeKind.Utc).AddTicks(9171), "", false, "", null, null, null, "" },
                    { 32, "BIF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 312, DateTimeKind.Utc).AddTicks(3621), "", false, "", null, null, null, "" },
                    { 33, "BYR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 312, DateTimeKind.Utc).AddTicks(8062), "", false, "", null, null, null, "" },
                    { 34, "XOF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 313, DateTimeKind.Utc).AddTicks(2462), "", false, "", null, null, null, "" },
                    { 35, "XAF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 313, DateTimeKind.Utc).AddTicks(6877), "", false, "", null, null, null, "" },
                    { 36, "XPF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 314, DateTimeKind.Utc).AddTicks(1286), "", false, "", null, null, null, "" },
                    { 37, "KHR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 314, DateTimeKind.Utc).AddTicks(5860), "", false, "", null, null, null, "" },
                    { 38, "CAD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 315, DateTimeKind.Utc).AddTicks(288), "", false, "", null, null, null, "" },
                    { 39, "CVE", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 315, DateTimeKind.Utc).AddTicks(4740), "", false, "", null, null, null, "" },
                    { 40, "KYD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 315, DateTimeKind.Utc).AddTicks(9241), "", false, "", null, null, null, "" },
                    { 41, "CLP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 316, DateTimeKind.Utc).AddTicks(3687), "", false, "", null, null, null, "" },
                    { 42, "CNY", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 316, DateTimeKind.Utc).AddTicks(8214), "", false, "", null, null, null, "" },
                    { 43, "COP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 317, DateTimeKind.Utc).AddTicks(2687), "", false, "", null, null, null, "" },
                    { 44, "KMF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 317, DateTimeKind.Utc).AddTicks(7137), "", false, "", null, null, null, "" },
                    { 45, "CDF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 318, DateTimeKind.Utc).AddTicks(1547), "", false, "", null, null, null, "" },
                    { 46, "CRC", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 318, DateTimeKind.Utc).AddTicks(6022), "", false, "", null, null, null, "" },
                    { 47, "HRK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 319, DateTimeKind.Utc).AddTicks(435), "", false, "", null, null, null, "" },
                    { 48, "CUC", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 319, DateTimeKind.Utc).AddTicks(4840), "", false, "", null, null, null, "" },
                    { 49, "CUP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 319, DateTimeKind.Utc).AddTicks(9208), "", false, "", null, null, null, "" },
                    { 50, "CYP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 320, DateTimeKind.Utc).AddTicks(3583), "", false, "", null, null, null, "" },
                    { 51, "CZK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 320, DateTimeKind.Utc).AddTicks(7939), "", false, "", null, null, null, "" },
                    { 52, "DKK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 321, DateTimeKind.Utc).AddTicks(2412), "", false, "", null, null, null, "" },
                    { 53, "DJF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 321, DateTimeKind.Utc).AddTicks(6853), "", false, "", null, null, null, "" },
                    { 54, "DOP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 322, DateTimeKind.Utc).AddTicks(1100), "", false, "", null, null, null, "" },
                    { 55, "NLG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 322, DateTimeKind.Utc).AddTicks(5534), "", false, "", null, null, null, "" },
                    { 56, "XCD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 322, DateTimeKind.Utc).AddTicks(9713), "", false, "", null, null, null, "" },
                    { 57, "XEU", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 323, DateTimeKind.Utc).AddTicks(3876), "", false, "", null, null, null, "" },
                    { 58, "ECS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 323, DateTimeKind.Utc).AddTicks(8172), "", false, "", null, null, null, "" },
                    { 59, "EGP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 324, DateTimeKind.Utc).AddTicks(2432), "", false, "", null, null, null, "" },
                    { 60, "SVC", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 324, DateTimeKind.Utc).AddTicks(7794), "", false, "", null, null, null, "" },
                    { 61, "EEK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 325, DateTimeKind.Utc).AddTicks(2624), "", false, "", null, null, null, "" },
                    { 62, "ETB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 325, DateTimeKind.Utc).AddTicks(7023), "", false, "", null, null, null, "" },
                    { 63, "EUR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 326, DateTimeKind.Utc).AddTicks(1302), "", false, "", null, null, null, "" },
                    { 64, "FKP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 326, DateTimeKind.Utc).AddTicks(5507), "", false, "", null, null, null, "" },
                    { 65, "FJD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 326, DateTimeKind.Utc).AddTicks(9634), "", false, "", null, null, null, "" },
                    { 66, "FIM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 327, DateTimeKind.Utc).AddTicks(3779), "", false, "", null, null, null, "" },
                    { 67, "FRF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 327, DateTimeKind.Utc).AddTicks(7991), "", false, "", null, null, null, "" },
                    { 68, "GMD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 328, DateTimeKind.Utc).AddTicks(2195), "", false, "", null, null, null, "" },
                    { 69, "GEL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 328, DateTimeKind.Utc).AddTicks(6423), "", false, "", null, null, null, "" },
                    { 70, "DEM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 329, DateTimeKind.Utc).AddTicks(581), "", false, "", null, null, null, "" },
                    { 71, "GHC", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 329, DateTimeKind.Utc).AddTicks(4786), "", false, "", null, null, null, "" },
                    { 72, "GHS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 329, DateTimeKind.Utc).AddTicks(9028), "", false, "", null, null, null, "" },
                    { 73, "GIP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 330, DateTimeKind.Utc).AddTicks(3220), "", false, "", null, null, null, "" },
                    { 74, "XAU", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 330, DateTimeKind.Utc).AddTicks(7395), "", false, "", null, null, null, "" },
                    { 75, "GRD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 331, DateTimeKind.Utc).AddTicks(1587), "", false, "", null, null, null, "" },
                    { 76, "GTQ", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 331, DateTimeKind.Utc).AddTicks(5912), "", false, "", null, null, null, "" },
                    { 77, "GNF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 332, DateTimeKind.Utc).AddTicks(541), "", false, "", null, null, null, "" },
                    { 78, "GYD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 332, DateTimeKind.Utc).AddTicks(4829), "", false, "", null, null, null, "" },
                    { 79, "HTG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 332, DateTimeKind.Utc).AddTicks(9121), "", false, "", null, null, null, "" },
                    { 80, "HNL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 333, DateTimeKind.Utc).AddTicks(3369), "", false, "", null, null, null, "" },
                    { 81, "HKD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 333, DateTimeKind.Utc).AddTicks(7670), "", false, "", null, null, null, "" },
                    { 82, "HUF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 334, DateTimeKind.Utc).AddTicks(1975), "", false, "", null, null, null, "" },
                    { 83, "ISK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 334, DateTimeKind.Utc).AddTicks(6262), "", false, "", null, null, null, "" },
                    { 84, "INR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 335, DateTimeKind.Utc).AddTicks(555), "", false, "", null, null, null, "" },
                    { 85, "IDR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 335, DateTimeKind.Utc).AddTicks(4831), "", false, "", null, null, null, "" },
                    { 86, "IRR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 335, DateTimeKind.Utc).AddTicks(9103), "", false, "", null, null, null, "" },
                    { 87, "IQD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 336, DateTimeKind.Utc).AddTicks(3379), "", false, "", null, null, null, "" },
                    { 88, "IEP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 336, DateTimeKind.Utc).AddTicks(7621), "", false, "", null, null, null, "" },
                    { 89, "ILS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 337, DateTimeKind.Utc).AddTicks(2354), "", false, "", null, null, null, "" },
                    { 90, "ITL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 337, DateTimeKind.Utc).AddTicks(6784), "", false, "", null, null, null, "" },
                    { 91, "JMD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 338, DateTimeKind.Utc).AddTicks(1066), "", false, "", null, null, null, "" },
                    { 92, "JPY", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 338, DateTimeKind.Utc).AddTicks(5403), "", false, "", null, null, null, "" },
                    { 93, "JOD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 338, DateTimeKind.Utc).AddTicks(9662), "", false, "", null, null, null, "" },
                    { 94, "KHR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 339, DateTimeKind.Utc).AddTicks(4480), "", false, "", null, null, null, "" },
                    { 95, "KZT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 339, DateTimeKind.Utc).AddTicks(8877), "", false, "", null, null, null, "" },
                    { 96, "KES", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 340, DateTimeKind.Utc).AddTicks(3124), "", false, "", null, null, null, "" },
                    { 97, "KRW", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 340, DateTimeKind.Utc).AddTicks(7736), "", false, "", null, null, null, "" },
                    { 98, "KWD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 341, DateTimeKind.Utc).AddTicks(1514), "", false, "", null, null, null, "" },
                    { 99, "KGS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 341, DateTimeKind.Utc).AddTicks(5099), "", false, "", null, null, null, "" },
                    { 100, "LAK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 341, DateTimeKind.Utc).AddTicks(8620), "", false, "", null, null, null, "" },
                    { 101, "LVL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 342, DateTimeKind.Utc).AddTicks(2095), "", false, "", null, null, null, "" },
                    { 102, "LBP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 342, DateTimeKind.Utc).AddTicks(5638), "", false, "", null, null, null, "" },
                    { 103, "LSL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 342, DateTimeKind.Utc).AddTicks(9088), "", false, "", null, null, null, "" },
                    { 104, "LRD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 343, DateTimeKind.Utc).AddTicks(2643), "", false, "", null, null, null, "" },
                    { 105, "LYD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 343, DateTimeKind.Utc).AddTicks(6138), "", false, "", null, null, null, "" },
                    { 106, "LTL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 343, DateTimeKind.Utc).AddTicks(9700), "", false, "", null, null, null, "" },
                    { 107, "LUF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 344, DateTimeKind.Utc).AddTicks(3163), "", false, "", null, null, null, "" },
                    { 108, "MOP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 344, DateTimeKind.Utc).AddTicks(6662), "", false, "", null, null, null, "" },
                    { 109, "MKD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 345, DateTimeKind.Utc).AddTicks(120), "", false, "", null, null, null, "" },
                    { 110, "MGF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 345, DateTimeKind.Utc).AddTicks(3655), "", false, "", null, null, null, "" },
                    { 111, "MWK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 345, DateTimeKind.Utc).AddTicks(7137), "", false, "", null, null, null, "" },
                    { 112, "MGA", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 346, DateTimeKind.Utc).AddTicks(596), "", false, "", null, null, null, "" },
                    { 113, "MYR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 346, DateTimeKind.Utc).AddTicks(4028), "", false, "", null, null, null, "" },
                    { 114, "MVR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 346, DateTimeKind.Utc).AddTicks(7528), "", false, "", null, null, null, "" },
                    { 115, "MTL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 347, DateTimeKind.Utc).AddTicks(960), "", false, "", null, null, null, "" },
                    { 116, "MRO", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 347, DateTimeKind.Utc).AddTicks(4472), "", false, "", null, null, null, "" },
                    { 117, "MUR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 347, DateTimeKind.Utc).AddTicks(7967), "", false, "", null, null, null, "" },
                    { 118, "MXN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 348, DateTimeKind.Utc).AddTicks(1473), "", false, "", null, null, null, "" },
                    { 119, "MDL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 348, DateTimeKind.Utc).AddTicks(4926), "", false, "", null, null, null, "" },
                    { 120, "MNT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 348, DateTimeKind.Utc).AddTicks(8340), "", false, "", null, null, null, "" },
                    { 121, "MAD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 349, DateTimeKind.Utc).AddTicks(1772), "", false, "", null, null, null, "" },
                    { 122, "MZM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 349, DateTimeKind.Utc).AddTicks(5283), "", false, "", null, null, null, "" },
                    { 123, "MZN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 349, DateTimeKind.Utc).AddTicks(8717), "", false, "", null, null, null, "" },
                    { 124, "MMK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 350, DateTimeKind.Utc).AddTicks(2177), "", false, "", null, null, null, "" },
                    { 125, "ANG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 350, DateTimeKind.Utc).AddTicks(5673), "", false, "", null, null, null, "" },
                    { 126, "NAD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 350, DateTimeKind.Utc).AddTicks(9249), "", false, "", null, null, null, "" },
                    { 127, "NPR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 351, DateTimeKind.Utc).AddTicks(2884), "", false, "", null, null, null, "" },
                    { 128, "NLG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 351, DateTimeKind.Utc).AddTicks(6379), "", false, "", null, null, null, "" },
                    { 129, "NZD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 351, DateTimeKind.Utc).AddTicks(9878), "", false, "", null, null, null, "" },
                    { 130, "NIO", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 352, DateTimeKind.Utc).AddTicks(3360), "", false, "", null, null, null, "" },
                    { 131, "NGN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 352, DateTimeKind.Utc).AddTicks(6901), "", false, "", null, null, null, "" },
                    { 132, "KPW", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 353, DateTimeKind.Utc).AddTicks(374), "", false, "", null, null, null, "" },
                    { 133, "NOK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 353, DateTimeKind.Utc).AddTicks(3856), "", false, "", null, null, null, "" },
                    { 134, "OMR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 353, DateTimeKind.Utc).AddTicks(7321), "", false, "", null, null, null, "" },
                    { 135, "PKR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 354, DateTimeKind.Utc).AddTicks(786), "", false, "", null, null, null, "" },
                    { 136, "XPD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 354, DateTimeKind.Utc).AddTicks(4320), "", false, "", null, null, null, "" },
                    { 137, "PAB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 354, DateTimeKind.Utc).AddTicks(7723), "", false, "", null, null, null, "" },
                    { 138, "PGK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 355, DateTimeKind.Utc).AddTicks(1128), "", false, "", null, null, null, "" },
                    { 139, "PYG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 355, DateTimeKind.Utc).AddTicks(4575), "", false, "", null, null, null, "" },
                    { 140, "PEN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 355, DateTimeKind.Utc).AddTicks(7961), "", false, "", null, null, null, "" },
                    { 141, "PHP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 356, DateTimeKind.Utc).AddTicks(1377), "", false, "", null, null, null, "" },
                    { 142, "XPT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 356, DateTimeKind.Utc).AddTicks(6063), "", false, "", null, null, null, "" },
                    { 143, "Mexico", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 357, DateTimeKind.Utc).AddTicks(1905), "", false, "", null, null, null, "" },
                    { 144, "PLN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 357, DateTimeKind.Utc).AddTicks(6580), "", false, "", null, null, null, "" },
                    { 145, "PTE", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 358, DateTimeKind.Utc).AddTicks(893), "", false, "", null, null, null, "" },
                    { 146, "GBP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 358, DateTimeKind.Utc).AddTicks(5203), "", false, "", null, null, null, "" },
                    { 147, "ROL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 358, DateTimeKind.Utc).AddTicks(9474), "", false, "", null, null, null, "" },
                    { 148, "RON", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 359, DateTimeKind.Utc).AddTicks(3731), "", false, "", null, null, null, "" },
                    { 149, "RUB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 359, DateTimeKind.Utc).AddTicks(8069), "", false, "", null, null, null, "" },
                    { 150, "RWF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 360, DateTimeKind.Utc).AddTicks(2337), "", false, "", null, null, null, "" },
                    { 151, "WST", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 360, DateTimeKind.Utc).AddTicks(6665), "", false, "", null, null, null, "" },
                    { 152, "STD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 361, DateTimeKind.Utc).AddTicks(875), "", false, "", null, null, null, "" },
                    { 153, "SAR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 361, DateTimeKind.Utc).AddTicks(5107), "", false, "", null, null, null, "" },
                    { 154, "RSD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 361, DateTimeKind.Utc).AddTicks(9569), "", false, "", null, null, null, "" },
                    { 155, "SCR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 362, DateTimeKind.Utc).AddTicks(3836), "", false, "", null, null, null, "" },
                    { 156, "SLL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 362, DateTimeKind.Utc).AddTicks(8162), "", false, "", null, null, null, "" },
                    { 157, "XAG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 363, DateTimeKind.Utc).AddTicks(2463), "", false, "", null, null, null, "" },
                    { 158, "SGD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 363, DateTimeKind.Utc).AddTicks(6867), "", false, "", null, null, null, "" },
                    { 159, "SKK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 368, DateTimeKind.Utc).AddTicks(1555), "", false, "", null, null, null, "" },
                    { 160, "SIT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 368, DateTimeKind.Utc).AddTicks(6713), "", false, "", null, null, null, "" },
                    { 161, "SBD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 369, DateTimeKind.Utc).AddTicks(1099), "", false, "", null, null, null, "" },
                    { 162, "SOS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 369, DateTimeKind.Utc).AddTicks(5479), "", false, "", null, null, null, "" },
                    { 163, "ZAR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 369, DateTimeKind.Utc).AddTicks(9854), "", false, "", null, null, null, "" },
                    { 164, "ESP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 370, DateTimeKind.Utc).AddTicks(4269), "", false, "", null, null, null, "" },
                    { 165, "LKR", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 370, DateTimeKind.Utc).AddTicks(8625), "", false, "", null, null, null, "" },
                    { 166, "SHP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 371, DateTimeKind.Utc).AddTicks(3014), "", false, "", null, null, null, "" },
                    { 167, "SDD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 371, DateTimeKind.Utc).AddTicks(7457), "", false, "", null, null, null, "" },
                    { 168, "SDG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 372, DateTimeKind.Utc).AddTicks(1916), "", false, "", null, null, null, "" },
                    { 169, "SDP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 372, DateTimeKind.Utc).AddTicks(8009), "", false, "", null, null, null, "" },
                    { 170, "SRD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 373, DateTimeKind.Utc).AddTicks(3479), "", false, "", null, null, null, "" },
                    { 171, "SRG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 373, DateTimeKind.Utc).AddTicks(8302), "", false, "", null, null, null, "" },
                    { 172, "SZL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 374, DateTimeKind.Utc).AddTicks(2893), "", false, "", null, null, null, "" },
                    { 173, "SEK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 374, DateTimeKind.Utc).AddTicks(7473), "", false, "", null, null, null, "" },
                    { 174, "CHF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 375, DateTimeKind.Utc).AddTicks(1979), "", false, "", null, null, null, "" },
                    { 175, "SYP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 376, DateTimeKind.Utc).AddTicks(5342), "", false, "", null, null, null, "" },
                    { 176, "TWD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 377, DateTimeKind.Utc).AddTicks(1781), "", false, "", null, null, null, "" },
                    { 177, "TJS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 377, DateTimeKind.Utc).AddTicks(6779), "", false, "", null, null, null, "" },
                    { 178, "TZS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 378, DateTimeKind.Utc).AddTicks(1368), "", false, "", null, null, null, "" },
                    { 179, "THB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 379, DateTimeKind.Utc).AddTicks(3726), "", false, "", null, null, null, "" },
                    { 180, "TMM", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 379, DateTimeKind.Utc).AddTicks(9394), "", false, "", null, null, null, "" },
                    { 181, "TMT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 380, DateTimeKind.Utc).AddTicks(3950), "", false, "", null, null, null, "" },
                    { 182, "TOP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 380, DateTimeKind.Utc).AddTicks(8455), "", false, "", null, null, null, "" },
                    { 183, "TTD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 381, DateTimeKind.Utc).AddTicks(2964), "", false, "", null, null, null, "" },
                    { 184, "TND", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 381, DateTimeKind.Utc).AddTicks(7708), "", false, "", null, null, null, "" },
                    { 185, "TRL", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 382, DateTimeKind.Utc).AddTicks(2271), "", false, "", null, null, null, "" },
                    { 186, "TRY", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 382, DateTimeKind.Utc).AddTicks(7120), "", false, "", null, null, null, "" },
                    { 187, "UGX", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 383, DateTimeKind.Utc).AddTicks(1754), "", false, "", null, null, null, "" },
                    { 188, "UAH", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 383, DateTimeKind.Utc).AddTicks(6233), "", false, "", null, null, null, "" },
                    { 189, "UAH", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 384, DateTimeKind.Utc).AddTicks(466), "", false, "", null, null, null, "" },
                    { 190, "GBP", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 384, DateTimeKind.Utc).AddTicks(4845), "", false, "", null, null, null, "" },
                    { 191, "USD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 384, DateTimeKind.Utc).AddTicks(9312), "", false, "", null, null, null, "" },
                    { 192, "UYU", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 385, DateTimeKind.Utc).AddTicks(3663), "", false, "", null, null, null, "" },
                    { 193, "UZS", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 385, DateTimeKind.Utc).AddTicks(8182), "", false, "", null, null, null, "" },
                    { 194, "AED", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 386, DateTimeKind.Utc).AddTicks(2563), "", false, "", null, null, null, "" },
                    { 195, "VUV", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 386, DateTimeKind.Utc).AddTicks(7241), "", false, "", null, null, null, "" },
                    { 196, "VEB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 387, DateTimeKind.Utc).AddTicks(2078), "", false, "", null, null, null, "" },
                    { 197, "VEF", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 387, DateTimeKind.Utc).AddTicks(7232), "", false, "", null, null, null, "" },
                    { 198, "VND", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 388, DateTimeKind.Utc).AddTicks(1831), "", false, "", null, null, null, "" },
                    { 199, "YER", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 388, DateTimeKind.Utc).AddTicks(8975), "", false, "", null, null, null, "" },
                    { 200, "YUN", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 389, DateTimeKind.Utc).AddTicks(4829), "", false, "", null, null, null, "" },
                    { 201, "ZMK", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 389, DateTimeKind.Utc).AddTicks(9716), "", false, "", null, null, null, "" },
                    { 202, "ZMW", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 390, DateTimeKind.Utc).AddTicks(4061), "", false, "", null, null, null, "" },
                    { 203, "ZWD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 390, DateTimeKind.Utc).AddTicks(7722), "", false, "", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 285, DateTimeKind.Utc).AddTicks(1414), "Setting", false, "Setting", null, null, null, null },
                    { 400, "MKDT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 285, DateTimeKind.Utc).AddTicks(7145), "Market Data", false, "MarketData", null, null, null, null },
                    { 500, "PROJ", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 286, DateTimeKind.Utc).AddTicks(2026), "Project", false, "Project", null, null, null, null },
                    { 501, "PROJCONFIG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 286, DateTimeKind.Utc).AddTicks(7108), "Project Configuration", false, "ProjectConfiguration", null, null, null, null },
                    { 502, "CONFIG", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 287, DateTimeKind.Utc).AddTicks(2234), "Configuration", false, "Configuration", null, null, null, null },
                    { 503, "EXT", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 287, DateTimeKind.Utc).AddTicks(7647), "Extractor", false, "Extractor", null, null, null, null },
                    { 504, "SB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 288, DateTimeKind.Utc).AddTicks(3623), "Strategy Builder", false, "StrategyBuilder", null, null, null, null },
                    { 505, "AB", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 288, DateTimeKind.Utc).AddTicks(8670), "Assembled Builder", false, "AssembledBuilder", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 296, DateTimeKind.Utc).AddTicks(3621), "", false, "Forex", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 296, DateTimeKind.Utc).AddTicks(8345), "", false, "America", null, null, null, null },
                    { 2, "Europe", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(2269), "", false, "Europe", null, null, null, null },
                    { 3, "Asia", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6191), "", false, "Asia", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 289, DateTimeKind.Utc).AddTicks(3285), null, false, null, null, null, null, "eng" },
                    { 2, "Theme", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 289, DateTimeKind.Utc).AddTicks(7789), null, false, null, null, null, null, "Light" },
                    { 3, "Color", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 290, DateTimeKind.Utc).AddTicks(2088), null, false, null, null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 290, DateTimeKind.Utc).AddTicks(6195), null, false, null, null, null, null, "" },
                    { 5, "Host", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 291, DateTimeKind.Utc).AddTicks(313), null, false, null, null, null, null, "192.168.50.137" },
                    { 6, "Port", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 291, DateTimeKind.Utc).AddTicks(4200), null, false, null, null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 291, DateTimeKind.Utc).AddTicks(4249), "Euro vs US Dollar", false, "EURUSD", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 291, DateTimeKind.Utc).AddTicks(8175), "", false, "1 Minute", null, null, null, "1" },
                    { 2, "M5", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 292, DateTimeKind.Utc).AddTicks(1870), "", false, "5 Minute", null, null, null, "5" },
                    { 3, "M15", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 292, DateTimeKind.Utc).AddTicks(7110), "", false, "15 Minute", null, null, null, "15" },
                    { 4, "M30", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 293, DateTimeKind.Utc).AddTicks(3894), "", false, "30 Minute", null, null, null, "30" },
                    { 5, "H1", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 293, DateTimeKind.Utc).AddTicks(9287), "", false, "1 Hour", null, null, null, "16385" },
                    { 6, "H4", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 294, DateTimeKind.Utc).AddTicks(3303), "", false, "4 Hour", null, null, null, "16388" },
                    { 7, "D1", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 294, DateTimeKind.Utc).AddTicks(7181), "", false, "Daily", null, null, null, "16408" },
                    { 8, "W1", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 295, DateTimeKind.Utc).AddTicks(1170), "", false, "Weekly", null, null, null, "32769" },
                    { 9, "MN1", "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 295, DateTimeKind.Utc).AddTicks(7546), "", false, "Monthly", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "ConfigurationId", "ABMinImprovePercent", "ABTransactionsTarget", "ABWekaMaxRatioTree", "ABWekaNTotalTree", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "Description", "EndDate", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxProgressivenessVariation", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "SBMaxCorrelationPercent", "SBMaxSuccessRateVariation", "SBMinSuccessRatePercentIS", "SBMinSuccessRatePercentOS", "SBMinTransactionsIS", "SBMinTransactionsOS", "SBTransactionsTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "StartDate", "SymbolId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 600, 2m, 500m, "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6260), 6, "Default Configuration", null, 50, null, null, false, false, 2m, 1.5m, 1000000, 100, 300m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6257), 1, 5, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "ScheduleConfiguration",
                columns: new[] { "ScheduleConfigurationId", "ConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "IsDeleted", "MarketRegionId", "StartDate", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 1, "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6297), "Default Schedule America", null, 54000, false, 1, new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6296), 82800, null, null, null },
                    { 2, 1, "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6302), "Default Schedule Europe", null, 32400, false, 2, new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6300), 64800, null, null, null },
                    { 3, 1, "0000", "admin", new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6305), "Default Schedule Asia", null, 3600, false, 3, new DateTime(2023, 6, 24, 19, 11, 18, 297, DateTimeKind.Utc).AddTicks(6303), 32400, null, null, null }
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
