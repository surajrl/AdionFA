using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdionFA.Infrastructure.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
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
                name: "CurrencySpread",
                columns: table => new
                {
                    CurrencySpreadId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_CurrencySpread", x => x.CurrencySpreadId);
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
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<string>(type: "TEXT", nullable: false),
                    ParentOrganizationId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LegalName = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_Organization_ParentOrganizationId",
                        column: x => x.ParentOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStep",
                columns: table => new
                {
                    ProjectStepId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_ProjectStep", x => x.ProjectStepId);
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
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectStepId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Project_ProjectStep_ProjectStepId",
                        column: x => x.ProjectStepId,
                        principalTable: "ProjectStep",
                        principalColumn: "ProjectStepId",
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
                    Variation = table.Column<int>(type: "INTEGER", nullable: false),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencySpreadId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustDepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinTransactionCountIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustMinTransactionCountIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinTransactionCountOS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustMinTransactionCountOS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    VariationTransaction = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustVariationTransaction = table.Column<decimal>(type: "TEXT", nullable: false),
                    Progressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustProgressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxPercentCorrelation = table.Column<decimal>(type: "TEXT", nullable: false),
                    WinningStrategyTotalUP = table.Column<int>(type: "INTEGER", nullable: false),
                    WinningStrategyTotalDOWN = table.Column<int>(type: "INTEGER", nullable: false),
                    AutoAdjustConfig = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxAdjustConfig = table.Column<int>(type: "INTEGER", nullable: false),
                    AsynchronousMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    TransactionTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAssemblyPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAssemblyIterations = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGlobalConfiguration", x => x.ProjectGlobalConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalConfiguration_CurrencySpread_CurrencySpreadId",
                        column: x => x.CurrencySpreadId,
                        principalTable: "CurrencySpread",
                        principalColumn: "CurrencySpreadId",
                        onDelete: ReferentialAction.Cascade);
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
                    Variation = table.Column<int>(type: "INTEGER", nullable: false),
                    FromDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateIS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FromDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ToDateOS = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WithoutSchedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeframeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencySpreadId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalInstanceWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    DepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustDepthWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalDecimalWeka = table.Column<int>(type: "INTEGER", nullable: false),
                    MinimalSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMaxRatioTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    NTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustNTotalTree = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinTransactionCountIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustMinTransactionCountIS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMinPercentSuccessIS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinTransactionCountOS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAdjustMinTransactionCountOS = table.Column<int>(type: "INTEGER", nullable: false),
                    MinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustMinPercentSuccessOS = table.Column<decimal>(type: "TEXT", nullable: false),
                    VariationTransaction = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustVariationTransaction = table.Column<decimal>(type: "TEXT", nullable: false),
                    Progressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    MinAdjustProgressiveness = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsProgressiveness = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxPercentCorrelation = table.Column<decimal>(type: "TEXT", nullable: false),
                    WinningStrategyTotalUP = table.Column<int>(type: "INTEGER", nullable: false),
                    WinningStrategyTotalDOWN = table.Column<int>(type: "INTEGER", nullable: false),
                    AutoAdjustConfig = table.Column<bool>(type: "INTEGER", nullable: false),
                    MaxAdjustConfig = table.Column<int>(type: "INTEGER", nullable: false),
                    AsynchronousMode = table.Column<bool>(type: "INTEGER", nullable: false),
                    TransactionTarget = table.Column<int>(type: "INTEGER", nullable: false),
                    MinAssemblyPercent = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAssemblyIterations = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectConfiguration", x => x.ProjectConfigurationId);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_CurrencySpread_CurrencySpreadId",
                        column: x => x.CurrencySpreadId,
                        principalTable: "CurrencySpread",
                        principalColumn: "CurrencySpreadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_HistoricalData_HistoricalDataId",
                        column: x => x.HistoricalDataId,
                        principalTable: "HistoricalData",
                        principalColumn: "HistoricalDataId",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "MarketRegionId",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "MarketRegionId",
                        onDelete: ReferentialAction.Restrict);
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
                values: new object[] { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 99, DateTimeKind.Utc).AddTicks(1497), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 119, DateTimeKind.Utc).AddTicks(8673), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 120, DateTimeKind.Utc).AddTicks(3940), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 120, DateTimeKind.Utc).AddTicks(9334), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 121, DateTimeKind.Utc).AddTicks(4580), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 121, DateTimeKind.Utc).AddTicks(9833), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 122, DateTimeKind.Utc).AddTicks(5013), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 123, DateTimeKind.Utc).AddTicks(298), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 123, DateTimeKind.Utc).AddTicks(5451), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 125, DateTimeKind.Utc).AddTicks(2840), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 126, DateTimeKind.Utc).AddTicks(942), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 126, DateTimeKind.Utc).AddTicks(8349), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 127, DateTimeKind.Utc).AddTicks(5745), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 128, DateTimeKind.Utc).AddTicks(3302), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 129, DateTimeKind.Utc).AddTicks(862), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 129, DateTimeKind.Utc).AddTicks(7673), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 130, DateTimeKind.Utc).AddTicks(5392), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 131, DateTimeKind.Utc).AddTicks(3701), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 132, DateTimeKind.Utc).AddTicks(4209), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 133, DateTimeKind.Utc).AddTicks(3273), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 134, DateTimeKind.Utc).AddTicks(1081), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 134, DateTimeKind.Utc).AddTicks(8442), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 135, DateTimeKind.Utc).AddTicks(5985), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 136, DateTimeKind.Utc).AddTicks(3406), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 137, DateTimeKind.Utc).AddTicks(291), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 137, DateTimeKind.Utc).AddTicks(8131), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 138, DateTimeKind.Utc).AddTicks(6165), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 139, DateTimeKind.Utc).AddTicks(3281), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 119, DateTimeKind.Utc).AddTicks(3443), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 118, DateTimeKind.Utc).AddTicks(8113), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 118, DateTimeKind.Utc).AddTicks(2801), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 117, DateTimeKind.Utc).AddTicks(7254), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 100, DateTimeKind.Utc).AddTicks(1652), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 100, DateTimeKind.Utc).AddTicks(6751), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 101, DateTimeKind.Utc).AddTicks(1952), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 101, DateTimeKind.Utc).AddTicks(7094), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 102, DateTimeKind.Utc).AddTicks(2111), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 102, DateTimeKind.Utc).AddTicks(7264), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 103, DateTimeKind.Utc).AddTicks(3026), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 103, DateTimeKind.Utc).AddTicks(9288), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 104, DateTimeKind.Utc).AddTicks(4451), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 104, DateTimeKind.Utc).AddTicks(9682), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 106, DateTimeKind.Utc).AddTicks(7459), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 107, DateTimeKind.Utc).AddTicks(3898), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 107, DateTimeKind.Utc).AddTicks(9513), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 140, DateTimeKind.Utc).AddTicks(663), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 108, DateTimeKind.Utc).AddTicks(5124), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 109, DateTimeKind.Utc).AddTicks(5952), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 110, DateTimeKind.Utc).AddTicks(1898), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 110, DateTimeKind.Utc).AddTicks(9935), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 111, DateTimeKind.Utc).AddTicks(5462), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 112, DateTimeKind.Utc).AddTicks(915), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 112, DateTimeKind.Utc).AddTicks(6207), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 113, DateTimeKind.Utc).AddTicks(1473), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 113, DateTimeKind.Utc).AddTicks(9994), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 114, DateTimeKind.Utc).AddTicks(5276), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 115, DateTimeKind.Utc).AddTicks(502), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 115, DateTimeKind.Utc).AddTicks(9228), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 116, DateTimeKind.Utc).AddTicks(4792), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 117, DateTimeKind.Utc).AddTicks(825), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 109, DateTimeKind.Utc).AddTicks(564), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 99, DateTimeKind.Utc).AddTicks(6546), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 140, DateTimeKind.Utc).AddTicks(8048), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 142, DateTimeKind.Utc).AddTicks(3103), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 179, DateTimeKind.Utc).AddTicks(5266), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 180, DateTimeKind.Utc).AddTicks(3203), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 181, DateTimeKind.Utc).AddTicks(1161), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 181, DateTimeKind.Utc).AddTicks(8328), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 182, DateTimeKind.Utc).AddTicks(6639), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 183, DateTimeKind.Utc).AddTicks(4748), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 184, DateTimeKind.Utc).AddTicks(284), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 184, DateTimeKind.Utc).AddTicks(5414), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 185, DateTimeKind.Utc).AddTicks(646), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 185, DateTimeKind.Utc).AddTicks(5729), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 186, DateTimeKind.Utc).AddTicks(1610), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 186, DateTimeKind.Utc).AddTicks(7288), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 187, DateTimeKind.Utc).AddTicks(5385), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 188, DateTimeKind.Utc).AddTicks(3207), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 189, DateTimeKind.Utc).AddTicks(536), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 189, DateTimeKind.Utc).AddTicks(7952), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 190, DateTimeKind.Utc).AddTicks(5085), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 191, DateTimeKind.Utc).AddTicks(2449), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 191, DateTimeKind.Utc).AddTicks(9830), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 192, DateTimeKind.Utc).AddTicks(7175), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 193, DateTimeKind.Utc).AddTicks(4836), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 194, DateTimeKind.Utc).AddTicks(2838), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 195, DateTimeKind.Utc).AddTicks(1521), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 195, DateTimeKind.Utc).AddTicks(9071), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 196, DateTimeKind.Utc).AddTicks(6306), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 197, DateTimeKind.Utc).AddTicks(2918), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 198, DateTimeKind.Utc).AddTicks(462), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 178, DateTimeKind.Utc).AddTicks(7559), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 178, DateTimeKind.Utc).AddTicks(118), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 177, DateTimeKind.Utc).AddTicks(2639), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 176, DateTimeKind.Utc).AddTicks(5354), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 142, DateTimeKind.Utc).AddTicks(9777), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 143, DateTimeKind.Utc).AddTicks(7228), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 144, DateTimeKind.Utc).AddTicks(5053), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 145, DateTimeKind.Utc).AddTicks(5499), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 146, DateTimeKind.Utc).AddTicks(7940), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 147, DateTimeKind.Utc).AddTicks(5837), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 148, DateTimeKind.Utc).AddTicks(9055), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 155, DateTimeKind.Utc).AddTicks(301), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 156, DateTimeKind.Utc).AddTicks(6399), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 158, DateTimeKind.Utc).AddTicks(517), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 159, DateTimeKind.Utc).AddTicks(4545), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 160, DateTimeKind.Utc).AddTicks(8865), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 162, DateTimeKind.Utc).AddTicks(3157), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 141, DateTimeKind.Utc).AddTicks(5416), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 163, DateTimeKind.Utc).AddTicks(7097), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 167, DateTimeKind.Utc).AddTicks(91), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 168, DateTimeKind.Utc).AddTicks(5059), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 169, DateTimeKind.Utc).AddTicks(3326), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 170, DateTimeKind.Utc).AddTicks(232), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 170, DateTimeKind.Utc).AddTicks(6663), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 171, DateTimeKind.Utc).AddTicks(2048), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 171, DateTimeKind.Utc).AddTicks(7096), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 172, DateTimeKind.Utc).AddTicks(2182), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 172, DateTimeKind.Utc).AddTicks(7913), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 173, DateTimeKind.Utc).AddTicks(4527), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 174, DateTimeKind.Utc).AddTicks(2888), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 175, DateTimeKind.Utc).AddTicks(734), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 175, DateTimeKind.Utc).AddTicks(8168), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 165, DateTimeKind.Utc).AddTicks(1758), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 198, DateTimeKind.Utc).AddTicks(8072), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 199, DateTimeKind.Utc).AddTicks(7490), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 98, DateTimeKind.Utc).AddTicks(1314), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 67, DateTimeKind.Utc).AddTicks(8684), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 68, DateTimeKind.Utc).AddTicks(3764), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 68, DateTimeKind.Utc).AddTicks(9823), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 69, DateTimeKind.Utc).AddTicks(6247), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 70, DateTimeKind.Utc).AddTicks(1495), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 70, DateTimeKind.Utc).AddTicks(6547), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 71, DateTimeKind.Utc).AddTicks(1564), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 71, DateTimeKind.Utc).AddTicks(6751), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 72, DateTimeKind.Utc).AddTicks(1834), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 72, DateTimeKind.Utc).AddTicks(6939), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 73, DateTimeKind.Utc).AddTicks(2064), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 73, DateTimeKind.Utc).AddTicks(7839), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 74, DateTimeKind.Utc).AddTicks(5058), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 75, DateTimeKind.Utc).AddTicks(2108), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 75, DateTimeKind.Utc).AddTicks(9688), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 67, DateTimeKind.Utc).AddTicks(3453), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 66, DateTimeKind.Utc).AddTicks(8361), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 66, DateTimeKind.Utc).AddTicks(3227), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 65, DateTimeKind.Utc).AddTicks(8031), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 57, DateTimeKind.Utc).AddTicks(109), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 57, DateTimeKind.Utc).AddTicks(5828), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 58, DateTimeKind.Utc).AddTicks(2043), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 58, DateTimeKind.Utc).AddTicks(8310), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 59, DateTimeKind.Utc).AddTicks(3480), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 59, DateTimeKind.Utc).AddTicks(8600), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 60, DateTimeKind.Utc).AddTicks(3837), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 76, DateTimeKind.Utc).AddTicks(7029), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 60, DateTimeKind.Utc).AddTicks(8884), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 61, DateTimeKind.Utc).AddTicks(9862), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 62, DateTimeKind.Utc).AddTicks(6450), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 63, DateTimeKind.Utc).AddTicks(1597), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 63, DateTimeKind.Utc).AddTicks(6682), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 98, DateTimeKind.Utc).AddTicks(6365), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 64, DateTimeKind.Utc).AddTicks(6994), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 65, DateTimeKind.Utc).AddTicks(2022), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 61, DateTimeKind.Utc).AddTicks(4002), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 77, DateTimeKind.Utc).AddTicks(2632), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 64, DateTimeKind.Utc).AddTicks(1870), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 78, DateTimeKind.Utc).AddTicks(3080), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 89, DateTimeKind.Utc).AddTicks(4123), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 90, DateTimeKind.Utc).AddTicks(1580), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 90, DateTimeKind.Utc).AddTicks(7333), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 91, DateTimeKind.Utc).AddTicks(2459), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 91, DateTimeKind.Utc).AddTicks(7616), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 92, DateTimeKind.Utc).AddTicks(2753), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 92, DateTimeKind.Utc).AddTicks(8018), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 88, DateTimeKind.Utc).AddTicks(7916), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 93, DateTimeKind.Utc).AddTicks(3147), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 94, DateTimeKind.Utc).AddTicks(8528), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 95, DateTimeKind.Utc).AddTicks(3659), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 95, DateTimeKind.Utc).AddTicks(8700), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 96, DateTimeKind.Utc).AddTicks(4646), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 97, DateTimeKind.Utc).AddTicks(521), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 97, DateTimeKind.Utc).AddTicks(6138), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 77, DateTimeKind.Utc).AddTicks(7872), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 94, DateTimeKind.Utc).AddTicks(3393), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 88, DateTimeKind.Utc).AddTicks(2793), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 93, DateTimeKind.Utc).AddTicks(8237), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 87, DateTimeKind.Utc).AddTicks(2660), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 78, DateTimeKind.Utc).AddTicks(8139), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 87, DateTimeKind.Utc).AddTicks(7747), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 79, DateTimeKind.Utc).AddTicks(3198), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 79, DateTimeKind.Utc).AddTicks(8317), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 80, DateTimeKind.Utc).AddTicks(3414), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 81, DateTimeKind.Utc).AddTicks(3716), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 81, DateTimeKind.Utc).AddTicks(8851), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 82, DateTimeKind.Utc).AddTicks(4525), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 80, DateTimeKind.Utc).AddTicks(8474), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 83, DateTimeKind.Utc).AddTicks(1131), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 83, DateTimeKind.Utc).AddTicks(6674), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 84, DateTimeKind.Utc).AddTicks(1837), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 84, DateTimeKind.Utc).AddTicks(6908), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 85, DateTimeKind.Utc).AddTicks(2103), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 85, DateTimeKind.Utc).AddTicks(7238), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 86, DateTimeKind.Utc).AddTicks(2358), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 86, DateTimeKind.Utc).AddTicks(7644), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "Six", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 13, DateTimeKind.Utc).AddTicks(6600), "", false, false, "Six", "22222222-2222-2222-2222-222222222222", null, null, null, "6" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "Seven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 14, DateTimeKind.Utc).AddTicks(3745), "", false, false, "Seven", "22222222-2222-2222-2222-222222222222", null, null, null, "7" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "Eight", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 14, DateTimeKind.Utc).AddTicks(8785), "", false, false, "Eight", "22222222-2222-2222-2222-222222222222", null, null, null, "8" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "Eleven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 16, DateTimeKind.Utc).AddTicks(3916), "", false, false, "Eleven", "22222222-2222-2222-2222-222222222222", null, null, null, "11" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "Ten", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 15, DateTimeKind.Utc).AddTicks(8891), "", false, false, "Ten", "22222222-2222-2222-2222-222222222222", null, null, null, "10" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "Fourteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 17, DateTimeKind.Utc).AddTicks(9234), "", false, false, "Fourteen", "22222222-2222-2222-2222-222222222222", null, null, null, "14" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "Fifteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 18, DateTimeKind.Utc).AddTicks(4677), "", false, false, "Fifteen", "22222222-2222-2222-2222-222222222222", null, null, null, "15" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "Five", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 13, DateTimeKind.Utc).AddTicks(318), "", false, false, "Five", "22222222-2222-2222-2222-222222222222", null, null, null, "5" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "Nine", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 15, DateTimeKind.Utc).AddTicks(3788), "", false, false, "Nine", "22222222-2222-2222-2222-222222222222", null, null, null, "9" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "Four", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 12, DateTimeKind.Utc).AddTicks(5317), "", false, false, "Four", "22222222-2222-2222-2222-222222222222", null, null, null, "4" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "Twelve", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 16, DateTimeKind.Utc).AddTicks(9247), "", false, false, "Twelve", "22222222-2222-2222-2222-222222222222", null, null, null, "12" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "Two", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 10, DateTimeKind.Utc).AddTicks(8774), "", false, false, "Two", "22222222-2222-2222-2222-222222222222", null, null, null, "2" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "One", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 10, DateTimeKind.Utc).AddTicks(3325), "", false, false, "One", "22222222-2222-2222-2222-222222222222", null, null, null, "1" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "Thirteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 17, DateTimeKind.Utc).AddTicks(4193), "", false, false, "Thirteen", "22222222-2222-2222-2222-222222222222", null, null, null, "13" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "Three", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 11, DateTimeKind.Utc).AddTicks(9904), "", false, false, "Three", "22222222-2222-2222-2222-222222222222", null, null, null, "3" });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 505, "ASSBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 5, DateTimeKind.Utc).AddTicks(7358), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 504, "STRBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 5, DateTimeKind.Utc).AddTicks(2186), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 4, DateTimeKind.Utc).AddTicks(7032), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 4, DateTimeKind.Utc).AddTicks(1987), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 3, DateTimeKind.Utc).AddTicks(1656), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 2, DateTimeKind.Utc).AddTicks(6234), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 3, DateTimeKind.Utc).AddTicks(6823), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 0, DateTimeKind.Utc).AddTicks(3665), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 24, DateTimeKind.Utc).AddTicks(3222), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 25, DateTimeKind.Utc).AddTicks(371), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 25, DateTimeKind.Utc).AddTicks(5803), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 26, DateTimeKind.Utc).AddTicks(881), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "LegalName", "Name", "ParentOrganizationId", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 50, DateTimeKind.Utc).AddTicks(2684), false, false, "AdionFA", "AdionFA", null, "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "ChileanTrees", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 55, DateTimeKind.Utc).AddTicks(9421), "", false, false, "Chilean Trees", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "MacroTransformation", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 55, DateTimeKind.Utc).AddTicks(2958), "", false, false, "Macro Transformation", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "DataExtractor", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 54, DateTimeKind.Utc).AddTicks(7277), "", false, false, "Data Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "InitialConfiguration", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 54, DateTimeKind.Utc).AddTicks(1584), "", false, false, "Initial Configuration", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Culture", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 6, DateTimeKind.Utc).AddTicks(5869), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "eng" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "Theme", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 7, DateTimeKind.Utc).AddTicks(3090), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Light" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "Color", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 7, DateTimeKind.Utc).AddTicks(8271), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Orange" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "DefaultWorkspace", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 8, DateTimeKind.Utc).AddTicks(3272), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "Port", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 9, DateTimeKind.Utc).AddTicks(3361), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "5555" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "Host", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 8, DateTimeKind.Utc).AddTicks(8217), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "127.0.0.1" });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 9, DateTimeKind.Utc).AddTicks(5186), "Euro vs US Dollar", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 22, DateTimeKind.Utc).AddTicks(620), "", false, false, "4 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16388" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 21, DateTimeKind.Utc).AddTicks(5548), "", false, false, "1 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16385" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 22, DateTimeKind.Utc).AddTicks(5589), "", false, false, "Daily", "22222222-2222-2222-2222-222222222222", null, null, null, "16408" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "W1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 23, DateTimeKind.Utc).AddTicks(597), "", false, false, "Weekly", "22222222-2222-2222-2222-222222222222", null, null, null, "32769" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 21, DateTimeKind.Utc).AddTicks(464), "", false, false, "30 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "30" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 20, DateTimeKind.Utc).AddTicks(5043), "", false, false, "15 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "15" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 19, DateTimeKind.Utc).AddTicks(9165), "", false, false, "5 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "5" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 19, DateTimeKind.Utc).AddTicks(2909), "", false, false, "1 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "1" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "MN1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 23, DateTimeKind.Utc).AddTicks(5662), "", false, false, "Monthly", "22222222-2222-2222-2222-222222222222", null, null, null, "49153" });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "AsynchronousMode", "AutoAdjustConfig", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencySpreadId", "DepthWeka", "Description", "EndDate", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxAdjustConfig", "MaxPercentCorrelation", "MaxRatioTree", "MaximumSeed", "MinAdjustDepthWeka", "MinAdjustMaxRatioTree", "MinAdjustMinPercentSuccessIS", "MinAdjustMinPercentSuccessOS", "MinAdjustMinTransactionCountIS", "MinAdjustMinTransactionCountOS", "MinAdjustNTotalTree", "MinAdjustProgressiveness", "MinAdjustVariationTransaction", "MinAssemblyPercent", "MinPercentSuccessIS", "MinPercentSuccessOS", "MinTransactionCountIS", "MinTransactionCountOS", "MinimalSeed", "NTotalTree", "Progressiveness", "StartDate", "SymbolId", "TenantId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalAssemblyIterations", "TotalDecimalWeka", "TotalInstanceWeka", "TransactionTarget", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Variation", "VariationTransaction", "WinningStrategyTotalDOWN", "WinningStrategyTotalUP", "WithoutSchedule" },
                values: new object[] { 1, false, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 52, DateTimeKind.Utc).AddTicks(8295), 5, 6, "Default Configuration", null, null, null, false, false, false, 5, 2m, 1.5m, 1000000, 5, 1m, 50m, 50m, 360, 180, 150m, 2m, 4m, 5m, 55m, 55m, 300, 100, 100, 300m, 2m, new DateTime(2023, 4, 11, 21, 57, 3, 52, DateTimeKind.Utc).AddTicks(6955), 1, "22222222-2222-2222-2222-222222222222", 5, null, null, 1, 5, 1, 600, null, null, null, 50, 4m, 6, 6, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1503), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1500), "22222222-2222-2222-2222-222222222222", 82800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1512), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1511), "22222222-2222-2222-2222-222222222222", 64800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1517), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 4, 11, 21, 57, 3, 53, DateTimeKind.Utc).AddTicks(1515), "22222222-2222-2222-2222-222222222222", 32400, null, null, null });

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
                name: "IX_Organization_ParentOrganizationId",
                table: "Organization",
                column: "ParentOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectName",
                table: "Project",
                column: "ProjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectStepId",
                table: "Project",
                column: "ProjectStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_CurrencySpreadId",
                table: "ProjectConfiguration",
                column: "CurrencySpreadId");

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
                name: "IX_ProjectGlobalConfiguration_CurrencySpreadId",
                table: "ProjectGlobalConfiguration",
                column: "CurrencySpreadId");

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
                name: "Organization");

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
                name: "CurrencySpread");

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

            migrationBuilder.DropTable(
                name: "ProjectStep");
        }
    }
}
