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
                    PushPort = table.Column<string>(type: "TEXT", nullable: true),
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
                    SBMinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxTransactionsVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    Progressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxPercentCorrelation = table.Column<decimal>(type: "TEXT", nullable: false),
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
                    SBMinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMinTransactionsOS = table.Column<int>(type: "INTEGER", nullable: false),
                    SBMinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxTransactionsVariation = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    Progressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    SBMaxPercentCorrelation = table.Column<decimal>(type: "TEXT", nullable: false),
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
                    { 1, "AFN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(7213), "", false, "", null, null, null, "" },
                    { 2, "AFA", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 717, DateTimeKind.Utc).AddTicks(460), "", false, "", null, null, null, "" },
                    { 3, "ALL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 717, DateTimeKind.Utc).AddTicks(3664), "", false, "", null, null, null, "" },
                    { 4, "DZD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 717, DateTimeKind.Utc).AddTicks(6939), "", false, "", null, null, null, "" },
                    { 5, "ADF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 718, DateTimeKind.Utc).AddTicks(262), "", false, "", null, null, null, "" },
                    { 6, "ADP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 718, DateTimeKind.Utc).AddTicks(3501), "", false, "", null, null, null, "" },
                    { 7, "AOA", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 718, DateTimeKind.Utc).AddTicks(6722), "", false, "", null, null, null, "" },
                    { 8, "AON", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 718, DateTimeKind.Utc).AddTicks(9920), "", false, "", null, null, null, "" },
                    { 9, "ARS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 719, DateTimeKind.Utc).AddTicks(3151), "", false, "", null, null, null, "" },
                    { 10, "AMD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 719, DateTimeKind.Utc).AddTicks(6597), "", false, "", null, null, null, "" },
                    { 11, "AWG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 719, DateTimeKind.Utc).AddTicks(9817), "", false, "", null, null, null, "" },
                    { 12, "AUD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 720, DateTimeKind.Utc).AddTicks(3030), "", false, "", null, null, null, "" },
                    { 13, "ATS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 720, DateTimeKind.Utc).AddTicks(6247), "", false, "", null, null, null, "" },
                    { 14, "AZM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 720, DateTimeKind.Utc).AddTicks(9492), "", false, "", null, null, null, "" },
                    { 15, "AZN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 721, DateTimeKind.Utc).AddTicks(2713), "", false, "", null, null, null, "" },
                    { 16, "BSD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 721, DateTimeKind.Utc).AddTicks(5929), "", false, "", null, null, null, "" },
                    { 17, "BHD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 721, DateTimeKind.Utc).AddTicks(9143), "", false, "", null, null, null, "" },
                    { 18, "BDT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 722, DateTimeKind.Utc).AddTicks(2338), "", false, "", null, null, null, "" },
                    { 19, "BBD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 722, DateTimeKind.Utc).AddTicks(5611), "", false, "", null, null, null, "" },
                    { 20, "BEF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 722, DateTimeKind.Utc).AddTicks(8839), "", false, "", null, null, null, "" },
                    { 21, "BZD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 723, DateTimeKind.Utc).AddTicks(2098), "", false, "", null, null, null, "" },
                    { 22, "BMD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 723, DateTimeKind.Utc).AddTicks(5296), "", false, "", null, null, null, "" },
                    { 23, "BTN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 723, DateTimeKind.Utc).AddTicks(8946), "", false, "", null, null, null, "" },
                    { 24, "BOB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 724, DateTimeKind.Utc).AddTicks(2176), "", false, "", null, null, null, "" },
                    { 25, "BAM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 724, DateTimeKind.Utc).AddTicks(5356), "", false, "", null, null, null, "" },
                    { 26, "BWP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 724, DateTimeKind.Utc).AddTicks(8537), "", false, "", null, null, null, "" },
                    { 27, "BRL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 725, DateTimeKind.Utc).AddTicks(1791), "", false, "", null, null, null, "" },
                    { 28, "GBP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 725, DateTimeKind.Utc).AddTicks(5404), "", false, "", null, null, null, "" },
                    { 29, "BND", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 725, DateTimeKind.Utc).AddTicks(8615), "", false, "", null, null, null, "" },
                    { 30, "BGN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 726, DateTimeKind.Utc).AddTicks(2067), "", false, "", null, null, null, "" },
                    { 31, "BGL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 726, DateTimeKind.Utc).AddTicks(5372), "", false, "", null, null, null, "" },
                    { 32, "BIF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 726, DateTimeKind.Utc).AddTicks(8575), "", false, "", null, null, null, "" },
                    { 33, "BYR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 727, DateTimeKind.Utc).AddTicks(1795), "", false, "", null, null, null, "" },
                    { 34, "XOF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 727, DateTimeKind.Utc).AddTicks(5037), "", false, "", null, null, null, "" },
                    { 35, "XAF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 727, DateTimeKind.Utc).AddTicks(8250), "", false, "", null, null, null, "" },
                    { 36, "XPF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 728, DateTimeKind.Utc).AddTicks(1487), "", false, "", null, null, null, "" },
                    { 37, "KHR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 728, DateTimeKind.Utc).AddTicks(4826), "", false, "", null, null, null, "" },
                    { 38, "CAD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 728, DateTimeKind.Utc).AddTicks(8160), "", false, "", null, null, null, "" },
                    { 39, "CVE", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 729, DateTimeKind.Utc).AddTicks(1471), "", false, "", null, null, null, "" },
                    { 40, "KYD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 729, DateTimeKind.Utc).AddTicks(4764), "", false, "", null, null, null, "" },
                    { 41, "CLP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 729, DateTimeKind.Utc).AddTicks(8081), "", false, "", null, null, null, "" },
                    { 42, "CNY", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 730, DateTimeKind.Utc).AddTicks(1386), "", false, "", null, null, null, "" },
                    { 43, "COP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 730, DateTimeKind.Utc).AddTicks(4695), "", false, "", null, null, null, "" },
                    { 44, "KMF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 730, DateTimeKind.Utc).AddTicks(8014), "", false, "", null, null, null, "" },
                    { 45, "CDF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 731, DateTimeKind.Utc).AddTicks(1416), "", false, "", null, null, null, "" },
                    { 46, "CRC", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 731, DateTimeKind.Utc).AddTicks(4763), "", false, "", null, null, null, "" },
                    { 47, "HRK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 731, DateTimeKind.Utc).AddTicks(8023), "", false, "", null, null, null, "" },
                    { 48, "CUC", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 732, DateTimeKind.Utc).AddTicks(1360), "", false, "", null, null, null, "" },
                    { 49, "CUP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 732, DateTimeKind.Utc).AddTicks(4641), "", false, "", null, null, null, "" },
                    { 50, "CYP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 732, DateTimeKind.Utc).AddTicks(7940), "", false, "", null, null, null, "" },
                    { 51, "CZK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 733, DateTimeKind.Utc).AddTicks(1242), "", false, "", null, null, null, "" },
                    { 52, "DKK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 733, DateTimeKind.Utc).AddTicks(4597), "", false, "", null, null, null, "" },
                    { 53, "DJF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 733, DateTimeKind.Utc).AddTicks(8576), "", false, "", null, null, null, "" },
                    { 54, "DOP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 734, DateTimeKind.Utc).AddTicks(2158), "", false, "", null, null, null, "" },
                    { 55, "NLG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 734, DateTimeKind.Utc).AddTicks(6196), "", false, "", null, null, null, "" },
                    { 56, "XCD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 734, DateTimeKind.Utc).AddTicks(9550), "", false, "", null, null, null, "" },
                    { 57, "XEU", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 735, DateTimeKind.Utc).AddTicks(2944), "", false, "", null, null, null, "" },
                    { 58, "ECS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 735, DateTimeKind.Utc).AddTicks(6325), "", false, "", null, null, null, "" },
                    { 59, "EGP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 735, DateTimeKind.Utc).AddTicks(9631), "", false, "", null, null, null, "" },
                    { 60, "SVC", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 736, DateTimeKind.Utc).AddTicks(2955), "", false, "", null, null, null, "" },
                    { 61, "EEK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 736, DateTimeKind.Utc).AddTicks(6317), "", false, "", null, null, null, "" },
                    { 62, "ETB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 737, DateTimeKind.Utc).AddTicks(163), "", false, "", null, null, null, "" },
                    { 63, "EUR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 737, DateTimeKind.Utc).AddTicks(3521), "", false, "", null, null, null, "" },
                    { 64, "FKP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 737, DateTimeKind.Utc).AddTicks(7031), "", false, "", null, null, null, "" },
                    { 65, "FJD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 738, DateTimeKind.Utc).AddTicks(373), "", false, "", null, null, null, "" },
                    { 66, "FIM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 738, DateTimeKind.Utc).AddTicks(3877), "", false, "", null, null, null, "" },
                    { 67, "FRF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 738, DateTimeKind.Utc).AddTicks(7321), "", false, "", null, null, null, "" },
                    { 68, "GMD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 739, DateTimeKind.Utc).AddTicks(542), "", false, "", null, null, null, "" },
                    { 69, "GEL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 739, DateTimeKind.Utc).AddTicks(3745), "", false, "", null, null, null, "" },
                    { 70, "DEM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 739, DateTimeKind.Utc).AddTicks(7002), "", false, "", null, null, null, "" },
                    { 71, "GHC", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 740, DateTimeKind.Utc).AddTicks(261), "", false, "", null, null, null, "" },
                    { 72, "GHS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 740, DateTimeKind.Utc).AddTicks(3981), "", false, "", null, null, null, "" },
                    { 73, "GIP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 740, DateTimeKind.Utc).AddTicks(7409), "", false, "", null, null, null, "" },
                    { 74, "XAU", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 741, DateTimeKind.Utc).AddTicks(647), "", false, "", null, null, null, "" },
                    { 75, "GRD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 741, DateTimeKind.Utc).AddTicks(3927), "", false, "", null, null, null, "" },
                    { 76, "GTQ", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 741, DateTimeKind.Utc).AddTicks(7330), "", false, "", null, null, null, "" },
                    { 77, "GNF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 742, DateTimeKind.Utc).AddTicks(646), "", false, "", null, null, null, "" },
                    { 78, "GYD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 742, DateTimeKind.Utc).AddTicks(3981), "", false, "", null, null, null, "" },
                    { 79, "HTG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 742, DateTimeKind.Utc).AddTicks(7300), "", false, "", null, null, null, "" },
                    { 80, "HNL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 743, DateTimeKind.Utc).AddTicks(566), "", false, "", null, null, null, "" },
                    { 81, "HKD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 743, DateTimeKind.Utc).AddTicks(3916), "", false, "", null, null, null, "" },
                    { 82, "HUF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 743, DateTimeKind.Utc).AddTicks(7269), "", false, "", null, null, null, "" },
                    { 83, "ISK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 744, DateTimeKind.Utc).AddTicks(645), "", false, "", null, null, null, "" },
                    { 84, "INR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 744, DateTimeKind.Utc).AddTicks(4029), "", false, "", null, null, null, "" },
                    { 85, "IDR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 744, DateTimeKind.Utc).AddTicks(7238), "", false, "", null, null, null, "" },
                    { 86, "IRR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 745, DateTimeKind.Utc).AddTicks(487), "", false, "", null, null, null, "" },
                    { 87, "IQD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 745, DateTimeKind.Utc).AddTicks(3681), "", false, "", null, null, null, "" },
                    { 88, "IEP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 745, DateTimeKind.Utc).AddTicks(6949), "", false, "", null, null, null, "" },
                    { 89, "ILS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 746, DateTimeKind.Utc).AddTicks(220), "", false, "", null, null, null, "" },
                    { 90, "ITL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 746, DateTimeKind.Utc).AddTicks(3396), "", false, "", null, null, null, "" },
                    { 91, "JMD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 746, DateTimeKind.Utc).AddTicks(6664), "", false, "", null, null, null, "" },
                    { 92, "JPY", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 746, DateTimeKind.Utc).AddTicks(9867), "", false, "", null, null, null, "" },
                    { 93, "JOD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 747, DateTimeKind.Utc).AddTicks(3038), "", false, "", null, null, null, "" },
                    { 94, "KHR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 747, DateTimeKind.Utc).AddTicks(6271), "", false, "", null, null, null, "" },
                    { 95, "KZT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 747, DateTimeKind.Utc).AddTicks(9699), "", false, "", null, null, null, "" },
                    { 96, "KES", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 748, DateTimeKind.Utc).AddTicks(2917), "", false, "", null, null, null, "" },
                    { 97, "KRW", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 748, DateTimeKind.Utc).AddTicks(6336), "", false, "", null, null, null, "" },
                    { 98, "KWD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 748, DateTimeKind.Utc).AddTicks(9849), "", false, "", null, null, null, "" },
                    { 99, "KGS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 749, DateTimeKind.Utc).AddTicks(3239), "", false, "", null, null, null, "" },
                    { 100, "LAK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 749, DateTimeKind.Utc).AddTicks(6975), "", false, "", null, null, null, "" },
                    { 101, "LVL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 750, DateTimeKind.Utc).AddTicks(634), "", false, "", null, null, null, "" },
                    { 102, "LBP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 750, DateTimeKind.Utc).AddTicks(4268), "", false, "", null, null, null, "" },
                    { 103, "LSL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 750, DateTimeKind.Utc).AddTicks(7639), "", false, "", null, null, null, "" },
                    { 104, "LRD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 751, DateTimeKind.Utc).AddTicks(1019), "", false, "", null, null, null, "" },
                    { 105, "LYD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 751, DateTimeKind.Utc).AddTicks(4430), "", false, "", null, null, null, "" },
                    { 106, "LTL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 751, DateTimeKind.Utc).AddTicks(7757), "", false, "", null, null, null, "" },
                    { 107, "LUF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 752, DateTimeKind.Utc).AddTicks(1079), "", false, "", null, null, null, "" },
                    { 108, "MOP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 752, DateTimeKind.Utc).AddTicks(4404), "", false, "", null, null, null, "" },
                    { 109, "MKD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 752, DateTimeKind.Utc).AddTicks(7716), "", false, "", null, null, null, "" },
                    { 110, "MGF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 753, DateTimeKind.Utc).AddTicks(1034), "", false, "", null, null, null, "" },
                    { 111, "MWK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 753, DateTimeKind.Utc).AddTicks(4367), "", false, "", null, null, null, "" },
                    { 112, "MGA", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 753, DateTimeKind.Utc).AddTicks(7610), "", false, "", null, null, null, "" },
                    { 113, "MYR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 754, DateTimeKind.Utc).AddTicks(832), "", false, "", null, null, null, "" },
                    { 114, "MVR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 754, DateTimeKind.Utc).AddTicks(4039), "", false, "", null, null, null, "" },
                    { 115, "MTL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 754, DateTimeKind.Utc).AddTicks(7299), "", false, "", null, null, null, "" },
                    { 116, "MRO", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 755, DateTimeKind.Utc).AddTicks(532), "", false, "", null, null, null, "" },
                    { 117, "MUR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 755, DateTimeKind.Utc).AddTicks(3708), "", false, "", null, null, null, "" },
                    { 118, "MXN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 755, DateTimeKind.Utc).AddTicks(6956), "", false, "", null, null, null, "" },
                    { 119, "MDL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 756, DateTimeKind.Utc).AddTicks(369), "", false, "", null, null, null, "" },
                    { 120, "MNT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 756, DateTimeKind.Utc).AddTicks(3696), "", false, "", null, null, null, "" },
                    { 121, "MAD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 756, DateTimeKind.Utc).AddTicks(6905), "", false, "", null, null, null, "" },
                    { 122, "MZM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 757, DateTimeKind.Utc).AddTicks(272), "", false, "", null, null, null, "" },
                    { 123, "MZN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 757, DateTimeKind.Utc).AddTicks(3542), "", false, "", null, null, null, "" },
                    { 124, "MMK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 757, DateTimeKind.Utc).AddTicks(6819), "", false, "", null, null, null, "" },
                    { 125, "ANG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 758, DateTimeKind.Utc).AddTicks(65), "", false, "", null, null, null, "" },
                    { 126, "NAD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 758, DateTimeKind.Utc).AddTicks(3275), "", false, "", null, null, null, "" },
                    { 127, "NPR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 758, DateTimeKind.Utc).AddTicks(6483), "", false, "", null, null, null, "" },
                    { 128, "NLG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 758, DateTimeKind.Utc).AddTicks(9690), "", false, "", null, null, null, "" },
                    { 129, "NZD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 759, DateTimeKind.Utc).AddTicks(2968), "", false, "", null, null, null, "" },
                    { 130, "NIO", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 759, DateTimeKind.Utc).AddTicks(6224), "", false, "", null, null, null, "" },
                    { 131, "NGN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 759, DateTimeKind.Utc).AddTicks(9405), "", false, "", null, null, null, "" },
                    { 132, "KPW", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 760, DateTimeKind.Utc).AddTicks(2643), "", false, "", null, null, null, "" },
                    { 133, "NOK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 760, DateTimeKind.Utc).AddTicks(5932), "", false, "", null, null, null, "" },
                    { 134, "OMR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 760, DateTimeKind.Utc).AddTicks(9173), "", false, "", null, null, null, "" },
                    { 135, "PKR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 761, DateTimeKind.Utc).AddTicks(2581), "", false, "", null, null, null, "" },
                    { 136, "XPD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 762, DateTimeKind.Utc).AddTicks(5996), "", false, "", null, null, null, "" },
                    { 137, "PAB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 763, DateTimeKind.Utc).AddTicks(143), "", false, "", null, null, null, "" },
                    { 138, "PGK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 763, DateTimeKind.Utc).AddTicks(3364), "", false, "", null, null, null, "" },
                    { 139, "PYG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 763, DateTimeKind.Utc).AddTicks(6556), "", false, "", null, null, null, "" },
                    { 140, "PEN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 763, DateTimeKind.Utc).AddTicks(9811), "", false, "", null, null, null, "" },
                    { 141, "PHP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 764, DateTimeKind.Utc).AddTicks(2963), "", false, "", null, null, null, "" },
                    { 142, "XPT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 764, DateTimeKind.Utc).AddTicks(6580), "", false, "", null, null, null, "" },
                    { 143, "Mexico", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 764, DateTimeKind.Utc).AddTicks(9933), "", false, "", null, null, null, "" },
                    { 144, "PLN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 765, DateTimeKind.Utc).AddTicks(3190), "", false, "", null, null, null, "" },
                    { 145, "PTE", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 765, DateTimeKind.Utc).AddTicks(6456), "", false, "", null, null, null, "" },
                    { 146, "GBP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 765, DateTimeKind.Utc).AddTicks(9727), "", false, "", null, null, null, "" },
                    { 147, "ROL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 766, DateTimeKind.Utc).AddTicks(3405), "", false, "", null, null, null, "" },
                    { 148, "RON", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 766, DateTimeKind.Utc).AddTicks(6895), "", false, "", null, null, null, "" },
                    { 149, "RUB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 767, DateTimeKind.Utc).AddTicks(244), "", false, "", null, null, null, "" },
                    { 150, "RWF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 767, DateTimeKind.Utc).AddTicks(3423), "", false, "", null, null, null, "" },
                    { 151, "WST", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 767, DateTimeKind.Utc).AddTicks(6578), "", false, "", null, null, null, "" },
                    { 152, "STD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 767, DateTimeKind.Utc).AddTicks(9716), "", false, "", null, null, null, "" },
                    { 153, "SAR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 768, DateTimeKind.Utc).AddTicks(2832), "", false, "", null, null, null, "" },
                    { 154, "RSD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 768, DateTimeKind.Utc).AddTicks(5957), "", false, "", null, null, null, "" },
                    { 155, "SCR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 768, DateTimeKind.Utc).AddTicks(9238), "", false, "", null, null, null, "" },
                    { 156, "SLL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 769, DateTimeKind.Utc).AddTicks(2400), "", false, "", null, null, null, "" },
                    { 157, "XAG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 769, DateTimeKind.Utc).AddTicks(5696), "", false, "", null, null, null, "" },
                    { 158, "SGD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 769, DateTimeKind.Utc).AddTicks(8824), "", false, "", null, null, null, "" },
                    { 159, "SKK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 770, DateTimeKind.Utc).AddTicks(2119), "", false, "", null, null, null, "" },
                    { 160, "SIT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 770, DateTimeKind.Utc).AddTicks(5230), "", false, "", null, null, null, "" },
                    { 161, "SBD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 770, DateTimeKind.Utc).AddTicks(8496), "", false, "", null, null, null, "" },
                    { 162, "SOS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 771, DateTimeKind.Utc).AddTicks(1623), "", false, "", null, null, null, "" },
                    { 163, "ZAR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 771, DateTimeKind.Utc).AddTicks(4896), "", false, "", null, null, null, "" },
                    { 164, "ESP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 771, DateTimeKind.Utc).AddTicks(8019), "", false, "", null, null, null, "" },
                    { 165, "LKR", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 772, DateTimeKind.Utc).AddTicks(1123), "", false, "", null, null, null, "" },
                    { 166, "SHP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 772, DateTimeKind.Utc).AddTicks(4241), "", false, "", null, null, null, "" },
                    { 167, "SDD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 772, DateTimeKind.Utc).AddTicks(7364), "", false, "", null, null, null, "" },
                    { 168, "SDG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 773, DateTimeKind.Utc).AddTicks(475), "", false, "", null, null, null, "" },
                    { 169, "SDP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 773, DateTimeKind.Utc).AddTicks(3595), "", false, "", null, null, null, "" },
                    { 170, "SRD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 773, DateTimeKind.Utc).AddTicks(6812), "", false, "", null, null, null, "" },
                    { 171, "SRG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 773, DateTimeKind.Utc).AddTicks(9936), "", false, "", null, null, null, "" },
                    { 172, "SZL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 774, DateTimeKind.Utc).AddTicks(3042), "", false, "", null, null, null, "" },
                    { 173, "SEK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 774, DateTimeKind.Utc).AddTicks(6165), "", false, "", null, null, null, "" },
                    { 174, "CHF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 774, DateTimeKind.Utc).AddTicks(9256), "", false, "", null, null, null, "" },
                    { 175, "SYP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 775, DateTimeKind.Utc).AddTicks(2339), "", false, "", null, null, null, "" },
                    { 176, "TWD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 775, DateTimeKind.Utc).AddTicks(5488), "", false, "", null, null, null, "" },
                    { 177, "TJS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 775, DateTimeKind.Utc).AddTicks(8678), "", false, "", null, null, null, "" },
                    { 178, "TZS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 776, DateTimeKind.Utc).AddTicks(1827), "", false, "", null, null, null, "" },
                    { 179, "THB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 776, DateTimeKind.Utc).AddTicks(4959), "", false, "", null, null, null, "" },
                    { 180, "TMM", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 776, DateTimeKind.Utc).AddTicks(8088), "", false, "", null, null, null, "" },
                    { 181, "TMT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 777, DateTimeKind.Utc).AddTicks(1190), "", false, "", null, null, null, "" },
                    { 182, "TOP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 777, DateTimeKind.Utc).AddTicks(4693), "", false, "", null, null, null, "" },
                    { 183, "TTD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 777, DateTimeKind.Utc).AddTicks(7847), "", false, "", null, null, null, "" },
                    { 184, "TND", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 778, DateTimeKind.Utc).AddTicks(1205), "", false, "", null, null, null, "" },
                    { 185, "TRL", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 778, DateTimeKind.Utc).AddTicks(4359), "", false, "", null, null, null, "" },
                    { 186, "TRY", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 778, DateTimeKind.Utc).AddTicks(7573), "", false, "", null, null, null, "" },
                    { 187, "UGX", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 779, DateTimeKind.Utc).AddTicks(719), "", false, "", null, null, null, "" },
                    { 188, "UAH", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 779, DateTimeKind.Utc).AddTicks(3873), "", false, "", null, null, null, "" },
                    { 189, "UAH", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 779, DateTimeKind.Utc).AddTicks(7237), "", false, "", null, null, null, "" },
                    { 190, "GBP", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 780, DateTimeKind.Utc).AddTicks(357), "", false, "", null, null, null, "" },
                    { 191, "USD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 780, DateTimeKind.Utc).AddTicks(3568), "", false, "", null, null, null, "" },
                    { 192, "UYU", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 780, DateTimeKind.Utc).AddTicks(6736), "", false, "", null, null, null, "" },
                    { 193, "UZS", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 780, DateTimeKind.Utc).AddTicks(9879), "", false, "", null, null, null, "" },
                    { 194, "AED", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 781, DateTimeKind.Utc).AddTicks(3057), "", false, "", null, null, null, "" },
                    { 195, "VUV", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 781, DateTimeKind.Utc).AddTicks(6398), "", false, "", null, null, null, "" },
                    { 196, "VEB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 781, DateTimeKind.Utc).AddTicks(9773), "", false, "", null, null, null, "" },
                    { 197, "VEF", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 782, DateTimeKind.Utc).AddTicks(3161), "", false, "", null, null, null, "" },
                    { 198, "VND", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 782, DateTimeKind.Utc).AddTicks(6621), "", false, "", null, null, null, "" },
                    { 199, "YER", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 782, DateTimeKind.Utc).AddTicks(9846), "", false, "", null, null, null, "" },
                    { 200, "YUN", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 783, DateTimeKind.Utc).AddTicks(3077), "", false, "", null, null, null, "" },
                    { 201, "ZMK", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 783, DateTimeKind.Utc).AddTicks(6333), "", false, "", null, null, null, "" },
                    { 202, "ZMW", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 783, DateTimeKind.Utc).AddTicks(9484), "", false, "", null, null, null, "" },
                    { 203, "ZWD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 784, DateTimeKind.Utc).AddTicks(2689), "", false, "", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 707, DateTimeKind.Utc).AddTicks(9898), "Setting", false, "Setting", null, null, null, null },
                    { 400, "MKDT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 708, DateTimeKind.Utc).AddTicks(3198), "Market Data", false, "MarketData", null, null, null, null },
                    { 500, "PROJ", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 708, DateTimeKind.Utc).AddTicks(6438), "Project", false, "Project", null, null, null, null },
                    { 501, "PROJCONFIG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 708, DateTimeKind.Utc).AddTicks(9664), "Project Configuration", false, "ProjectConfiguration", null, null, null, null },
                    { 502, "CONFIG", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 709, DateTimeKind.Utc).AddTicks(2852), "Configuration", false, "Configuration", null, null, null, null },
                    { 503, "EXT", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 709, DateTimeKind.Utc).AddTicks(6024), "Extractor", false, "Extractor", null, null, null, null },
                    { 504, "SB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 709, DateTimeKind.Utc).AddTicks(9220), "Strategy Builder", false, "StrategyBuilder", null, null, null, null },
                    { 505, "AB", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 710, DateTimeKind.Utc).AddTicks(2362), "Assembled Builder", false, "AssembledBuilder", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 715, DateTimeKind.Utc).AddTicks(3153), "", false, "Forex", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 715, DateTimeKind.Utc).AddTicks(6421), "", false, "America", null, null, null, null },
                    { 2, "Europe", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 715, DateTimeKind.Utc).AddTicks(9558), "", false, "Europe", null, null, null, null },
                    { 3, "Asia", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2671), "", false, "Asia", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 710, DateTimeKind.Utc).AddTicks(5632), null, false, null, null, null, null, "eng" },
                    { 2, "Theme", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 710, DateTimeKind.Utc).AddTicks(8775), null, false, null, null, null, null, "Light" },
                    { 3, "Color", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 711, DateTimeKind.Utc).AddTicks(1893), null, false, null, null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 711, DateTimeKind.Utc).AddTicks(5076), null, false, null, null, null, null, "" },
                    { 5, "Host", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 711, DateTimeKind.Utc).AddTicks(8234), null, false, null, null, null, null, "192.168.50.137" },
                    { 6, "Port", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 712, DateTimeKind.Utc).AddTicks(1412), null, false, null, null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 712, DateTimeKind.Utc).AddTicks(1455), "Euro vs US Dollar", false, "EURUSD", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "IsDeleted", "Name", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 712, DateTimeKind.Utc).AddTicks(4731), "", false, "1 Minute", null, null, null, "1" },
                    { 2, "M5", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 712, DateTimeKind.Utc).AddTicks(7900), "", false, "5 Minute", null, null, null, "5" },
                    { 3, "M15", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 713, DateTimeKind.Utc).AddTicks(1032), "", false, "15 Minute", null, null, null, "15" },
                    { 4, "M30", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 713, DateTimeKind.Utc).AddTicks(4186), "", false, "30 Minute", null, null, null, "30" },
                    { 5, "H1", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 713, DateTimeKind.Utc).AddTicks(7319), "", false, "1 Hour", null, null, null, "16385" },
                    { 6, "H4", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 714, DateTimeKind.Utc).AddTicks(469), "", false, "4 Hour", null, null, null, "16388" },
                    { 7, "D1", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 714, DateTimeKind.Utc).AddTicks(3633), "", false, "Daily", null, null, null, "16408" },
                    { 8, "W1", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 714, DateTimeKind.Utc).AddTicks(6808), "", false, "Weekly", null, null, null, "32769" },
                    { 9, "MN1", "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 714, DateTimeKind.Utc).AddTicks(9956), "", false, "Monthly", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "Configuration",
                columns: new[] { "ConfigurationId", "ABMinImprovePercent", "ABTransactionsTarget", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "Description", "EndDate", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "IsDeleted", "IsProgressiveness", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "Progressiveness", "SBMaxPercentCorrelation", "SBMaxTransactionsVariation", "SBMinPercentSuccessIS", "SBMinPercentSuccessOS", "SBMinTransactionsIS", "SBMinTransactionsOS", "SBTransactionsTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "StartDate", "SymbolId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 600, "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2719), 6, "Default Configuration", null, 50, null, null, false, false, 1.5m, 1000000, 100, 300m, 2m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2717), 1, 5, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "ScheduleConfiguration",
                columns: new[] { "ScheduleConfigurationId", "ConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "IsDeleted", "MarketRegionId", "StartDate", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 1, "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2744), "Default Schedule America", null, 54000, false, 1, new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2743), 82800, null, null, null },
                    { 2, 1, "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2747), "Default Schedule Europe", null, 32400, false, 2, new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2746), 64800, null, null, null },
                    { 3, 1, "0000", "admin", new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2750), "Default Schedule Asia", null, 3600, false, 3, new DateTime(2023, 5, 29, 17, 38, 38, 716, DateTimeKind.Utc).AddTicks(2748), 32400, null, null, null }
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
                name: "IX_ExpertAdvisor_PushPort",
                table: "ExpertAdvisor",
                column: "PushPort",
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
