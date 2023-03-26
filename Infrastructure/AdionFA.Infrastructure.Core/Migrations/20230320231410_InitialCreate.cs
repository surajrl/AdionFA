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
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
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
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                    Protocol = table.Column<string>(type: "TEXT", nullable: true),
                    HostName = table.Column<string>(type: "TEXT", nullable: true),
                    REPPort = table.Column<int>(type: "INTEGER", nullable: false),
                    PUSHPort = table.Column<int>(type: "INTEGER", nullable: false),
                    Timer = table.Column<int>(type: "INTEGER", nullable: false),
                    MagicNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumOrders = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumLotSize = table.Column<double>(type: "REAL", nullable: false),
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
                name: "HistoricalDataDetail",
                columns: table => new
                {
                    HistoricalDataDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HistoricalDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<long>(type: "INTEGER", nullable: false),
                    OpenPrice = table.Column<double>(type: "REAL", nullable: false),
                    MaxPrice = table.Column<double>(type: "REAL", nullable: false),
                    MinPrice = table.Column<double>(type: "REAL", nullable: false),
                    ClosePrice = table.Column<double>(type: "REAL", nullable: false),
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
                    table.PrimaryKey("PK_HistoricalDataDetail", x => x.HistoricalDataDetailId);
                    table.ForeignKey(
                        name: "FK_HistoricalDataDetail_HistoricalData_HistoricalDataId",
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
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 325, DateTimeKind.Utc).AddTicks(808), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 416, DateTimeKind.Utc).AddTicks(1669), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 417, DateTimeKind.Utc).AddTicks(3219), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 419, DateTimeKind.Utc).AddTicks(1410), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 421, DateTimeKind.Utc).AddTicks(9469), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 422, DateTimeKind.Utc).AddTicks(8649), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 429, DateTimeKind.Utc).AddTicks(4519), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 431, DateTimeKind.Utc).AddTicks(5272), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 432, DateTimeKind.Utc).AddTicks(4673), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 433, DateTimeKind.Utc).AddTicks(3419), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 434, DateTimeKind.Utc).AddTicks(3030), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 435, DateTimeKind.Utc).AddTicks(996), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 436, DateTimeKind.Utc).AddTicks(7192), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 440, DateTimeKind.Utc).AddTicks(7379), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 441, DateTimeKind.Utc).AddTicks(8348), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 442, DateTimeKind.Utc).AddTicks(9173), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 444, DateTimeKind.Utc).AddTicks(1854), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 445, DateTimeKind.Utc).AddTicks(796), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 445, DateTimeKind.Utc).AddTicks(8860), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 446, DateTimeKind.Utc).AddTicks(6692), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 447, DateTimeKind.Utc).AddTicks(6605), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 449, DateTimeKind.Utc).AddTicks(1392), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 450, DateTimeKind.Utc).AddTicks(1921), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 450, DateTimeKind.Utc).AddTicks(9724), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 451, DateTimeKind.Utc).AddTicks(8418), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 452, DateTimeKind.Utc).AddTicks(6128), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 454, DateTimeKind.Utc).AddTicks(3963), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 461, DateTimeKind.Utc).AddTicks(1919), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 407, DateTimeKind.Utc).AddTicks(7098), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 406, DateTimeKind.Utc).AddTicks(1832), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 404, DateTimeKind.Utc).AddTicks(8070), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 403, DateTimeKind.Utc).AddTicks(246), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 327, DateTimeKind.Utc).AddTicks(1931), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 328, DateTimeKind.Utc).AddTicks(386), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 336, DateTimeKind.Utc).AddTicks(5151), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 337, DateTimeKind.Utc).AddTicks(3452), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 338, DateTimeKind.Utc).AddTicks(7352), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 339, DateTimeKind.Utc).AddTicks(9365), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 341, DateTimeKind.Utc).AddTicks(3225), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 349, DateTimeKind.Utc).AddTicks(5401), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 351, DateTimeKind.Utc).AddTicks(655), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 352, DateTimeKind.Utc).AddTicks(6295), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 353, DateTimeKind.Utc).AddTicks(7857), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 355, DateTimeKind.Utc).AddTicks(7561), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 357, DateTimeKind.Utc).AddTicks(7723), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 463, DateTimeKind.Utc).AddTicks(3045), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 359, DateTimeKind.Utc).AddTicks(3662), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 366, DateTimeKind.Utc).AddTicks(9933), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 368, DateTimeKind.Utc).AddTicks(7759), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 370, DateTimeKind.Utc).AddTicks(7460), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 372, DateTimeKind.Utc).AddTicks(3697), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 374, DateTimeKind.Utc).AddTicks(8874), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 376, DateTimeKind.Utc).AddTicks(3868), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 383, DateTimeKind.Utc).AddTicks(7535), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 385, DateTimeKind.Utc).AddTicks(9176), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 387, DateTimeKind.Utc).AddTicks(4729), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 392, DateTimeKind.Utc).AddTicks(7281), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 394, DateTimeKind.Utc).AddTicks(2110), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 395, DateTimeKind.Utc).AddTicks(479), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 400, DateTimeKind.Utc).AddTicks(4101), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 363, DateTimeKind.Utc).AddTicks(6096), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 326, DateTimeKind.Utc).AddTicks(3588), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 467, DateTimeKind.Utc).AddTicks(2888), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 469, DateTimeKind.Utc).AddTicks(6243), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 520, DateTimeKind.Utc).AddTicks(6185), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 524, DateTimeKind.Utc).AddTicks(2467), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 525, DateTimeKind.Utc).AddTicks(7871), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 526, DateTimeKind.Utc).AddTicks(6435), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 527, DateTimeKind.Utc).AddTicks(3811), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 528, DateTimeKind.Utc).AddTicks(5714), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 529, DateTimeKind.Utc).AddTicks(4507), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 530, DateTimeKind.Utc).AddTicks(3468), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 531, DateTimeKind.Utc).AddTicks(2975), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 532, DateTimeKind.Utc).AddTicks(1593), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 532, DateTimeKind.Utc).AddTicks(9487), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 533, DateTimeKind.Utc).AddTicks(7561), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 534, DateTimeKind.Utc).AddTicks(5876), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 535, DateTimeKind.Utc).AddTicks(4500), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 536, DateTimeKind.Utc).AddTicks(3245), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 537, DateTimeKind.Utc).AddTicks(8784), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 540, DateTimeKind.Utc).AddTicks(471), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 540, DateTimeKind.Utc).AddTicks(9106), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 541, DateTimeKind.Utc).AddTicks(6805), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 542, DateTimeKind.Utc).AddTicks(4943), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 543, DateTimeKind.Utc).AddTicks(2508), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 543, DateTimeKind.Utc).AddTicks(9864), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 544, DateTimeKind.Utc).AddTicks(7313), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 545, DateTimeKind.Utc).AddTicks(4842), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 546, DateTimeKind.Utc).AddTicks(2843), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 547, DateTimeKind.Utc).AddTicks(3750), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 548, DateTimeKind.Utc).AddTicks(5586), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 518, DateTimeKind.Utc).AddTicks(3223), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 517, DateTimeKind.Utc).AddTicks(2531), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 516, DateTimeKind.Utc).AddTicks(5521), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 515, DateTimeKind.Utc).AddTicks(6617), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 470, DateTimeKind.Utc).AddTicks(6367), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 471, DateTimeKind.Utc).AddTicks(4761), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 478, DateTimeKind.Utc).AddTicks(7200), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 479, DateTimeKind.Utc).AddTicks(6199), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 480, DateTimeKind.Utc).AddTicks(3499), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 481, DateTimeKind.Utc).AddTicks(1668), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 481, DateTimeKind.Utc).AddTicks(9415), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 483, DateTimeKind.Utc).AddTicks(73), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 483, DateTimeKind.Utc).AddTicks(8228), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 484, DateTimeKind.Utc).AddTicks(6173), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 493, DateTimeKind.Utc).AddTicks(1384), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 493, DateTimeKind.Utc).AddTicks(8371), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 494, DateTimeKind.Utc).AddTicks(5709), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 468, DateTimeKind.Utc).AddTicks(2901), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 501, DateTimeKind.Utc).AddTicks(395), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 503, DateTimeKind.Utc).AddTicks(406), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 504, DateTimeKind.Utc).AddTicks(57), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 505, DateTimeKind.Utc).AddTicks(3784), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 506, DateTimeKind.Utc).AddTicks(4896), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 507, DateTimeKind.Utc).AddTicks(4021), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 508, DateTimeKind.Utc).AddTicks(2599), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 508, DateTimeKind.Utc).AddTicks(9673), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 509, DateTimeKind.Utc).AddTicks(8788), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 511, DateTimeKind.Utc).AddTicks(1042), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 511, DateTimeKind.Utc).AddTicks(9866), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 512, DateTimeKind.Utc).AddTicks(8521), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 513, DateTimeKind.Utc).AddTicks(8397), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 514, DateTimeKind.Utc).AddTicks(6351), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 502, DateTimeKind.Utc).AddTicks(2448), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 555, DateTimeKind.Utc).AddTicks(2863), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 555, DateTimeKind.Utc).AddTicks(9964), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 322, DateTimeKind.Utc).AddTicks(6831), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 198, DateTimeKind.Utc).AddTicks(9990), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 199, DateTimeKind.Utc).AddTicks(8619), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 200, DateTimeKind.Utc).AddTicks(9951), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 202, DateTimeKind.Utc).AddTicks(83), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 203, DateTimeKind.Utc).AddTicks(5348), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 205, DateTimeKind.Utc).AddTicks(5339), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 206, DateTimeKind.Utc).AddTicks(6953), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 211, DateTimeKind.Utc).AddTicks(493), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 218, DateTimeKind.Utc).AddTicks(4308), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 227, DateTimeKind.Utc).AddTicks(3692), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 228, DateTimeKind.Utc).AddTicks(3768), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 229, DateTimeKind.Utc).AddTicks(1981), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 230, DateTimeKind.Utc).AddTicks(571), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 231, DateTimeKind.Utc).AddTicks(1684), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 232, DateTimeKind.Utc).AddTicks(3630), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 198, DateTimeKind.Utc).AddTicks(2026), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 197, DateTimeKind.Utc).AddTicks(2881), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 196, DateTimeKind.Utc).AddTicks(4367), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 195, DateTimeKind.Utc).AddTicks(2923), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 169, DateTimeKind.Utc).AddTicks(689), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 170, DateTimeKind.Utc).AddTicks(2461), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 173, DateTimeKind.Utc).AddTicks(1287), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 174, DateTimeKind.Utc).AddTicks(1103), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 175, DateTimeKind.Utc).AddTicks(11), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 175, DateTimeKind.Utc).AddTicks(7950), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 176, DateTimeKind.Utc).AddTicks(8122), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 233, DateTimeKind.Utc).AddTicks(2173), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 177, DateTimeKind.Utc).AddTicks(7136), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 185, DateTimeKind.Utc).AddTicks(8941), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 186, DateTimeKind.Utc).AddTicks(9014), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 189, DateTimeKind.Utc).AddTicks(5966), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 190, DateTimeKind.Utc).AddTicks(8039), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 323, DateTimeKind.Utc).AddTicks(6818), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 192, DateTimeKind.Utc).AddTicks(8202), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 194, DateTimeKind.Utc).AddTicks(3121), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 178, DateTimeKind.Utc).AddTicks(5339), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 233, DateTimeKind.Utc).AddTicks(9998), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 191, DateTimeKind.Utc).AddTicks(9298), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 243, DateTimeKind.Utc).AddTicks(7713), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 307, DateTimeKind.Utc).AddTicks(3665), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 308, DateTimeKind.Utc).AddTicks(1342), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 308, DateTimeKind.Utc).AddTicks(8842), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 309, DateTimeKind.Utc).AddTicks(7204), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 310, DateTimeKind.Utc).AddTicks(5183), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 311, DateTimeKind.Utc).AddTicks(6531), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 312, DateTimeKind.Utc).AddTicks(4571), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 306, DateTimeKind.Utc).AddTicks(5953), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 314, DateTimeKind.Utc).AddTicks(864), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 317, DateTimeKind.Utc).AddTicks(3176), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 318, DateTimeKind.Utc).AddTicks(1274), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 318, DateTimeKind.Utc).AddTicks(9364), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 319, DateTimeKind.Utc).AddTicks(8405), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 320, DateTimeKind.Utc).AddTicks(7636), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 321, DateTimeKind.Utc).AddTicks(5464), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 242, DateTimeKind.Utc).AddTicks(4557), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 316, DateTimeKind.Utc).AddTicks(3495), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 305, DateTimeKind.Utc).AddTicks(7756), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 315, DateTimeKind.Utc).AddTicks(4169), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 304, DateTimeKind.Utc).AddTicks(1678), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 251, DateTimeKind.Utc).AddTicks(2827), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 304, DateTimeKind.Utc).AddTicks(9283), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 252, DateTimeKind.Utc).AddTicks(1010), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 253, DateTimeKind.Utc).AddTicks(2210), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 254, DateTimeKind.Utc).AddTicks(572), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 271, DateTimeKind.Utc).AddTicks(1035), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 272, DateTimeKind.Utc).AddTicks(4029), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 273, DateTimeKind.Utc).AddTicks(4985), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 265, DateTimeKind.Utc).AddTicks(301), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 275, DateTimeKind.Utc).AddTicks(8563), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 277, DateTimeKind.Utc).AddTicks(1382), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 278, DateTimeKind.Utc).AddTicks(3729), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 279, DateTimeKind.Utc).AddTicks(7313), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 280, DateTimeKind.Utc).AddTicks(4314), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 282, DateTimeKind.Utc).AddTicks(6060), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 302, DateTimeKind.Utc).AddTicks(4102), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 303, DateTimeKind.Utc).AddTicks(3386), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "Six", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 98, DateTimeKind.Utc).AddTicks(3285), "", false, false, "Six", "", "22222222-2222-2222-2222-222222222222", null, null, null, "6" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "Seven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 98, DateTimeKind.Utc).AddTicks(7048), "", false, false, "Seven", "", "22222222-2222-2222-2222-222222222222", null, null, null, "7" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "Eight", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 99, DateTimeKind.Utc).AddTicks(823), "", false, false, "Eight", "", "22222222-2222-2222-2222-222222222222", null, null, null, "8" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "Eleven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 100, DateTimeKind.Utc).AddTicks(2565), "", false, false, "Eleven", "", "22222222-2222-2222-2222-222222222222", null, null, null, "11" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "Ten", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 99, DateTimeKind.Utc).AddTicks(8801), "", false, false, "Ten", "", "22222222-2222-2222-2222-222222222222", null, null, null, "10" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "Fourteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 101, DateTimeKind.Utc).AddTicks(6424), "", false, false, "Fourteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "14" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "Fifteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 102, DateTimeKind.Utc).AddTicks(299), "", false, false, "Fifteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "15" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "Five", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 97, DateTimeKind.Utc).AddTicks(9259), "", false, false, "Five", "", "22222222-2222-2222-2222-222222222222", null, null, null, "5" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "Nine", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 99, DateTimeKind.Utc).AddTicks(4938), "", false, false, "Nine", "", "22222222-2222-2222-2222-222222222222", null, null, null, "9" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "Four", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 97, DateTimeKind.Utc).AddTicks(5217), "", false, false, "Four", "", "22222222-2222-2222-2222-222222222222", null, null, null, "4" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "Twelve", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 100, DateTimeKind.Utc).AddTicks(8026), "", false, false, "Twelve", "", "22222222-2222-2222-2222-222222222222", null, null, null, "12" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "Two", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 96, DateTimeKind.Utc).AddTicks(7580), "", false, false, "Two", "", "22222222-2222-2222-2222-222222222222", null, null, null, "2" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "One", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 96, DateTimeKind.Utc).AddTicks(3112), "", false, false, "One", "", "22222222-2222-2222-2222-222222222222", null, null, null, "1" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "Thirteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 101, DateTimeKind.Utc).AddTicks(2402), "", false, false, "Thirteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "13" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "Three", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 97, DateTimeKind.Utc).AddTicks(1419), "", false, false, "Three", "", "22222222-2222-2222-2222-222222222222", null, null, null, "3" });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 505, "ASSBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 88, DateTimeKind.Utc).AddTicks(3365), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 504, "STRBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 87, DateTimeKind.Utc).AddTicks(9385), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 87, DateTimeKind.Utc).AddTicks(5605), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 87, DateTimeKind.Utc).AddTicks(1714), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 86, DateTimeKind.Utc).AddTicks(3845), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 85, DateTimeKind.Utc).AddTicks(9492), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 86, DateTimeKind.Utc).AddTicks(7789), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 84, DateTimeKind.Utc).AddTicks(2558), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 102, DateTimeKind.Utc).AddTicks(6582), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 103, DateTimeKind.Utc).AddTicks(3003), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 103, DateTimeKind.Utc).AddTicks(7534), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 104, DateTimeKind.Utc).AddTicks(1351), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "LegalName", "Name", "ParentOrganizationId", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 131, DateTimeKind.Utc).AddTicks(5744), false, false, "AdionFA", "AdionFA", null, "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 4, "ChileanTrees", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 167, DateTimeKind.Utc).AddTicks(2750), "", false, false, "Chilean Trees", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "MacroTransformation", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 166, DateTimeKind.Utc).AddTicks(4479), "", false, false, "Macro Transformation", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "DataExtractor", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 165, DateTimeKind.Utc).AddTicks(4739), "", false, false, "Data Extractor", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "InitialConfiguration", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 158, DateTimeKind.Utc).AddTicks(3981), "", false, false, "Initial Configuration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 89, DateTimeKind.Utc).AddTicks(2022), false, false, "Culture", "22222222-2222-2222-2222-222222222222", null, null, null, "Culture" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 89, DateTimeKind.Utc).AddTicks(7825), false, false, "Theme", "22222222-2222-2222-2222-222222222222", null, null, null, "Theme" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 90, DateTimeKind.Utc).AddTicks(2076), false, false, "Color", "22222222-2222-2222-2222-222222222222", null, null, null, "Color" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 90, DateTimeKind.Utc).AddTicks(5999), false, false, "DefaultWorkspace", "22222222-2222-2222-2222-222222222222", null, null, null, "DefaultWorkspace" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 91, DateTimeKind.Utc).AddTicks(3906), false, false, "Port", "22222222-2222-2222-2222-222222222222", null, null, null, "Port" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 90, DateTimeKind.Utc).AddTicks(9925), false, false, "IPAddress", "22222222-2222-2222-2222-222222222222", null, null, null, "IPAddress" });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "EURUSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 91, DateTimeKind.Utc).AddTicks(5653), "Euro vs US Dollar", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 94, DateTimeKind.Utc).AddTicks(5635), "", false, false, "4 Hour", "", "22222222-2222-2222-2222-222222222222", null, null, null, "16388" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 94, DateTimeKind.Utc).AddTicks(1700), "", false, false, "1 Hour", "", "22222222-2222-2222-2222-222222222222", null, null, null, "16385" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 94, DateTimeKind.Utc).AddTicks(9386), "", false, false, "Daily", "", "22222222-2222-2222-2222-222222222222", null, null, null, "16408" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "W1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 95, DateTimeKind.Utc).AddTicks(3188), "", false, false, "Weekly", "", "22222222-2222-2222-2222-222222222222", null, null, null, "32769" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 93, DateTimeKind.Utc).AddTicks(7393), "", false, false, "30 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "30" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 93, DateTimeKind.Utc).AddTicks(3320), "", false, false, "15 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "15" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 92, DateTimeKind.Utc).AddTicks(9192), "", false, false, "5 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "5" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 92, DateTimeKind.Utc).AddTicks(4070), "", false, false, "1 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "1" });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "MN1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 95, DateTimeKind.Utc).AddTicks(7038), "", false, false, "Monthly", "", "22222222-2222-2222-2222-222222222222", null, null, null, "49153" });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "AsynchronousMode", "AutoAdjustConfig", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencySpreadId", "DepthWeka", "Description", "EndDate", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxAdjustConfig", "MaxPercentCorrelation", "MaxRatioTree", "MaximumSeed", "MinAdjustDepthWeka", "MinAdjustMaxRatioTree", "MinAdjustMinPercentSuccessIS", "MinAdjustMinPercentSuccessOS", "MinAdjustMinTransactionCountIS", "MinAdjustMinTransactionCountOS", "MinAdjustNTotalTree", "MinAdjustProgressiveness", "MinAdjustVariationTransaction", "MinAssemblyPercent", "MinPercentSuccessIS", "MinPercentSuccessOS", "MinTransactionCountIS", "MinTransactionCountOS", "MinimalSeed", "NTotalTree", "Progressiveness", "StartDate", "SymbolId", "TenantId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalAssemblyIterations", "TotalDecimalWeka", "TotalInstanceWeka", "TransactionTarget", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Variation", "VariationTransaction", "WinningStrategyTotalDOWN", "WinningStrategyTotalUP", "WithoutSchedule" },
                values: new object[] { 1, false, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 146, DateTimeKind.Utc).AddTicks(8269), 5, 10, "Default Configuration", null, null, null, false, false, false, 5, 2m, 1.5m, 1000000, 5, 1m, 50m, 50m, 360, 180, 150m, 2m, 4m, 5m, 55m, 55m, 600, 360, 100, 300m, 2m, new DateTime(2023, 3, 20, 23, 14, 7, 146, DateTimeKind.Utc).AddTicks(6086), 1, "22222222-2222-2222-2222-222222222222", 5, null, null, 1, 8, 1, 600, null, null, null, 50, 4m, 6, 6, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2719), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2706), "22222222-2222-2222-2222-222222222222", 82800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2749), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2747), "22222222-2222-2222-2222-222222222222", 64800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2758), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 3, 20, 23, 14, 7, 154, DateTimeKind.Utc).AddTicks(2756), "22222222-2222-2222-2222-222222222222", 32400, null, null, null });

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
                name: "IX_ExpertAdvisor_PUSHPort",
                table: "ExpertAdvisor",
                column: "PUSHPort",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertAdvisor_REPPort",
                table: "ExpertAdvisor",
                column: "REPPort",
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
                name: "IX_HistoricalDataDetail_HistoricalDataId",
                table: "HistoricalDataDetail",
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
                name: "HistoricalDataDetail");

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
