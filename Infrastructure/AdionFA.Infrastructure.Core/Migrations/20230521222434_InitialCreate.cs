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
                    { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 839, DateTimeKind.Utc).AddTicks(7201), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 840, DateTimeKind.Utc).AddTicks(2016), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 840, DateTimeKind.Utc).AddTicks(6457), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 841, DateTimeKind.Utc).AddTicks(849), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 841, DateTimeKind.Utc).AddTicks(5441), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 841, DateTimeKind.Utc).AddTicks(9948), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 843, DateTimeKind.Utc).AddTicks(3357), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 844, DateTimeKind.Utc).AddTicks(6656), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 845, DateTimeKind.Utc).AddTicks(8634), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 846, DateTimeKind.Utc).AddTicks(4932), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 847, DateTimeKind.Utc).AddTicks(512), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 847, DateTimeKind.Utc).AddTicks(5895), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 848, DateTimeKind.Utc).AddTicks(1031), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 848, DateTimeKind.Utc).AddTicks(5116), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 848, DateTimeKind.Utc).AddTicks(9140), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 849, DateTimeKind.Utc).AddTicks(2907), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 849, DateTimeKind.Utc).AddTicks(6829), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 850, DateTimeKind.Utc).AddTicks(4181), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 851, DateTimeKind.Utc).AddTicks(314), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 851, DateTimeKind.Utc).AddTicks(7294), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 852, DateTimeKind.Utc).AddTicks(3521), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 852, DateTimeKind.Utc).AddTicks(8342), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 853, DateTimeKind.Utc).AddTicks(2508), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 853, DateTimeKind.Utc).AddTicks(6547), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 854, DateTimeKind.Utc).AddTicks(635), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 854, DateTimeKind.Utc).AddTicks(5399), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 854, DateTimeKind.Utc).AddTicks(9418), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 855, DateTimeKind.Utc).AddTicks(3228), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 855, DateTimeKind.Utc).AddTicks(6996), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 856, DateTimeKind.Utc).AddTicks(779), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 856, DateTimeKind.Utc).AddTicks(4609), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 856, DateTimeKind.Utc).AddTicks(8436), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 857, DateTimeKind.Utc).AddTicks(2263), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 857, DateTimeKind.Utc).AddTicks(6100), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 857, DateTimeKind.Utc).AddTicks(9874), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 858, DateTimeKind.Utc).AddTicks(4375), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 859, DateTimeKind.Utc).AddTicks(3119), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 860, DateTimeKind.Utc).AddTicks(6745), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 861, DateTimeKind.Utc).AddTicks(8484), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 862, DateTimeKind.Utc).AddTicks(6248), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 863, DateTimeKind.Utc).AddTicks(3658), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 864, DateTimeKind.Utc).AddTicks(76), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 864, DateTimeKind.Utc).AddTicks(7980), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 865, DateTimeKind.Utc).AddTicks(4916), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 866, DateTimeKind.Utc).AddTicks(1837), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 866, DateTimeKind.Utc).AddTicks(7708), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 867, DateTimeKind.Utc).AddTicks(3045), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 867, DateTimeKind.Utc).AddTicks(8525), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 868, DateTimeKind.Utc).AddTicks(4974), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 869, DateTimeKind.Utc).AddTicks(286), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 869, DateTimeKind.Utc).AddTicks(6014), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 870, DateTimeKind.Utc).AddTicks(1489), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 870, DateTimeKind.Utc).AddTicks(6666), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 871, DateTimeKind.Utc).AddTicks(2140), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 871, DateTimeKind.Utc).AddTicks(7754), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 872, DateTimeKind.Utc).AddTicks(2637), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 872, DateTimeKind.Utc).AddTicks(7594), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 873, DateTimeKind.Utc).AddTicks(3737), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 873, DateTimeKind.Utc).AddTicks(9375), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 874, DateTimeKind.Utc).AddTicks(5225), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 875, DateTimeKind.Utc).AddTicks(3938), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 876, DateTimeKind.Utc).AddTicks(6406), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 877, DateTimeKind.Utc).AddTicks(5434), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 878, DateTimeKind.Utc).AddTicks(4232), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 878, DateTimeKind.Utc).AddTicks(9246), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 879, DateTimeKind.Utc).AddTicks(3829), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 879, DateTimeKind.Utc).AddTicks(8492), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 880, DateTimeKind.Utc).AddTicks(3661), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 880, DateTimeKind.Utc).AddTicks(9135), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 881, DateTimeKind.Utc).AddTicks(4599), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 882, DateTimeKind.Utc).AddTicks(4392), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 883, DateTimeKind.Utc).AddTicks(1012), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 883, DateTimeKind.Utc).AddTicks(5449), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 883, DateTimeKind.Utc).AddTicks(9418), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 884, DateTimeKind.Utc).AddTicks(3310), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 884, DateTimeKind.Utc).AddTicks(7268), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 885, DateTimeKind.Utc).AddTicks(1224), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 885, DateTimeKind.Utc).AddTicks(5198), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 885, DateTimeKind.Utc).AddTicks(9399), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 886, DateTimeKind.Utc).AddTicks(3195), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 886, DateTimeKind.Utc).AddTicks(6979), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 887, DateTimeKind.Utc).AddTicks(1233), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 887, DateTimeKind.Utc).AddTicks(5018), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 887, DateTimeKind.Utc).AddTicks(8678), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 888, DateTimeKind.Utc).AddTicks(2284), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 888, DateTimeKind.Utc).AddTicks(6032), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 888, DateTimeKind.Utc).AddTicks(9679), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 889, DateTimeKind.Utc).AddTicks(3423), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 889, DateTimeKind.Utc).AddTicks(7095), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 890, DateTimeKind.Utc).AddTicks(888), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 890, DateTimeKind.Utc).AddTicks(4354), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 890, DateTimeKind.Utc).AddTicks(7858), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 891, DateTimeKind.Utc).AddTicks(1498), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 891, DateTimeKind.Utc).AddTicks(4965), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 891, DateTimeKind.Utc).AddTicks(8536), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 892, DateTimeKind.Utc).AddTicks(3601), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 893, DateTimeKind.Utc).AddTicks(797), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 894, DateTimeKind.Utc).AddTicks(11), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 895, DateTimeKind.Utc).AddTicks(419), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 895, DateTimeKind.Utc).AddTicks(6873), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 896, DateTimeKind.Utc).AddTicks(2741), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 896, DateTimeKind.Utc).AddTicks(7385), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 897, DateTimeKind.Utc).AddTicks(4566), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 898, DateTimeKind.Utc).AddTicks(3290), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 898, DateTimeKind.Utc).AddTicks(9013), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 899, DateTimeKind.Utc).AddTicks(3512), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 899, DateTimeKind.Utc).AddTicks(8191), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 900, DateTimeKind.Utc).AddTicks(3032), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 900, DateTimeKind.Utc).AddTicks(7883), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 901, DateTimeKind.Utc).AddTicks(2922), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 901, DateTimeKind.Utc).AddTicks(9348), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 902, DateTimeKind.Utc).AddTicks(4439), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 902, DateTimeKind.Utc).AddTicks(9349), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 903, DateTimeKind.Utc).AddTicks(4222), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 903, DateTimeKind.Utc).AddTicks(8963), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 904, DateTimeKind.Utc).AddTicks(3406), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 904, DateTimeKind.Utc).AddTicks(8070), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 905, DateTimeKind.Utc).AddTicks(2981), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 905, DateTimeKind.Utc).AddTicks(7602), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 906, DateTimeKind.Utc).AddTicks(2706), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 906, DateTimeKind.Utc).AddTicks(8277), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 907, DateTimeKind.Utc).AddTicks(3449), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 907, DateTimeKind.Utc).AddTicks(8801), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 908, DateTimeKind.Utc).AddTicks(3392), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 909, DateTimeKind.Utc).AddTicks(51), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 909, DateTimeKind.Utc).AddTicks(7229), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 910, DateTimeKind.Utc).AddTicks(4146), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 911, DateTimeKind.Utc).AddTicks(1904), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 911, DateTimeKind.Utc).AddTicks(7377), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 912, DateTimeKind.Utc).AddTicks(1522), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 912, DateTimeKind.Utc).AddTicks(5611), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 913, DateTimeKind.Utc).AddTicks(1802), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 913, DateTimeKind.Utc).AddTicks(8302), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 914, DateTimeKind.Utc).AddTicks(5221), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 915, DateTimeKind.Utc).AddTicks(2166), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 915, DateTimeKind.Utc).AddTicks(6722), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 916, DateTimeKind.Utc).AddTicks(976), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 916, DateTimeKind.Utc).AddTicks(4929), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 916, DateTimeKind.Utc).AddTicks(9200), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 917, DateTimeKind.Utc).AddTicks(3758), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 917, DateTimeKind.Utc).AddTicks(8515), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 918, DateTimeKind.Utc).AddTicks(3611), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 918, DateTimeKind.Utc).AddTicks(8195), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 919, DateTimeKind.Utc).AddTicks(2541), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 919, DateTimeKind.Utc).AddTicks(7242), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 920, DateTimeKind.Utc).AddTicks(1398), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 923, DateTimeKind.Utc).AddTicks(2047), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 923, DateTimeKind.Utc).AddTicks(7408), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 924, DateTimeKind.Utc).AddTicks(3212), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 924, DateTimeKind.Utc).AddTicks(8509), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 925, DateTimeKind.Utc).AddTicks(3803), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 928, DateTimeKind.Utc).AddTicks(678), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 929, DateTimeKind.Utc).AddTicks(973), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 929, DateTimeKind.Utc).AddTicks(8319), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 930, DateTimeKind.Utc).AddTicks(3688), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 930, DateTimeKind.Utc).AddTicks(8243), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 931, DateTimeKind.Utc).AddTicks(3010), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 931, DateTimeKind.Utc).AddTicks(7328), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 932, DateTimeKind.Utc).AddTicks(2080), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 932, DateTimeKind.Utc).AddTicks(6085), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 933, DateTimeKind.Utc).AddTicks(80), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 933, DateTimeKind.Utc).AddTicks(4094), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 933, DateTimeKind.Utc).AddTicks(7949), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 934, DateTimeKind.Utc).AddTicks(2720), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 934, DateTimeKind.Utc).AddTicks(7323), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 935, DateTimeKind.Utc).AddTicks(1577), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 935, DateTimeKind.Utc).AddTicks(5568), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 935, DateTimeKind.Utc).AddTicks(9447), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 936, DateTimeKind.Utc).AddTicks(3305), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 936, DateTimeKind.Utc).AddTicks(7035), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 937, DateTimeKind.Utc).AddTicks(835), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 937, DateTimeKind.Utc).AddTicks(4556), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 937, DateTimeKind.Utc).AddTicks(8374), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 938, DateTimeKind.Utc).AddTicks(2163), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 938, DateTimeKind.Utc).AddTicks(5889), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 938, DateTimeKind.Utc).AddTicks(9632), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 939, DateTimeKind.Utc).AddTicks(3580), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 939, DateTimeKind.Utc).AddTicks(7017), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 940, DateTimeKind.Utc).AddTicks(446), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 940, DateTimeKind.Utc).AddTicks(3884), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 940, DateTimeKind.Utc).AddTicks(7238), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 941, DateTimeKind.Utc).AddTicks(682), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 941, DateTimeKind.Utc).AddTicks(4285), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 941, DateTimeKind.Utc).AddTicks(7822), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 942, DateTimeKind.Utc).AddTicks(2577), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 943, DateTimeKind.Utc).AddTicks(562), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 944, DateTimeKind.Utc).AddTicks(4550), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 945, DateTimeKind.Utc).AddTicks(5371), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 946, DateTimeKind.Utc).AddTicks(649), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 946, DateTimeKind.Utc).AddTicks(4752), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 946, DateTimeKind.Utc).AddTicks(8882), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 947, DateTimeKind.Utc).AddTicks(4021), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 947, DateTimeKind.Utc).AddTicks(8592), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 948, DateTimeKind.Utc).AddTicks(2724), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 948, DateTimeKind.Utc).AddTicks(6674), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 949, DateTimeKind.Utc).AddTicks(633), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 949, DateTimeKind.Utc).AddTicks(4494), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 949, DateTimeKind.Utc).AddTicks(8214), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 950, DateTimeKind.Utc).AddTicks(1912), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 950, DateTimeKind.Utc).AddTicks(5811), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 950, DateTimeKind.Utc).AddTicks(9523), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 951, DateTimeKind.Utc).AddTicks(3348), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 951, DateTimeKind.Utc).AddTicks(7124), "", false, false, "", "22222222-2222-2222-2222-222222222222", null, null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "One", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 806, DateTimeKind.Utc).AddTicks(8005), "", false, false, "One", "22222222-2222-2222-2222-222222222222", null, null, null, "1" },
                    { 2, "Two", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 807, DateTimeKind.Utc).AddTicks(1873), "", false, false, "Two", "22222222-2222-2222-2222-222222222222", null, null, null, "2" },
                    { 3, "Three", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 807, DateTimeKind.Utc).AddTicks(5886), "", false, false, "Three", "22222222-2222-2222-2222-222222222222", null, null, null, "3" },
                    { 4, "Four", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 807, DateTimeKind.Utc).AddTicks(9699), "", false, false, "Four", "22222222-2222-2222-2222-222222222222", null, null, null, "4" },
                    { 5, "Five", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 808, DateTimeKind.Utc).AddTicks(3795), "", false, false, "Five", "22222222-2222-2222-2222-222222222222", null, null, null, "5" },
                    { 6, "Six", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 808, DateTimeKind.Utc).AddTicks(8740), "", false, false, "Six", "22222222-2222-2222-2222-222222222222", null, null, null, "6" },
                    { 7, "Seven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 809, DateTimeKind.Utc).AddTicks(6029), "", false, false, "Seven", "22222222-2222-2222-2222-222222222222", null, null, null, "7" },
                    { 8, "Eight", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 810, DateTimeKind.Utc).AddTicks(8002), "", false, false, "Eight", "22222222-2222-2222-2222-222222222222", null, null, null, "8" },
                    { 9, "Nine", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 811, DateTimeKind.Utc).AddTicks(9621), "", false, false, "Nine", "22222222-2222-2222-2222-222222222222", null, null, null, "9" },
                    { 10, "Ten", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 812, DateTimeKind.Utc).AddTicks(7889), "", false, false, "Ten", "22222222-2222-2222-2222-222222222222", null, null, null, "10" },
                    { 11, "Eleven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 813, DateTimeKind.Utc).AddTicks(5582), "", false, false, "Eleven", "22222222-2222-2222-2222-222222222222", null, null, null, "11" },
                    { 12, "Twelve", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 814, DateTimeKind.Utc).AddTicks(9077), "", false, false, "Twelve", "22222222-2222-2222-2222-222222222222", null, null, null, "12" },
                    { 13, "Thirteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 815, DateTimeKind.Utc).AddTicks(5114), "", false, false, "Thirteen", "22222222-2222-2222-2222-222222222222", null, null, null, "13" },
                    { 14, "Fourteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 816, DateTimeKind.Utc).AddTicks(328), "", false, false, "Fourteen", "22222222-2222-2222-2222-222222222222", null, null, null, "14" },
                    { 15, "Fifteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 816, DateTimeKind.Utc).AddTicks(5530), "", false, false, "Fifteen", "22222222-2222-2222-2222-222222222222", null, null, null, "15" }
                });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 799, DateTimeKind.Utc).AddTicks(3688), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 800, DateTimeKind.Utc).AddTicks(853), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 800, DateTimeKind.Utc).AddTicks(5950), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 801, DateTimeKind.Utc).AddTicks(1235), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 801, DateTimeKind.Utc).AddTicks(6624), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 802, DateTimeKind.Utc).AddTicks(1778), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 504, "STRBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 802, DateTimeKind.Utc).AddTicks(9940), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 505, "ASSBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 803, DateTimeKind.Utc).AddTicks(5535), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 822, DateTimeKind.Utc).AddTicks(3151), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 822, DateTimeKind.Utc).AddTicks(8430), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 823, DateTimeKind.Utc).AddTicks(3535), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 823, DateTimeKind.Utc).AddTicks(8413), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "LegalName", "Name", "ParentOrganizationId", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9029), false, false, "AdionFA", "AdionFA", null, "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "InitialConfiguration", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 837, DateTimeKind.Utc).AddTicks(6404), "", false, false, "Initial Configuration", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 2, "DataExtractor", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 838, DateTimeKind.Utc).AddTicks(1232), "", false, false, "Data Extractor", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 3, "MacroTransformation", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 838, DateTimeKind.Utc).AddTicks(6132), "", false, false, "Macro Transformation", "22222222-2222-2222-2222-222222222222", null, null, null, null },
                    { 4, "ChileanTrees", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 839, DateTimeKind.Utc).AddTicks(859), "", false, false, "Chilean Trees", "22222222-2222-2222-2222-222222222222", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "Culture", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 804, DateTimeKind.Utc).AddTicks(1394), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "eng" },
                    { 2, "Theme", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 804, DateTimeKind.Utc).AddTicks(6552), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Light" },
                    { 3, "Color", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 805, DateTimeKind.Utc).AddTicks(1261), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "Orange" },
                    { 4, "DefaultWorkspace", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 805, DateTimeKind.Utc).AddTicks(5691), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "" },
                    { 5, "Host", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 805, DateTimeKind.Utc).AddTicks(9764), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "192.168.50.137" },
                    { 6, "Port", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 806, DateTimeKind.Utc).AddTicks(3861), null, false, false, null, "22222222-2222-2222-2222-222222222222", null, null, null, "5555" }
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "EURUSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 806, DateTimeKind.Utc).AddTicks(3910), "Euro vs US Dollar", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null, null });

            migrationBuilder.InsertData(
                table: "Timeframe",
                columns: new[] { "TimeframeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[,]
                {
                    { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 817, DateTimeKind.Utc).AddTicks(847), "", false, false, "1 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "1" },
                    { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 817, DateTimeKind.Utc).AddTicks(5769), "", false, false, "5 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "5" },
                    { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 818, DateTimeKind.Utc).AddTicks(5030), "", false, false, "15 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "15" },
                    { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 819, DateTimeKind.Utc).AddTicks(2114), "", false, false, "30 Minute", "22222222-2222-2222-2222-222222222222", null, null, null, "30" },
                    { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 819, DateTimeKind.Utc).AddTicks(8045), "", false, false, "1 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16385" },
                    { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 820, DateTimeKind.Utc).AddTicks(3702), "", false, false, "4 Hour", "22222222-2222-2222-2222-222222222222", null, null, null, "16388" },
                    { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 820, DateTimeKind.Utc).AddTicks(8831), "", false, false, "Daily", "22222222-2222-2222-2222-222222222222", null, null, null, "16408" },
                    { 8, "W1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 821, DateTimeKind.Utc).AddTicks(3311), "", false, false, "Weekly", "22222222-2222-2222-2222-222222222222", null, null, null, "32769" },
                    { 9, "MN1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 821, DateTimeKind.Utc).AddTicks(8191), "", false, false, "Monthly", "22222222-2222-2222-2222-222222222222", null, null, null, "49153" }
                });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "AutoAdjustConfig", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencySpreadId", "DepthWeka", "Description", "EndDate", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxAdjustConfig", "MaxPercentCorrelation", "MaxRatioTree", "MaximumSeed", "MinAdjustDepthWeka", "MinAdjustMaxRatioTree", "MinAdjustMinPercentSuccessIS", "MinAdjustMinPercentSuccessOS", "MinAdjustMinTransactionCountIS", "MinAdjustMinTransactionCountOS", "MinAdjustNTotalTree", "MinAdjustProgressiveness", "MinAdjustVariationTransaction", "MinAssemblyPercent", "MinPercentSuccessIS", "MinPercentSuccessOS", "MinTransactionCountIS", "MinTransactionCountOS", "MinimalSeed", "NTotalTree", "Progressiveness", "StartDate", "SymbolId", "TenantId", "TimeframeId", "ToDateIS", "ToDateOS", "TotalAssemblyIterations", "TotalDecimalWeka", "TotalInstanceWeka", "TransactionTarget", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Variation", "VariationTransaction", "WinningStrategyTotalDOWN", "WinningStrategyTotalUP", "WithoutSchedule" },
                values: new object[] { 1, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9238), 5, 6, "Default Configuration", null, null, null, false, false, false, 5, 2m, 1.5m, 1000000, 5, 1m, 50m, 50m, 360, 180, 150m, 2m, 4m, 5m, 55m, 55m, 300, 100, 100, 300m, 2m, new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9234), 1, "22222222-2222-2222-2222-222222222222", 5, null, null, 1, 5, 1, 600, null, null, null, 50, 4m, 6, 6, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9290), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9288), "22222222-2222-2222-2222-222222222222", 82800, null, null, null },
                    { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9296), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9295), "22222222-2222-2222-2222-222222222222", 64800, null, null, null },
                    { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9300), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 5, 21, 22, 24, 33, 836, DateTimeKind.Utc).AddTicks(9298), "22222222-2222-2222-2222-222222222222", 32400, null, null, null }
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
