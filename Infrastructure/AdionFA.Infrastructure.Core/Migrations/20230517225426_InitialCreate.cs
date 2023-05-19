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
                        principalColumn: "OrganizationId");
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
                    { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 215, DateTimeKind.Utc).AddTicks(4864), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 215, DateTimeKind.Utc).AddTicks(8970), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 216, DateTimeKind.Utc).AddTicks(3082), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 216, DateTimeKind.Utc).AddTicks(7195), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 217, DateTimeKind.Utc).AddTicks(1292), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 217, DateTimeKind.Utc).AddTicks(5382), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 217, DateTimeKind.Utc).AddTicks(9432), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 218, DateTimeKind.Utc).AddTicks(3487), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 218, DateTimeKind.Utc).AddTicks(7525), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 219, DateTimeKind.Utc).AddTicks(1579), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 219, DateTimeKind.Utc).AddTicks(5625), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 219, DateTimeKind.Utc).AddTicks(9732), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 220, DateTimeKind.Utc).AddTicks(3889), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 220, DateTimeKind.Utc).AddTicks(7949), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 221, DateTimeKind.Utc).AddTicks(2046), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 221, DateTimeKind.Utc).AddTicks(6116), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 222, DateTimeKind.Utc).AddTicks(222), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 222, DateTimeKind.Utc).AddTicks(4300), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 222, DateTimeKind.Utc).AddTicks(8397), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 223, DateTimeKind.Utc).AddTicks(2483), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 223, DateTimeKind.Utc).AddTicks(6571), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 224, DateTimeKind.Utc).AddTicks(725), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 224, DateTimeKind.Utc).AddTicks(4823), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 224, DateTimeKind.Utc).AddTicks(8965), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 225, DateTimeKind.Utc).AddTicks(3074), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 225, DateTimeKind.Utc).AddTicks(7216), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 226, DateTimeKind.Utc).AddTicks(1351), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 226, DateTimeKind.Utc).AddTicks(5508), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 226, DateTimeKind.Utc).AddTicks(9658), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 227, DateTimeKind.Utc).AddTicks(3882), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 227, DateTimeKind.Utc).AddTicks(8082), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 228, DateTimeKind.Utc).AddTicks(2249), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 228, DateTimeKind.Utc).AddTicks(6474), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 229, DateTimeKind.Utc).AddTicks(711), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 229, DateTimeKind.Utc).AddTicks(4918), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 229, DateTimeKind.Utc).AddTicks(9184), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 230, DateTimeKind.Utc).AddTicks(3395), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 230, DateTimeKind.Utc).AddTicks(8231), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 231, DateTimeKind.Utc).AddTicks(2315), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 231, DateTimeKind.Utc).AddTicks(6286), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 232, DateTimeKind.Utc).AddTicks(259), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 232, DateTimeKind.Utc).AddTicks(4276), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 232, DateTimeKind.Utc).AddTicks(8232), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 233, DateTimeKind.Utc).AddTicks(2241), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 233, DateTimeKind.Utc).AddTicks(6276), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 234, DateTimeKind.Utc).AddTicks(286), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 234, DateTimeKind.Utc).AddTicks(4249), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 234, DateTimeKind.Utc).AddTicks(8264), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 235, DateTimeKind.Utc).AddTicks(2308), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 235, DateTimeKind.Utc).AddTicks(6315), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 236, DateTimeKind.Utc).AddTicks(306), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 236, DateTimeKind.Utc).AddTicks(4317), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 237, DateTimeKind.Utc).AddTicks(5539), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 237, DateTimeKind.Utc).AddTicks(9788), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 238, DateTimeKind.Utc).AddTicks(3799), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 238, DateTimeKind.Utc).AddTicks(7809), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 239, DateTimeKind.Utc).AddTicks(1817), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 239, DateTimeKind.Utc).AddTicks(5841), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 239, DateTimeKind.Utc).AddTicks(9868), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 240, DateTimeKind.Utc).AddTicks(3867), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 240, DateTimeKind.Utc).AddTicks(7869), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 241, DateTimeKind.Utc).AddTicks(1850), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 241, DateTimeKind.Utc).AddTicks(5863), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 241, DateTimeKind.Utc).AddTicks(9886), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 242, DateTimeKind.Utc).AddTicks(3882), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 242, DateTimeKind.Utc).AddTicks(7917), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 243, DateTimeKind.Utc).AddTicks(1947), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 243, DateTimeKind.Utc).AddTicks(5913), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 243, DateTimeKind.Utc).AddTicks(9881), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 244, DateTimeKind.Utc).AddTicks(3908), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 244, DateTimeKind.Utc).AddTicks(7889), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 245, DateTimeKind.Utc).AddTicks(1907), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 245, DateTimeKind.Utc).AddTicks(5901), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 245, DateTimeKind.Utc).AddTicks(9892), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 250, DateTimeKind.Utc).AddTicks(8937), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 251, DateTimeKind.Utc).AddTicks(7157), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 252, DateTimeKind.Utc).AddTicks(3821), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 252, DateTimeKind.Utc).AddTicks(9679), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 253, DateTimeKind.Utc).AddTicks(5878), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 254, DateTimeKind.Utc).AddTicks(2256), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 254, DateTimeKind.Utc).AddTicks(8424), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 255, DateTimeKind.Utc).AddTicks(4747), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 256, DateTimeKind.Utc).AddTicks(649), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 256, DateTimeKind.Utc).AddTicks(6433), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 257, DateTimeKind.Utc).AddTicks(2797), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 257, DateTimeKind.Utc).AddTicks(9075), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 258, DateTimeKind.Utc).AddTicks(5004), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 259, DateTimeKind.Utc).AddTicks(941), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 259, DateTimeKind.Utc).AddTicks(6986), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 260, DateTimeKind.Utc).AddTicks(3027), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 260, DateTimeKind.Utc).AddTicks(9367), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 261, DateTimeKind.Utc).AddTicks(6913), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 262, DateTimeKind.Utc).AddTicks(3185), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 262, DateTimeKind.Utc).AddTicks(8539), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 263, DateTimeKind.Utc).AddTicks(3726), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 263, DateTimeKind.Utc).AddTicks(8976), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 264, DateTimeKind.Utc).AddTicks(4075), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 264, DateTimeKind.Utc).AddTicks(9179), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 265, DateTimeKind.Utc).AddTicks(4367), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 265, DateTimeKind.Utc).AddTicks(9422), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 266, DateTimeKind.Utc).AddTicks(4458), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 266, DateTimeKind.Utc).AddTicks(9497), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 267, DateTimeKind.Utc).AddTicks(4548), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 267, DateTimeKind.Utc).AddTicks(9840), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 268, DateTimeKind.Utc).AddTicks(5289), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 269, DateTimeKind.Utc).AddTicks(277), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 269, DateTimeKind.Utc).AddTicks(5304), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 270, DateTimeKind.Utc).AddTicks(345), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 270, DateTimeKind.Utc).AddTicks(5352), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 271, DateTimeKind.Utc).AddTicks(694), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 271, DateTimeKind.Utc).AddTicks(5994), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 272, DateTimeKind.Utc).AddTicks(1261), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 272, DateTimeKind.Utc).AddTicks(5774), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 273, DateTimeKind.Utc).AddTicks(113), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 273, DateTimeKind.Utc).AddTicks(4439), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 273, DateTimeKind.Utc).AddTicks(8786), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 274, DateTimeKind.Utc).AddTicks(3104), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 274, DateTimeKind.Utc).AddTicks(7413), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 275, DateTimeKind.Utc).AddTicks(1769), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 275, DateTimeKind.Utc).AddTicks(6096), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 276, DateTimeKind.Utc).AddTicks(315), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 276, DateTimeKind.Utc).AddTicks(4554), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 276, DateTimeKind.Utc).AddTicks(8808), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 277, DateTimeKind.Utc).AddTicks(3031), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 277, DateTimeKind.Utc).AddTicks(8629), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 278, DateTimeKind.Utc).AddTicks(3753), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 278, DateTimeKind.Utc).AddTicks(8115), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 279, DateTimeKind.Utc).AddTicks(2407), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 279, DateTimeKind.Utc).AddTicks(6761), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 280, DateTimeKind.Utc).AddTicks(1106), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 280, DateTimeKind.Utc).AddTicks(5429), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 280, DateTimeKind.Utc).AddTicks(9746), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 281, DateTimeKind.Utc).AddTicks(4076), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 281, DateTimeKind.Utc).AddTicks(8425), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 282, DateTimeKind.Utc).AddTicks(2794), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 282, DateTimeKind.Utc).AddTicks(7132), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 283, DateTimeKind.Utc).AddTicks(1454), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 283, DateTimeKind.Utc).AddTicks(5788), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 284, DateTimeKind.Utc).AddTicks(105), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 284, DateTimeKind.Utc).AddTicks(4339), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 284, DateTimeKind.Utc).AddTicks(7769), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 285, DateTimeKind.Utc).AddTicks(1098), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 285, DateTimeKind.Utc).AddTicks(4434), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 285, DateTimeKind.Utc).AddTicks(7938), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 286, DateTimeKind.Utc).AddTicks(1335), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 286, DateTimeKind.Utc).AddTicks(4676), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 286, DateTimeKind.Utc).AddTicks(8161), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 287, DateTimeKind.Utc).AddTicks(1543), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 287, DateTimeKind.Utc).AddTicks(4975), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 287, DateTimeKind.Utc).AddTicks(8405), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 288, DateTimeKind.Utc).AddTicks(1829), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 288, DateTimeKind.Utc).AddTicks(5234), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 288, DateTimeKind.Utc).AddTicks(8698), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 289, DateTimeKind.Utc).AddTicks(2071), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 289, DateTimeKind.Utc).AddTicks(5431), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 289, DateTimeKind.Utc).AddTicks(8771), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 290, DateTimeKind.Utc).AddTicks(2117), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 290, DateTimeKind.Utc).AddTicks(5482), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 290, DateTimeKind.Utc).AddTicks(8861), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 291, DateTimeKind.Utc).AddTicks(2224), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 291, DateTimeKind.Utc).AddTicks(5559), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 291, DateTimeKind.Utc).AddTicks(8919), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 292, DateTimeKind.Utc).AddTicks(2306), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 292, DateTimeKind.Utc).AddTicks(5649), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 292, DateTimeKind.Utc).AddTicks(8983), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 293, DateTimeKind.Utc).AddTicks(2509), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 293, DateTimeKind.Utc).AddTicks(6176), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 293, DateTimeKind.Utc).AddTicks(9576), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 294, DateTimeKind.Utc).AddTicks(2967), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 294, DateTimeKind.Utc).AddTicks(6339), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 294, DateTimeKind.Utc).AddTicks(9749), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 295, DateTimeKind.Utc).AddTicks(3127), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 295, DateTimeKind.Utc).AddTicks(6522), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 295, DateTimeKind.Utc).AddTicks(9892), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 296, DateTimeKind.Utc).AddTicks(3336), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 296, DateTimeKind.Utc).AddTicks(6689), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 297, DateTimeKind.Utc).AddTicks(174), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 297, DateTimeKind.Utc).AddTicks(3523), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 297, DateTimeKind.Utc).AddTicks(6983), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 298, DateTimeKind.Utc).AddTicks(369), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 298, DateTimeKind.Utc).AddTicks(3771), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 298, DateTimeKind.Utc).AddTicks(7140), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 299, DateTimeKind.Utc).AddTicks(506), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 299, DateTimeKind.Utc).AddTicks(3928), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 299, DateTimeKind.Utc).AddTicks(7305), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 300, DateTimeKind.Utc).AddTicks(676), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 300, DateTimeKind.Utc).AddTicks(4072), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 300, DateTimeKind.Utc).AddTicks(7467), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 301, DateTimeKind.Utc).AddTicks(841), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 301, DateTimeKind.Utc).AddTicks(4208), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 301, DateTimeKind.Utc).AddTicks(7596), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 302, DateTimeKind.Utc).AddTicks(972), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 302, DateTimeKind.Utc).AddTicks(4346), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 302, DateTimeKind.Utc).AddTicks(7723), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 303, DateTimeKind.Utc).AddTicks(1094), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 303, DateTimeKind.Utc).AddTicks(4496), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 303, DateTimeKind.Utc).AddTicks(7873), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 304, DateTimeKind.Utc).AddTicks(1273), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 304, DateTimeKind.Utc).AddTicks(4650), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 304, DateTimeKind.Utc).AddTicks(8033), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 305, DateTimeKind.Utc).AddTicks(1390), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 305, DateTimeKind.Utc).AddTicks(4766), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 305, DateTimeKind.Utc).AddTicks(8130), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "One", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 198, DateTimeKind.Utc).AddTicks(9310), "", false, false, "One", "22222222-2222-2222-2222-222222222222", null, null, null, "1" },
                    { 2, "Two", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 199, DateTimeKind.Utc).AddTicks(2409), "", false, false, "Two", "22222222-2222-2222-2222-222222222222", null, null, null, "2" },
                    { 3, "Three", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 199, DateTimeKind.Utc).AddTicks(8736), "", false, false, "Three", "22222222-2222-2222-2222-222222222222", null, null, null, "3" },
                    { 4, "Four", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 200, DateTimeKind.Utc).AddTicks(4722), "", false, false, "Four", "22222222-2222-2222-2222-222222222222", null, null, null, "4" },
                    { 5, "Five", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 200, DateTimeKind.Utc).AddTicks(9284), "", false, false, "Five", "22222222-2222-2222-2222-222222222222", null, null, null, "5" },
                    { 6, "Six", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 201, DateTimeKind.Utc).AddTicks(3180), "", false, false, "Six", "22222222-2222-2222-2222-222222222222", null, null, null, "6" },
                    { 7, "Seven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 201, DateTimeKind.Utc).AddTicks(6970), "", false, false, "Seven", "22222222-2222-2222-2222-222222222222", null, null, null, "7" },
                    { 8, "Eight", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 202, DateTimeKind.Utc).AddTicks(715), "", false, false, "Eight", "22222222-2222-2222-2222-222222222222", null, null, null, "8" },
                    { 9, "Nine", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 202, DateTimeKind.Utc).AddTicks(4465), "", false, false, "Nine", "22222222-2222-2222-2222-222222222222", null, null, null, "9" },
                    { 10, "Ten", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 202, DateTimeKind.Utc).AddTicks(8231), "", false, false, "Ten", "22222222-2222-2222-2222-222222222222", null, null, null, "10" },
                    { 11, "Eleven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 203, DateTimeKind.Utc).AddTicks(1975), "", false, false, "Eleven", "22222222-2222-2222-2222-222222222222", null, null, null, "11" },
                    { 12, "Twelve", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 203, DateTimeKind.Utc).AddTicks(5778), "", false, false, "Twelve", "22222222-2222-2222-2222-222222222222", null, null, null, "12" },
                    { 13, "Thirteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 203, DateTimeKind.Utc).AddTicks(9530), "", false, false, "Thirteen", "22222222-2222-2222-2222-222222222222", null, null, null, "13" },
                    { 14, "Fourteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 204, DateTimeKind.Utc).AddTicks(3281), "", false, false, "Fourteen", "22222222-2222-2222-2222-222222222222", null, null, null, "14" },
                    { 15, "Fifteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 204, DateTimeKind.Utc).AddTicks(7069), "", false, false, "Fifteen", "22222222-2222-2222-2222-222222222222", null, null, null, "15" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 194, DateTimeKind.Utc).AddTicks(5555), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 194, DateTimeKind.Utc).AddTicks(8855), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 195, DateTimeKind.Utc).AddTicks(1988), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 195, DateTimeKind.Utc).AddTicks(5098), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 195, DateTimeKind.Utc).AddTicks(8191), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 196, DateTimeKind.Utc).AddTicks(1298), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 504, "STRBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 196, DateTimeKind.Utc).AddTicks(4389), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 505, "ASSBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 196, DateTimeKind.Utc).AddTicks(7455), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 208, DateTimeKind.Utc).AddTicks(4683), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 208, DateTimeKind.Utc).AddTicks(8509), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 209, DateTimeKind.Utc).AddTicks(3579), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 209, DateTimeKind.Utc).AddTicks(8031), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "LegalName", "Name", "ParentOrganizationId", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2510), false, false, "AdionFA", "AdionFA", null, "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "InitialConfiguration", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(7544), "", false, false, "Initial Configuration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 2, "DataExtractor", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 214, DateTimeKind.Utc).AddTicks(1414), "", false, false, "Data Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 3, "MacroTransformation", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 214, DateTimeKind.Utc).AddTicks(5261), "", false, false, "Macro Transformation", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 4, "ChileanTrees", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 214, DateTimeKind.Utc).AddTicks(9067), "", false, false, "Chilean Trees", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 197, DateTimeKind.Utc).AddTicks(602), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "eng" },
                    { 2, "Theme", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 197, DateTimeKind.Utc).AddTicks(3710), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Light" },
                    { 3, "Color", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 197, DateTimeKind.Utc).AddTicks(6805), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 197, DateTimeKind.Utc).AddTicks(9860), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "Host", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 198, DateTimeKind.Utc).AddTicks(2989), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "192.168.50.137" },
                    { 6, "Port", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 198, DateTimeKind.Utc).AddTicks(6060), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 198, DateTimeKind.Utc).AddTicks(6096), "Euro vs US Dollar", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 205, DateTimeKind.Utc).AddTicks(940), "", false, false, "1 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "1" },
                    { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 205, DateTimeKind.Utc).AddTicks(4719), "", false, false, "5 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "5" },
                    { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 205, DateTimeKind.Utc).AddTicks(8450), "", false, false, "15 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "15" },
                    { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 206, DateTimeKind.Utc).AddTicks(2165), "", false, false, "30 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "30" },
                    { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 206, DateTimeKind.Utc).AddTicks(5935), "", false, false, "1 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16385" },
                    { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 206, DateTimeKind.Utc).AddTicks(9661), "", false, false, "4 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16388" },
                    { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 207, DateTimeKind.Utc).AddTicks(3391), "", false, false, "Daily", "22222222-2222-2222-2222-222222222222", null, null, null, "16408" },
                    { 8, "W1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 207, DateTimeKind.Utc).AddTicks(7116), "", false, false, "Weekly", "22222222-2222-2222-2222-222222222222", null, null, null, "32769" },
                    { 9, "MN1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 208, DateTimeKind.Utc).AddTicks(889), "", false, false, "Monthly", "22222222-2222-2222-2222-222222222222", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "AsynchronousMode", "AutoAdjustConfig", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencySpreadId", "DepthWeka", "Description", "EndDate", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxAdjustConfig", "MaxPercentCorrelation", "MaxRatioTree", "MaximumSeed", "MinAdjustDepthWeka", "MinAdjustMaxRatioTree", "MinAdjustMinPercentSuccessIS", "MinAdjustMinPercentSuccessOS", "MinAdjustMinTransactionCountIS", "MinAdjustMinTransactionCountOS", "MinAdjustNTotalTree", "MinAdjustProgressiveness", "MinAdjustVariationTransaction", "MinAssemblyPercent", "MinPercentSuccessIS", "MinPercentSuccessOS", "MinTransactionCountIS", "MinTransactionCountOS", "MinimalSeed", "NTotalTree", "Progressiveness", "StartDate", "SymbolId", "TenantId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalAssemblyIterations", "TotalDecimalWeka", "TotalInstanceWeka", "TransactionTarget", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Variation", "VariationTransaction", "WinningStrategyTotalDOWN", "WinningStrategyTotalUP", "WithoutSchedule" },
                values: new object[] { 1, false, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2590), 5, 6, "Default Configuration", null, null, null, false, false, false, 5, 2m, 1.5m, 1000000, 5, 1m, 50m, 50m, 360, 180, 150m, 2m, 4m, 5m, 55m, 55m, 300, 100, 100, 300m, 2m, new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2587), 1, "22222222-2222-2222-2222-222222222222", 5, null, null, 1, 5, 1, 600, null, null, null, 50, 4m, 6, 6, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2624), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2623), "22222222-2222-2222-2222-222222222222", 82800, null, null, null },
                    { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2628), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2627), "22222222-2222-2222-2222-222222222222", 64800, null, null, null },
                    { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2632), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 5, 17, 22, 54, 26, 213, DateTimeKind.Utc).AddTicks(2630), "22222222-2222-2222-2222-222222222222", 32400, null, null, null }
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
