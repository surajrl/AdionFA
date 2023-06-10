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
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false)
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
                    ABMinImprovePercent = table.Column<decimal>(type: "TEXT", nullable: false)
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
                    { 1, "AFN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 109, DateTimeKind.Utc).AddTicks(2025), "", false, "", null, null, null, "" },
                    { 2, "AFA", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 109, DateTimeKind.Utc).AddTicks(9088), "", false, "", null, null, null, "" },
                    { 3, "ALL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 110, DateTimeKind.Utc).AddTicks(6738), "", false, "", null, null, null, "" },
                    { 4, "DZD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 111, DateTimeKind.Utc).AddTicks(3788), "", false, "", null, null, null, "" },
                    { 5, "ADF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 112, DateTimeKind.Utc).AddTicks(1099), "", false, "", null, null, null, "" },
                    { 6, "ADP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 112, DateTimeKind.Utc).AddTicks(8014), "", false, "", null, null, null, "" },
                    { 7, "AOA", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 113, DateTimeKind.Utc).AddTicks(3068), "", false, "", null, null, null, "" },
                    { 8, "AON", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 113, DateTimeKind.Utc).AddTicks(8103), "", false, "", null, null, null, "" },
                    { 9, "ARS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 114, DateTimeKind.Utc).AddTicks(3093), "", false, "", null, null, null, "" },
                    { 10, "AMD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 114, DateTimeKind.Utc).AddTicks(8032), "", false, "", null, null, null, "" },
                    { 11, "AWG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 115, DateTimeKind.Utc).AddTicks(2892), "", false, "", null, null, null, "" },
                    { 12, "AUD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 115, DateTimeKind.Utc).AddTicks(8237), "", false, "", null, null, null, "" },
                    { 13, "ATS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 116, DateTimeKind.Utc).AddTicks(3330), "", false, "", null, null, null, "" },
                    { 14, "AZM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 116, DateTimeKind.Utc).AddTicks(8307), "", false, "", null, null, null, "" },
                    { 15, "AZN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 117, DateTimeKind.Utc).AddTicks(3683), "", false, "", null, null, null, "" },
                    { 16, "BSD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 117, DateTimeKind.Utc).AddTicks(9104), "", false, "", null, null, null, "" },
                    { 17, "BHD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 118, DateTimeKind.Utc).AddTicks(4080), "", false, "", null, null, null, "" },
                    { 18, "BDT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 118, DateTimeKind.Utc).AddTicks(9133), "", false, "", null, null, null, "" },
                    { 19, "BBD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 119, DateTimeKind.Utc).AddTicks(4303), "", false, "", null, null, null, "" },
                    { 20, "BEF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 119, DateTimeKind.Utc).AddTicks(9654), "", false, "", null, null, null, "" },
                    { 21, "BZD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 120, DateTimeKind.Utc).AddTicks(4663), "", false, "", null, null, null, "" },
                    { 22, "BMD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 120, DateTimeKind.Utc).AddTicks(9752), "", false, "", null, null, null, "" },
                    { 23, "BTN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 121, DateTimeKind.Utc).AddTicks(4774), "", false, "", null, null, null, "" },
                    { 24, "BOB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 123, DateTimeKind.Utc).AddTicks(5246), "", false, "", null, null, null, "" },
                    { 25, "BAM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 124, DateTimeKind.Utc).AddTicks(4778), "", false, "", null, null, null, "" },
                    { 26, "BWP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 125, DateTimeKind.Utc).AddTicks(3774), "", false, "", null, null, null, "" },
                    { 27, "BRL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 126, DateTimeKind.Utc).AddTicks(1781), "", false, "", null, null, null, "" },
                    { 28, "GBP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 127, DateTimeKind.Utc).AddTicks(831), "", false, "", null, null, null, "" },
                    { 29, "BND", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 127, DateTimeKind.Utc).AddTicks(6117), "", false, "", null, null, null, "" },
                    { 30, "BGN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 128, DateTimeKind.Utc).AddTicks(1335), "", false, "", null, null, null, "" },
                    { 31, "BGL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 128, DateTimeKind.Utc).AddTicks(6300), "", false, "", null, null, null, "" },
                    { 32, "BIF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 129, DateTimeKind.Utc).AddTicks(1824), "", false, "", null, null, null, "" },
                    { 33, "BYR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 129, DateTimeKind.Utc).AddTicks(7006), "", false, "", null, null, null, "" },
                    { 34, "XOF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 130, DateTimeKind.Utc).AddTicks(2045), "", false, "", null, null, null, "" },
                    { 35, "XAF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 130, DateTimeKind.Utc).AddTicks(7040), "", false, "", null, null, null, "" },
                    { 36, "XPF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 131, DateTimeKind.Utc).AddTicks(1945), "", false, "", null, null, null, "" },
                    { 37, "KHR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 131, DateTimeKind.Utc).AddTicks(7076), "", false, "", null, null, null, "" },
                    { 38, "CAD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 132, DateTimeKind.Utc).AddTicks(2122), "", false, "", null, null, null, "" },
                    { 39, "CVE", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 132, DateTimeKind.Utc).AddTicks(7087), "", false, "", null, null, null, "" },
                    { 40, "KYD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 133, DateTimeKind.Utc).AddTicks(2198), "", false, "", null, null, null, "" },
                    { 41, "CLP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 133, DateTimeKind.Utc).AddTicks(7175), "", false, "", null, null, null, "" },
                    { 42, "CNY", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 134, DateTimeKind.Utc).AddTicks(2086), "", false, "", null, null, null, "" },
                    { 43, "COP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 134, DateTimeKind.Utc).AddTicks(7155), "", false, "", null, null, null, "" },
                    { 44, "KMF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 135, DateTimeKind.Utc).AddTicks(2275), "", false, "", null, null, null, "" },
                    { 45, "CDF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 135, DateTimeKind.Utc).AddTicks(7299), "", false, "", null, null, null, "" },
                    { 46, "CRC", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 136, DateTimeKind.Utc).AddTicks(2154), "", false, "", null, null, null, "" },
                    { 47, "HRK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 136, DateTimeKind.Utc).AddTicks(7274), "", false, "", null, null, null, "" },
                    { 48, "CUC", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 137, DateTimeKind.Utc).AddTicks(2217), "", false, "", null, null, null, "" },
                    { 49, "CUP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 138, DateTimeKind.Utc).AddTicks(2325), "", false, "", null, null, null, "" },
                    { 50, "CYP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 138, DateTimeKind.Utc).AddTicks(9032), "", false, "", null, null, null, "" },
                    { 51, "CZK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 139, DateTimeKind.Utc).AddTicks(4254), "", false, "", null, null, null, "" },
                    { 52, "DKK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 139, DateTimeKind.Utc).AddTicks(9310), "", false, "", null, null, null, "" },
                    { 53, "DJF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 140, DateTimeKind.Utc).AddTicks(4355), "", false, "", null, null, null, "" },
                    { 54, "DOP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 140, DateTimeKind.Utc).AddTicks(9537), "", false, "", null, null, null, "" },
                    { 55, "NLG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 141, DateTimeKind.Utc).AddTicks(4495), "", false, "", null, null, null, "" },
                    { 56, "XCD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 141, DateTimeKind.Utc).AddTicks(9493), "", false, "", null, null, null, "" },
                    { 57, "XEU", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 142, DateTimeKind.Utc).AddTicks(4560), "", false, "", null, null, null, "" },
                    { 58, "ECS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 142, DateTimeKind.Utc).AddTicks(9576), "", false, "", null, null, null, "" },
                    { 59, "EGP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 143, DateTimeKind.Utc).AddTicks(4541), "", false, "", null, null, null, "" },
                    { 60, "SVC", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 143, DateTimeKind.Utc).AddTicks(9532), "", false, "", null, null, null, "" },
                    { 61, "EEK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 144, DateTimeKind.Utc).AddTicks(4486), "", false, "", null, null, null, "" },
                    { 62, "ETB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 144, DateTimeKind.Utc).AddTicks(9554), "", false, "", null, null, null, "" },
                    { 63, "EUR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 145, DateTimeKind.Utc).AddTicks(4472), "", false, "", null, null, null, "" },
                    { 64, "FKP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 145, DateTimeKind.Utc).AddTicks(9558), "", false, "", null, null, null, "" },
                    { 65, "FJD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 146, DateTimeKind.Utc).AddTicks(4566), "", false, "", null, null, null, "" },
                    { 66, "FIM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 146, DateTimeKind.Utc).AddTicks(9605), "", false, "", null, null, null, "" },
                    { 67, "FRF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 147, DateTimeKind.Utc).AddTicks(4610), "", false, "", null, null, null, "" },
                    { 68, "GMD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 147, DateTimeKind.Utc).AddTicks(9610), "", false, "", null, null, null, "" },
                    { 69, "GEL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 148, DateTimeKind.Utc).AddTicks(4613), "", false, "", null, null, null, "" },
                    { 70, "DEM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 148, DateTimeKind.Utc).AddTicks(9586), "", false, "", null, null, null, "" },
                    { 71, "GHC", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 149, DateTimeKind.Utc).AddTicks(4580), "", false, "", null, null, null, "" },
                    { 72, "GHS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 150, DateTimeKind.Utc).AddTicks(92), "", false, "", null, null, null, "" },
                    { 73, "GIP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 150, DateTimeKind.Utc).AddTicks(5045), "", false, "", null, null, null, "" },
                    { 74, "XAU", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 150, DateTimeKind.Utc).AddTicks(9962), "", false, "", null, null, null, "" },
                    { 75, "GRD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 151, DateTimeKind.Utc).AddTicks(5019), "", false, "", null, null, null, "" },
                    { 76, "GTQ", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 152, DateTimeKind.Utc).AddTicks(46), "", false, "", null, null, null, "" },
                    { 77, "GNF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 152, DateTimeKind.Utc).AddTicks(5101), "", false, "", null, null, null, "" },
                    { 78, "GYD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 153, DateTimeKind.Utc).AddTicks(161), "", false, "", null, null, null, "" },
                    { 79, "HTG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 153, DateTimeKind.Utc).AddTicks(5107), "", false, "", null, null, null, "" },
                    { 80, "HNL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 154, DateTimeKind.Utc).AddTicks(4020), "", false, "", null, null, null, "" },
                    { 81, "HKD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 154, DateTimeKind.Utc).AddTicks(9699), "", false, "", null, null, null, "" },
                    { 82, "HUF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 155, DateTimeKind.Utc).AddTicks(4908), "", false, "", null, null, null, "" },
                    { 83, "ISK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 155, DateTimeKind.Utc).AddTicks(9947), "", false, "", null, null, null, "" },
                    { 84, "INR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 156, DateTimeKind.Utc).AddTicks(4944), "", false, "", null, null, null, "" },
                    { 85, "IDR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 157, DateTimeKind.Utc).AddTicks(32), "", false, "", null, null, null, "" },
                    { 86, "IRR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 157, DateTimeKind.Utc).AddTicks(5052), "", false, "", null, null, null, "" },
                    { 87, "IQD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 158, DateTimeKind.Utc).AddTicks(41), "", false, "", null, null, null, "" },
                    { 88, "IEP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 158, DateTimeKind.Utc).AddTicks(4986), "", false, "", null, null, null, "" },
                    { 89, "ILS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 158, DateTimeKind.Utc).AddTicks(9989), "", false, "", null, null, null, "" },
                    { 90, "ITL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 159, DateTimeKind.Utc).AddTicks(5007), "", false, "", null, null, null, "" },
                    { 91, "JMD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 160, DateTimeKind.Utc).AddTicks(96), "", false, "", null, null, null, "" },
                    { 92, "JPY", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 160, DateTimeKind.Utc).AddTicks(5064), "", false, "", null, null, null, "" },
                    { 93, "JOD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 161, DateTimeKind.Utc).AddTicks(2532), "", false, "", null, null, null, "" },
                    { 94, "KHR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 161, DateTimeKind.Utc).AddTicks(9304), "", false, "", null, null, null, "" },
                    { 95, "KZT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 162, DateTimeKind.Utc).AddTicks(5803), "", false, "", null, null, null, "" },
                    { 96, "KES", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 163, DateTimeKind.Utc).AddTicks(2418), "", false, "", null, null, null, "" },
                    { 97, "KRW", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 163, DateTimeKind.Utc).AddTicks(9010), "", false, "", null, null, null, "" },
                    { 98, "KWD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 164, DateTimeKind.Utc).AddTicks(5631), "", false, "", null, null, null, "" },
                    { 99, "KGS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 165, DateTimeKind.Utc).AddTicks(2278), "", false, "", null, null, null, "" },
                    { 100, "LAK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 165, DateTimeKind.Utc).AddTicks(8918), "", false, "", null, null, null, "" },
                    { 101, "LVL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 166, DateTimeKind.Utc).AddTicks(4056), "", false, "", null, null, null, "" },
                    { 102, "LBP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 166, DateTimeKind.Utc).AddTicks(9104), "", false, "", null, null, null, "" },
                    { 103, "LSL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 167, DateTimeKind.Utc).AddTicks(4053), "", false, "", null, null, null, "" },
                    { 104, "LRD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 167, DateTimeKind.Utc).AddTicks(9058), "", false, "", null, null, null, "" },
                    { 105, "LYD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 168, DateTimeKind.Utc).AddTicks(4163), "", false, "", null, null, null, "" },
                    { 106, "LTL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 168, DateTimeKind.Utc).AddTicks(9266), "", false, "", null, null, null, "" },
                    { 107, "LUF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 169, DateTimeKind.Utc).AddTicks(4227), "", false, "", null, null, null, "" },
                    { 108, "MOP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 170, DateTimeKind.Utc).AddTicks(630), "", false, "", null, null, null, "" },
                    { 109, "MKD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 170, DateTimeKind.Utc).AddTicks(7452), "", false, "", null, null, null, "" },
                    { 110, "MGF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 171, DateTimeKind.Utc).AddTicks(2596), "", false, "", null, null, null, "" },
                    { 111, "MWK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 171, DateTimeKind.Utc).AddTicks(7742), "", false, "", null, null, null, "" },
                    { 112, "MGA", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 172, DateTimeKind.Utc).AddTicks(2737), "", false, "", null, null, null, "" },
                    { 113, "MYR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 172, DateTimeKind.Utc).AddTicks(7732), "", false, "", null, null, null, "" },
                    { 114, "MVR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 173, DateTimeKind.Utc).AddTicks(2724), "", false, "", null, null, null, "" },
                    { 115, "MTL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 173, DateTimeKind.Utc).AddTicks(7832), "", false, "", null, null, null, "" },
                    { 116, "MRO", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 174, DateTimeKind.Utc).AddTicks(2796), "", false, "", null, null, null, "" },
                    { 117, "MUR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 174, DateTimeKind.Utc).AddTicks(7748), "", false, "", null, null, null, "" },
                    { 118, "MXN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 176, DateTimeKind.Utc).AddTicks(8374), "", false, "", null, null, null, "" },
                    { 119, "MDL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 177, DateTimeKind.Utc).AddTicks(3754), "", false, "", null, null, null, "" },
                    { 120, "MNT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 177, DateTimeKind.Utc).AddTicks(8880), "", false, "", null, null, null, "" },
                    { 121, "MAD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 178, DateTimeKind.Utc).AddTicks(3682), "", false, "", null, null, null, "" },
                    { 122, "MZM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 178, DateTimeKind.Utc).AddTicks(8626), "", false, "", null, null, null, "" },
                    { 123, "MZN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 179, DateTimeKind.Utc).AddTicks(3490), "", false, "", null, null, null, "" },
                    { 124, "MMK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 179, DateTimeKind.Utc).AddTicks(8738), "", false, "", null, null, null, "" },
                    { 125, "ANG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 180, DateTimeKind.Utc).AddTicks(3545), "", false, "", null, null, null, "" },
                    { 126, "NAD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 180, DateTimeKind.Utc).AddTicks(8438), "", false, "", null, null, null, "" },
                    { 127, "NPR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 181, DateTimeKind.Utc).AddTicks(3349), "", false, "", null, null, null, "" },
                    { 128, "NLG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 181, DateTimeKind.Utc).AddTicks(8215), "", false, "", null, null, null, "" },
                    { 129, "NZD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 182, DateTimeKind.Utc).AddTicks(3435), "", false, "", null, null, null, "" },
                    { 130, "NIO", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 182, DateTimeKind.Utc).AddTicks(8886), "", false, "", null, null, null, "" },
                    { 131, "NGN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 183, DateTimeKind.Utc).AddTicks(3761), "", false, "", null, null, null, "" },
                    { 132, "KPW", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 183, DateTimeKind.Utc).AddTicks(8639), "", false, "", null, null, null, "" },
                    { 133, "NOK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 184, DateTimeKind.Utc).AddTicks(3521), "", false, "", null, null, null, "" },
                    { 134, "OMR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 184, DateTimeKind.Utc).AddTicks(8727), "", false, "", null, null, null, "" },
                    { 135, "PKR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 185, DateTimeKind.Utc).AddTicks(3630), "", false, "", null, null, null, "" },
                    { 136, "XPD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 186, DateTimeKind.Utc).AddTicks(2357), "", false, "", null, null, null, "" },
                    { 137, "PAB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 187, DateTimeKind.Utc).AddTicks(2338), "", false, "", null, null, null, "" },
                    { 138, "PGK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 188, DateTimeKind.Utc).AddTicks(1704), "", false, "", null, null, null, "" },
                    { 139, "PYG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 189, DateTimeKind.Utc).AddTicks(1018), "", false, "", null, null, null, "" },
                    { 140, "PEN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 190, DateTimeKind.Utc).AddTicks(405), "", false, "", null, null, null, "" },
                    { 141, "PHP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 190, DateTimeKind.Utc).AddTicks(9625), "", false, "", null, null, null, "" },
                    { 142, "XPT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 191, DateTimeKind.Utc).AddTicks(8745), "", false, "", null, null, null, "" },
                    { 143, "Mexico", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 192, DateTimeKind.Utc).AddTicks(8517), "", false, "", null, null, null, "" },
                    { 144, "PLN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 193, DateTimeKind.Utc).AddTicks(7735), "", false, "", null, null, null, "" },
                    { 145, "PTE", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 194, DateTimeKind.Utc).AddTicks(6819), "", false, "", null, null, null, "" },
                    { 146, "GBP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 195, DateTimeKind.Utc).AddTicks(5863), "", false, "", null, null, null, "" },
                    { 147, "ROL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 196, DateTimeKind.Utc).AddTicks(5725), "", false, "", null, null, null, "" },
                    { 148, "RON", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 197, DateTimeKind.Utc).AddTicks(4857), "", false, "", null, null, null, "" },
                    { 149, "RUB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 198, DateTimeKind.Utc).AddTicks(4037), "", false, "", null, null, null, "" },
                    { 150, "RWF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 199, DateTimeKind.Utc).AddTicks(3234), "", false, "", null, null, null, "" },
                    { 151, "WST", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 200, DateTimeKind.Utc).AddTicks(2608), "", false, "", null, null, null, "" },
                    { 152, "STD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 201, DateTimeKind.Utc).AddTicks(1731), "", false, "", null, null, null, "" },
                    { 153, "SAR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 202, DateTimeKind.Utc).AddTicks(1960), "", false, "", null, null, null, "" },
                    { 154, "RSD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 203, DateTimeKind.Utc).AddTicks(1621), "", false, "", null, null, null, "" },
                    { 155, "SCR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 204, DateTimeKind.Utc).AddTicks(854), "", false, "", null, null, null, "" },
                    { 156, "SLL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 205, DateTimeKind.Utc).AddTicks(13), "", false, "", null, null, null, "" },
                    { 157, "XAG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 205, DateTimeKind.Utc).AddTicks(9183), "", false, "", null, null, null, "" },
                    { 158, "SGD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 206, DateTimeKind.Utc).AddTicks(8473), "", false, "", null, null, null, "" },
                    { 159, "SKK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 207, DateTimeKind.Utc).AddTicks(7632), "", false, "", null, null, null, "" },
                    { 160, "SIT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 208, DateTimeKind.Utc).AddTicks(6824), "", false, "", null, null, null, "" },
                    { 161, "SBD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 209, DateTimeKind.Utc).AddTicks(6143), "", false, "", null, null, null, "" },
                    { 162, "SOS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 210, DateTimeKind.Utc).AddTicks(5881), "", false, "", null, null, null, "" },
                    { 163, "ZAR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 211, DateTimeKind.Utc).AddTicks(4122), "", false, "", null, null, null, "" },
                    { 164, "ESP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 211, DateTimeKind.Utc).AddTicks(9094), "", false, "", null, null, null, "" },
                    { 165, "LKR", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 212, DateTimeKind.Utc).AddTicks(4190), "", false, "", null, null, null, "" },
                    { 166, "SHP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 212, DateTimeKind.Utc).AddTicks(9079), "", false, "", null, null, null, "" },
                    { 167, "SDD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 213, DateTimeKind.Utc).AddTicks(3999), "", false, "", null, null, null, "" },
                    { 168, "SDG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 213, DateTimeKind.Utc).AddTicks(8949), "", false, "", null, null, null, "" },
                    { 169, "SDP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 214, DateTimeKind.Utc).AddTicks(3857), "", false, "", null, null, null, "" },
                    { 170, "SRD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 214, DateTimeKind.Utc).AddTicks(8714), "", false, "", null, null, null, "" },
                    { 171, "SRG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 215, DateTimeKind.Utc).AddTicks(3637), "", false, "", null, null, null, "" },
                    { 172, "SZL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 215, DateTimeKind.Utc).AddTicks(8709), "", false, "", null, null, null, "" },
                    { 173, "SEK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 216, DateTimeKind.Utc).AddTicks(3559), "", false, "", null, null, null, "" },
                    { 174, "CHF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 216, DateTimeKind.Utc).AddTicks(8460), "", false, "", null, null, null, "" },
                    { 175, "SYP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 217, DateTimeKind.Utc).AddTicks(3417), "", false, "", null, null, null, "" },
                    { 176, "TWD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 217, DateTimeKind.Utc).AddTicks(8763), "", false, "", null, null, null, "" },
                    { 177, "TJS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 218, DateTimeKind.Utc).AddTicks(5715), "", false, "", null, null, null, "" },
                    { 178, "TZS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 219, DateTimeKind.Utc).AddTicks(862), "", false, "", null, null, null, "" },
                    { 179, "THB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 219, DateTimeKind.Utc).AddTicks(5882), "", false, "", null, null, null, "" },
                    { 180, "TMM", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 220, DateTimeKind.Utc).AddTicks(853), "", false, "", null, null, null, "" },
                    { 181, "TMT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 220, DateTimeKind.Utc).AddTicks(5655), "", false, "", null, null, null, "" },
                    { 182, "TOP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 221, DateTimeKind.Utc).AddTicks(673), "", false, "", null, null, null, "" },
                    { 183, "TTD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 221, DateTimeKind.Utc).AddTicks(6087), "", false, "", null, null, null, "" },
                    { 184, "TND", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 222, DateTimeKind.Utc).AddTicks(1068), "", false, "", null, null, null, "" },
                    { 185, "TRL", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 222, DateTimeKind.Utc).AddTicks(6071), "", false, "", null, null, null, "" },
                    { 186, "TRY", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 223, DateTimeKind.Utc).AddTicks(918), "", false, "", null, null, null, "" },
                    { 187, "UGX", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 223, DateTimeKind.Utc).AddTicks(5751), "", false, "", null, null, null, "" },
                    { 188, "UAH", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 224, DateTimeKind.Utc).AddTicks(539), "", false, "", null, null, null, "" },
                    { 189, "UAH", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 224, DateTimeKind.Utc).AddTicks(5472), "", false, "", null, null, null, "" },
                    { 190, "GBP", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 225, DateTimeKind.Utc).AddTicks(300), "", false, "", null, null, null, "" },
                    { 191, "USD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 225, DateTimeKind.Utc).AddTicks(5171), "", false, "", null, null, null, "" },
                    { 192, "UYU", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 226, DateTimeKind.Utc).AddTicks(51), "", false, "", null, null, null, "" },
                    { 193, "UZS", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 226, DateTimeKind.Utc).AddTicks(5018), "", false, "", null, null, null, "" },
                    { 194, "AED", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 226, DateTimeKind.Utc).AddTicks(9812), "", false, "", null, null, null, "" },
                    { 195, "VUV", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 227, DateTimeKind.Utc).AddTicks(4671), "", false, "", null, null, null, "" },
                    { 196, "VEB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 227, DateTimeKind.Utc).AddTicks(9518), "", false, "", null, null, null, "" },
                    { 197, "VEF", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 228, DateTimeKind.Utc).AddTicks(4410), "", false, "", null, null, null, "" },
                    { 198, "VND", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 228, DateTimeKind.Utc).AddTicks(9176), "", false, "", null, null, null, "" },
                    { 199, "YER", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 229, DateTimeKind.Utc).AddTicks(4082), "", false, "", null, null, null, "" },
                    { 200, "YUN", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 229, DateTimeKind.Utc).AddTicks(8957), "", false, "", null, null, null, "" },
                    { 201, "ZMK", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 230, DateTimeKind.Utc).AddTicks(4313), "", false, "", null, null, null, "" },
                    { 202, "ZMW", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 230, DateTimeKind.Utc).AddTicks(9136), "", false, "", null, null, null, "" },
                    { 203, "ZWD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 231, DateTimeKind.Utc).AddTicks(3993), "", false, "", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 84, DateTimeKind.Utc).AddTicks(476), "Setting", false, "Setting", null, null, null, null },
                    { 400, "MKDT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 84, DateTimeKind.Utc).AddTicks(5771), "Market Data", false, "MarketData", null, null, null, null },
                    { 500, "PROJ", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 85, DateTimeKind.Utc).AddTicks(873), "Project", false, "Project", null, null, null, null },
                    { 501, "PROJCONFIG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 85, DateTimeKind.Utc).AddTicks(5901), "Project Configuration", false, "ProjectConfiguration", null, null, null, null },
                    { 502, "CONFIG", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 86, DateTimeKind.Utc).AddTicks(931), "Configuration", false, "Configuration", null, null, null, null },
                    { 503, "EXT", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 86, DateTimeKind.Utc).AddTicks(5914), "Extractor", false, "Extractor", null, null, null, null },
                    { 504, "SB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 87, DateTimeKind.Utc).AddTicks(921), "Strategy Builder", false, "StrategyBuilder", null, null, null, null },
                    { 505, "AB", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 87, DateTimeKind.Utc).AddTicks(5857), "Assembled Builder", false, "AssembledBuilder", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 104, DateTimeKind.Utc).AddTicks(9325), "", false, "Forex", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 106, DateTimeKind.Utc).AddTicks(5673), "", false, "America", null, null, null, null },
                    { 2, "Europe", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 107, DateTimeKind.Utc).AddTicks(4140), "", false, "Europe", null, null, null, null },
                    { 3, "Asia", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(1922), "", false, "Asia", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 88, DateTimeKind.Utc).AddTicks(969), null, false, null, null, null, null, "eng" },
                    { 2, "Theme", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 88, DateTimeKind.Utc).AddTicks(5990), null, false, null, null, null, null, "Light" },
                    { 3, "Color", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 89, DateTimeKind.Utc).AddTicks(899), null, false, null, null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 89, DateTimeKind.Utc).AddTicks(5958), null, false, null, null, null, null, "" },
                    { 5, "Host", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 96, DateTimeKind.Utc).AddTicks(3530), null, false, null, null, null, null, "192.168.50.137" },
                    { 6, "Port", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 97, DateTimeKind.Utc).AddTicks(2806), null, false, null, null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 97, DateTimeKind.Utc).AddTicks(2898), "Euro vs US Dollar", false, "EURUSD", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 98, DateTimeKind.Utc).AddTicks(1156), "", false, "1 Minute", null, null, null, "1" },
                    { 2, "M5", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 98, DateTimeKind.Utc).AddTicks(8769), "", false, "5 Minute", null, null, null, "5" },
                    { 3, "M15", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 99, DateTimeKind.Utc).AddTicks(6446), "", false, "15 Minute", null, null, null, "15" },
                    { 4, "M30", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 100, DateTimeKind.Utc).AddTicks(3704), "", false, "30 Minute", null, null, null, "30" },
                    { 5, "H1", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 101, DateTimeKind.Utc).AddTicks(1360), "", false, "1 Hour", null, null, null, "16385" },
                    { 6, "H4", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 101, DateTimeKind.Utc).AddTicks(8374), "", false, "4 Hour", null, null, null, "16388" },
                    { 7, "D1", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 102, DateTimeKind.Utc).AddTicks(5312), "", false, "Daily", null, null, null, "16408" },
                    { 8, "W1", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 103, DateTimeKind.Utc).AddTicks(4861), "", false, "Weekly", null, null, null, "32769" },
                    { 9, "MN1", "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 104, DateTimeKind.Utc).AddTicks(2330), "", false, "Monthly", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "ConfigurationId", "ABMinImprovePercent", "ABTransactionsTarget", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "Description", "EndDate", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxProgressivenessVariation", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "SBMaxCorrelationPercent", "SBMaxSuccessRateVariation", "SBMinSuccessRatePercentIS", "SBMinSuccessRatePercentOS", "SBMinTransactionsIS", "SBMinTransactionsOS", "SBTransactionsTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "StartDate", "SymbolId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 600, "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2044), 6, "Default Configuration", null, 50, null, null, false, false, 2m, 1.5m, 1000000, 100, 300m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2039), 1, 5, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "ScheduleConfiguration",
                columns: new[] { "ScheduleConfigurationId", "ConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "IsDeleted", "MarketRegionId", "StartDate", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 1, "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2122), "Default Schedule America", null, 54000, false, 1, new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2119), 82800, null, null, null },
                    { 2, 1, "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2128), "Default Schedule Europe", null, 32400, false, 2, new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2126), 64800, null, null, null },
                    { 3, 1, "0000", "admin", new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2133), "Default Schedule Asia", null, 3600, false, 3, new DateTime(2023, 6, 2, 19, 49, 33, 108, DateTimeKind.Utc).AddTicks(2131), 32400, null, null, null }
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
