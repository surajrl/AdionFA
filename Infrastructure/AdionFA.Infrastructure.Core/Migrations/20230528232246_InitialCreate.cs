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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "HistoricalData",
                columns: table => new
                {
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "ProjectGlobalConfiguration",
                columns: table => new
                {
                    ProjectGlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_ProjectGlobalConfiguration", x => x.ProjectGlobalConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalConfiguration_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalConfiguration_Timeframe_TimeframeId",
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
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                name: "ProjectGlobalScheduleConfiguration",
                columns: table => new
                {
                    ProjectGlobalScheduleConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectGlobalConfigurationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketRegionId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    ToTimeInSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_ProjectGlobalScheduleConfiguration", x => x.ProjectGlobalScheduleConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalScheduleConfiguration_MarketRegion_MarketRegionId",
                        column: x => x.MarketRegionId,
                        principalTable: "MarketRegion",
                        principalColumn: "MarketRegionId");
                    table.ForeignKey(
                        name: "FK_ProjectGlobalScheduleConfiguration_ProjectGlobalConfiguration_ProjectGlobalConfigurationId",
                        column: x => x.ProjectGlobalConfigurationId,
                        principalTable: "ProjectGlobalConfiguration",
                        principalColumn: "ProjectGlobalConfigurationId",
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
                    Inaccesible = table.Column<bool>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
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
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 983, DateTimeKind.Utc).AddTicks(986), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 983, DateTimeKind.Utc).AddTicks(8977), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 984, DateTimeKind.Utc).AddTicks(7783), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 985, DateTimeKind.Utc).AddTicks(5946), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 986, DateTimeKind.Utc).AddTicks(3388), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 987, DateTimeKind.Utc).AddTicks(1860), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 987, DateTimeKind.Utc).AddTicks(9620), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 988, DateTimeKind.Utc).AddTicks(7752), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 989, DateTimeKind.Utc).AddTicks(6658), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 990, DateTimeKind.Utc).AddTicks(4655), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 991, DateTimeKind.Utc).AddTicks(1625), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 991, DateTimeKind.Utc).AddTicks(9803), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 992, DateTimeKind.Utc).AddTicks(8192), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 993, DateTimeKind.Utc).AddTicks(5539), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 994, DateTimeKind.Utc).AddTicks(3010), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 995, DateTimeKind.Utc).AddTicks(1003), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 995, DateTimeKind.Utc).AddTicks(9536), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 996, DateTimeKind.Utc).AddTicks(6800), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 997, DateTimeKind.Utc).AddTicks(4062), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 998, DateTimeKind.Utc).AddTicks(2171), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 998, DateTimeKind.Utc).AddTicks(9371), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 999, DateTimeKind.Utc).AddTicks(6188), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 0, DateTimeKind.Utc).AddTicks(3854), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 1, DateTimeKind.Utc).AddTicks(2788), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 2, DateTimeKind.Utc).AddTicks(1168), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 2, DateTimeKind.Utc).AddTicks(8832), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 3, DateTimeKind.Utc).AddTicks(6943), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 4, DateTimeKind.Utc).AddTicks(5772), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 5, DateTimeKind.Utc).AddTicks(3719), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 6, DateTimeKind.Utc).AddTicks(1963), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 7, DateTimeKind.Utc).AddTicks(46), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 7, DateTimeKind.Utc).AddTicks(7513), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 8, DateTimeKind.Utc).AddTicks(5340), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 9, DateTimeKind.Utc).AddTicks(4651), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 10, DateTimeKind.Utc).AddTicks(2947), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 10, DateTimeKind.Utc).AddTicks(8436), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 11, DateTimeKind.Utc).AddTicks(3646), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 11, DateTimeKind.Utc).AddTicks(8704), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 12, DateTimeKind.Utc).AddTicks(3497), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 12, DateTimeKind.Utc).AddTicks(8311), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 13, DateTimeKind.Utc).AddTicks(3281), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 13, DateTimeKind.Utc).AddTicks(8483), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 14, DateTimeKind.Utc).AddTicks(3253), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 14, DateTimeKind.Utc).AddTicks(8153), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 15, DateTimeKind.Utc).AddTicks(3082), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 15, DateTimeKind.Utc).AddTicks(7879), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 16, DateTimeKind.Utc).AddTicks(2943), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 16, DateTimeKind.Utc).AddTicks(9179), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 17, DateTimeKind.Utc).AddTicks(6407), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 18, DateTimeKind.Utc).AddTicks(1763), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 18, DateTimeKind.Utc).AddTicks(6918), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 19, DateTimeKind.Utc).AddTicks(1897), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 19, DateTimeKind.Utc).AddTicks(7028), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 20, DateTimeKind.Utc).AddTicks(2449), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 20, DateTimeKind.Utc).AddTicks(7507), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 21, DateTimeKind.Utc).AddTicks(2351), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 21, DateTimeKind.Utc).AddTicks(7137), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 22, DateTimeKind.Utc).AddTicks(2107), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 22, DateTimeKind.Utc).AddTicks(6959), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 23, DateTimeKind.Utc).AddTicks(2575), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 24, DateTimeKind.Utc).AddTicks(1998), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 24, DateTimeKind.Utc).AddTicks(8287), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 25, DateTimeKind.Utc).AddTicks(3294), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 25, DateTimeKind.Utc).AddTicks(8185), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 26, DateTimeKind.Utc).AddTicks(3132), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 26, DateTimeKind.Utc).AddTicks(8222), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 27, DateTimeKind.Utc).AddTicks(2981), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 27, DateTimeKind.Utc).AddTicks(7760), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 28, DateTimeKind.Utc).AddTicks(2689), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 28, DateTimeKind.Utc).AddTicks(7549), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 29, DateTimeKind.Utc).AddTicks(2460), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 29, DateTimeKind.Utc).AddTicks(7533), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 30, DateTimeKind.Utc).AddTicks(3187), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 30, DateTimeKind.Utc).AddTicks(9216), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 31, DateTimeKind.Utc).AddTicks(5122), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 32, DateTimeKind.Utc).AddTicks(1983), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 33, DateTimeKind.Utc).AddTicks(152), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 34, DateTimeKind.Utc).AddTicks(661), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 34, DateTimeKind.Utc).AddTicks(9289), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 35, DateTimeKind.Utc).AddTicks(6582), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 36, DateTimeKind.Utc).AddTicks(3569), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 37, DateTimeKind.Utc).AddTicks(2501), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 38, DateTimeKind.Utc).AddTicks(1196), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 38, DateTimeKind.Utc).AddTicks(8747), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 39, DateTimeKind.Utc).AddTicks(6408), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 40, DateTimeKind.Utc).AddTicks(3468), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 41, DateTimeKind.Utc).AddTicks(1168), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 41, DateTimeKind.Utc).AddTicks(8350), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 42, DateTimeKind.Utc).AddTicks(5334), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 43, DateTimeKind.Utc).AddTicks(2883), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 44, DateTimeKind.Utc).AddTicks(943), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 44, DateTimeKind.Utc).AddTicks(8149), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 45, DateTimeKind.Utc).AddTicks(5353), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 46, DateTimeKind.Utc).AddTicks(2358), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 46, DateTimeKind.Utc).AddTicks(9940), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 47, DateTimeKind.Utc).AddTicks(8607), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 48, DateTimeKind.Utc).AddTicks(7928), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 49, DateTimeKind.Utc).AddTicks(3802), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 49, DateTimeKind.Utc).AddTicks(9302), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 50, DateTimeKind.Utc).AddTicks(4547), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 50, DateTimeKind.Utc).AddTicks(9739), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 51, DateTimeKind.Utc).AddTicks(5204), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 52, DateTimeKind.Utc).AddTicks(598), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 52, DateTimeKind.Utc).AddTicks(8715), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 53, DateTimeKind.Utc).AddTicks(6967), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 54, DateTimeKind.Utc).AddTicks(4434), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 55, DateTimeKind.Utc).AddTicks(1880), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 55, DateTimeKind.Utc).AddTicks(9870), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 56, DateTimeKind.Utc).AddTicks(7734), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 57, DateTimeKind.Utc).AddTicks(5755), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 58, DateTimeKind.Utc).AddTicks(3543), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 59, DateTimeKind.Utc).AddTicks(1431), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 59, DateTimeKind.Utc).AddTicks(7259), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 60, DateTimeKind.Utc).AddTicks(3001), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 60, DateTimeKind.Utc).AddTicks(8544), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 61, DateTimeKind.Utc).AddTicks(3904), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 61, DateTimeKind.Utc).AddTicks(9707), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 62, DateTimeKind.Utc).AddTicks(5107), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 63, DateTimeKind.Utc).AddTicks(614), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 64, DateTimeKind.Utc).AddTicks(3464), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 65, DateTimeKind.Utc).AddTicks(1159), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 66, DateTimeKind.Utc).AddTicks(92), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 68, DateTimeKind.Utc).AddTicks(2045), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 69, DateTimeKind.Utc).AddTicks(873), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 69, DateTimeKind.Utc).AddTicks(8963), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 70, DateTimeKind.Utc).AddTicks(8824), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 71, DateTimeKind.Utc).AddTicks(7080), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 72, DateTimeKind.Utc).AddTicks(9851), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 74, DateTimeKind.Utc).AddTicks(2880), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 75, DateTimeKind.Utc).AddTicks(2543), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 76, DateTimeKind.Utc).AddTicks(6332), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 77, DateTimeKind.Utc).AddTicks(5306), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 78, DateTimeKind.Utc).AddTicks(3000), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 79, DateTimeKind.Utc).AddTicks(3708), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 80, DateTimeKind.Utc).AddTicks(8379), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 81, DateTimeKind.Utc).AddTicks(9990), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 83, DateTimeKind.Utc).AddTicks(212), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 84, DateTimeKind.Utc).AddTicks(562), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 85, DateTimeKind.Utc).AddTicks(892), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 85, DateTimeKind.Utc).AddTicks(9781), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 86, DateTimeKind.Utc).AddTicks(7710), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 87, DateTimeKind.Utc).AddTicks(4498), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 88, DateTimeKind.Utc).AddTicks(1959), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 88, DateTimeKind.Utc).AddTicks(8631), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 89, DateTimeKind.Utc).AddTicks(4821), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 90, DateTimeKind.Utc).AddTicks(4020), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 91, DateTimeKind.Utc).AddTicks(5563), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 92, DateTimeKind.Utc).AddTicks(5435), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 93, DateTimeKind.Utc).AddTicks(5015), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 94, DateTimeKind.Utc).AddTicks(3593), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 95, DateTimeKind.Utc).AddTicks(3549), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 96, DateTimeKind.Utc).AddTicks(5227), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 97, DateTimeKind.Utc).AddTicks(6081), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 98, DateTimeKind.Utc).AddTicks(4769), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 99, DateTimeKind.Utc).AddTicks(3037), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 100, DateTimeKind.Utc).AddTicks(1256), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 100, DateTimeKind.Utc).AddTicks(9670), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 101, DateTimeKind.Utc).AddTicks(6213), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 102, DateTimeKind.Utc).AddTicks(1856), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 102, DateTimeKind.Utc).AddTicks(7592), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 103, DateTimeKind.Utc).AddTicks(3583), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 103, DateTimeKind.Utc).AddTicks(9397), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 104, DateTimeKind.Utc).AddTicks(4756), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 105, DateTimeKind.Utc).AddTicks(44), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 105, DateTimeKind.Utc).AddTicks(5441), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 106, DateTimeKind.Utc).AddTicks(1297), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 106, DateTimeKind.Utc).AddTicks(7303), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 107, DateTimeKind.Utc).AddTicks(4236), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 108, DateTimeKind.Utc).AddTicks(2857), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 109, DateTimeKind.Utc).AddTicks(7105), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 110, DateTimeKind.Utc).AddTicks(4486), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 113, DateTimeKind.Utc).AddTicks(993), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 113, DateTimeKind.Utc).AddTicks(9054), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 114, DateTimeKind.Utc).AddTicks(4834), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 115, DateTimeKind.Utc).AddTicks(141), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 115, DateTimeKind.Utc).AddTicks(5404), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 116, DateTimeKind.Utc).AddTicks(1423), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 116, DateTimeKind.Utc).AddTicks(6988), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 117, DateTimeKind.Utc).AddTicks(2657), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 117, DateTimeKind.Utc).AddTicks(7886), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 118, DateTimeKind.Utc).AddTicks(2968), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 118, DateTimeKind.Utc).AddTicks(8175), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 119, DateTimeKind.Utc).AddTicks(3408), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 119, DateTimeKind.Utc).AddTicks(8463), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 120, DateTimeKind.Utc).AddTicks(3730), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 120, DateTimeKind.Utc).AddTicks(9392), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 121, DateTimeKind.Utc).AddTicks(4405), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 121, DateTimeKind.Utc).AddTicks(9521), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 122, DateTimeKind.Utc).AddTicks(4690), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 123, DateTimeKind.Utc).AddTicks(110), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 123, DateTimeKind.Utc).AddTicks(5286), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 124, DateTimeKind.Utc).AddTicks(3204), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 125, DateTimeKind.Utc).AddTicks(4449), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 126, DateTimeKind.Utc).AddTicks(5004), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 127, DateTimeKind.Utc).AddTicks(1807), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 128, DateTimeKind.Utc).AddTicks(1166), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 129, DateTimeKind.Utc).AddTicks(744), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 129, DateTimeKind.Utc).AddTicks(9689), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 130, DateTimeKind.Utc).AddTicks(7572), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 131, DateTimeKind.Utc).AddTicks(3644), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 131, DateTimeKind.Utc).AddTicks(9012), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 132, DateTimeKind.Utc).AddTicks(4119), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 46, 132, DateTimeKind.Utc).AddTicks(9230), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 963, DateTimeKind.Utc).AddTicks(4475), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 964, DateTimeKind.Utc).AddTicks(45), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 964, DateTimeKind.Utc).AddTicks(5996), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 965, DateTimeKind.Utc).AddTicks(1491), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 965, DateTimeKind.Utc).AddTicks(6921), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 966, DateTimeKind.Utc).AddTicks(2328), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 504, "SB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 966, DateTimeKind.Utc).AddTicks(7671), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 505, "AB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 967, DateTimeKind.Utc).AddTicks(3232), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 979, DateTimeKind.Utc).AddTicks(5795), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 980, DateTimeKind.Utc).AddTicks(3883), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 981, DateTimeKind.Utc).AddTicks(1740), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(55), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 967, DateTimeKind.Utc).AddTicks(9127), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "eng" },
                    { 2, "Theme", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 968, DateTimeKind.Utc).AddTicks(4641), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Light" },
                    { 3, "Color", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 969, DateTimeKind.Utc).AddTicks(2735), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 970, DateTimeKind.Utc).AddTicks(1262), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "Host", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 970, DateTimeKind.Utc).AddTicks(8615), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "192.168.50.137" },
                    { 6, "Port", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 971, DateTimeKind.Utc).AddTicks(6653), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 971, DateTimeKind.Utc).AddTicks(6763), "Euro vs US Dollar", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 972, DateTimeKind.Utc).AddTicks(4605), "", false, false, "1 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "1" },
                    { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 973, DateTimeKind.Utc).AddTicks(2277), "", false, false, "5 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "5" },
                    { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 974, DateTimeKind.Utc).AddTicks(593), "", false, false, "15 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "15" },
                    { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 974, DateTimeKind.Utc).AddTicks(9553), "", false, false, "30 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "30" },
                    { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 975, DateTimeKind.Utc).AddTicks(8609), "", false, false, "1 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16385" },
                    { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 976, DateTimeKind.Utc).AddTicks(6467), "", false, false, "4 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16388" },
                    { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 977, DateTimeKind.Utc).AddTicks(3749), "", false, false, "Daily", "22222222-2222-2222-2222-222222222222", null, null, null, "16408" },
                    { 8, "W1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 978, DateTimeKind.Utc).AddTicks(878), "", false, false, "Weekly", "22222222-2222-2222-2222-222222222222", null, null, null, "32769" },
                    { 9, "MN1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 978, DateTimeKind.Utc).AddTicks(8209), "", false, false, "Monthly", "22222222-2222-2222-2222-222222222222", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "ABMinImprovePercent", "ABTransactionsTarget", "CreatedById", "CreatedByUserName", "CreatedOn", "DepthWeka", "Description", "EndDate", "ExtractorMinVariation", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxRatioTree", "MaximumSeed", "MinimalSeed", "NTotalTree", "Progressiveness", "SBMaxPercentCorrelation", "SBMaxTransactionsVariation", "SBMinPercentSuccessIS", "SBMinPercentSuccessOS", "SBMinTransactionsIS", "SBMinTransactionsOS", "SBTransactionsTarget", "SBWinningStrategyDOWNTarget", "SBWinningStrategyUPTarget", "StartDate", "SymbolId", "TenantId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalDecimalWeka", "TotalInstanceWeka", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "WithoutSchedule" },
                values: new object[] { 1, 5m, 600, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(174), 6, "Default Configuration", null, 50, null, null, false, false, false, 1.5m, 1000000, 100, 300m, 2m, 2m, 4m, 55m, 55m, 300, 100, 300, 6, 6, new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(162), 1, "22222222-2222-2222-2222-222222222222", 5, null, null, 5, 1, null, null, null, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(248), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(246), "22222222-2222-2222-2222-222222222222", 82800, null, null, null },
                    { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(271), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(269), "22222222-2222-2222-2222-222222222222", 64800, null, null, null },
                    { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(276), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 5, 28, 23, 22, 45, 982, DateTimeKind.Utc).AddTicks(275), "22222222-2222-2222-2222-222222222222", 32400, null, null, null }
                });

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
                name: "IX_ProjectGlobalConfiguration_SymbolId",
                table: "ProjectGlobalConfiguration",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalConfiguration_TimeframeId",
                table: "ProjectGlobalConfiguration",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalScheduleConfiguration_MarketRegionId",
                table: "ProjectGlobalScheduleConfiguration",
                column: "MarketRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalScheduleConfiguration_ProjectGlobalConfigurationId",
                table: "ProjectGlobalScheduleConfiguration",
                column: "ProjectGlobalConfigurationId");

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
                name: "Currency");

            migrationBuilder.DropTable(
                name: "EntityServiceHost");

            migrationBuilder.DropTable(
                name: "ExpertAdvisor");

            migrationBuilder.DropTable(
                name: "HistoricalDataCandle");

            migrationBuilder.DropTable(
                name: "ProjectGlobalScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "ProjectScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.DropTable(
                name: "ProjectGlobalConfiguration");

            migrationBuilder.DropTable(
                name: "MarketRegion");

            migrationBuilder.DropTable(
                name: "ProjectConfiguration");

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
