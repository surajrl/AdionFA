using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adion.FA.Infrastructure.Core.Data.Migrations.AdionFADb
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
                name: "CurrencyPeriod",
                columns: table => new
                {
                    CurrencyPeriodId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_CurrencyPeriod", x => x.CurrencyPeriodId);
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
                name: "CurrencyPair",
                columns: table => new
                {
                    CurrencyPairId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencyFromId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyToId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_CurrencyPair", x => x.CurrencyPairId);
                    table.ForeignKey(
                        name: "FK_CurrencyPair_Currency_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyPair_Currency_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Status_EntityType_EntityTypeId",
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
                name: "MarketData",
                columns: table => new
                {
                    MarketDataId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarketId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyPairId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyPeriodId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_MarketData", x => x.MarketDataId);
                    table.ForeignKey(
                        name: "FK_MarketData_CurrencyPair_CurrencyPairId",
                        column: x => x.CurrencyPairId,
                        principalTable: "CurrencyPair",
                        principalColumn: "CurrencyPairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketData_CurrencyPeriod_CurrencyPeriodId",
                        column: x => x.CurrencyPeriodId,
                        principalTable: "CurrencyPeriod",
                        principalColumn: "CurrencyPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketData_Market_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Market",
                        principalColumn: "MarketId",
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
                    CurrencyPairId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyPeriodId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_ProjectGlobalConfiguration_CurrencyPair_CurrencyPairId",
                        column: x => x.CurrencyPairId,
                        principalTable: "CurrencyPair",
                        principalColumn: "CurrencyPairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalConfiguration_CurrencyPeriod_CurrencyPeriodId",
                        column: x => x.CurrencyPeriodId,
                        principalTable: "CurrencyPeriod",
                        principalColumn: "CurrencyPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectGlobalConfiguration_CurrencySpread_CurrencySpreadId",
                        column: x => x.CurrencySpreadId,
                        principalTable: "CurrencySpread",
                        principalColumn: "CurrencySpreadId",
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
                name: "MarketDataDetail",
                columns: table => new
                {
                    MarketDataDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarketDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<long>(type: "INTEGER", nullable: false),
                    OpenPrice = table.Column<double>(type: "REAL", nullable: false),
                    MaxPrice = table.Column<double>(type: "REAL", nullable: false),
                    MinPrice = table.Column<double>(type: "REAL", nullable: false),
                    ClosePrice = table.Column<double>(type: "REAL", nullable: false),
                    Volumen = table.Column<double>(type: "REAL", nullable: false),
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
                    table.PrimaryKey("PK_MarketDataDetail", x => x.MarketDataDetailId);
                    table.ForeignKey(
                        name: "FK_MarketDataDetail_MarketData_MarketDataId",
                        column: x => x.MarketDataId,
                        principalTable: "MarketData",
                        principalColumn: "MarketDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectConfiguration",
                columns: table => new
                {
                    ProjectConfigurationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketDataId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    AbsoluteWorkspacePath = table.Column<string>(type: "TEXT", nullable: true),
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
                    CurrencyPairId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyPeriodId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_ProjectConfiguration_CurrencyPair_CurrencyPairId",
                        column: x => x.CurrencyPairId,
                        principalTable: "CurrencyPair",
                        principalColumn: "CurrencyPairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_CurrencyPeriod_CurrencyPeriodId",
                        column: x => x.CurrencyPeriodId,
                        principalTable: "CurrencyPeriod",
                        principalColumn: "CurrencyPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_CurrencySpread_CurrencySpreadId",
                        column: x => x.CurrencySpreadId,
                        principalTable: "CurrencySpread",
                        principalColumn: "CurrencySpreadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_MarketData_MarketDataId",
                        column: x => x.MarketDataId,
                        principalTable: "MarketData",
                        principalColumn: "MarketDataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectConfiguration_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
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
                values: new object[] { 79, "HTG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(3676), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 113, "MYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(9598), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 114, "MVR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(1250), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 115, "MTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(2886), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 116, "MRO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(4525), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 117, "MUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(6162), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 118, "MXN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(7800), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 119, "MDL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 54, DateTimeKind.Utc).AddTicks(9454), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 120, "MNT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(1146), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 121, "MAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(2798), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 122, "MZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(4435), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 123, "MZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(6067), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 124, "MMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(7703), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 125, "ANG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 55, DateTimeKind.Utc).AddTicks(9357), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 126, "NAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(996), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 127, "NPR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(2634), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 128, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(4264), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 129, "NZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(5910), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 130, "NIO", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(7554), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 131, "NGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 56, DateTimeKind.Utc).AddTicks(9211), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 132, "KPW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(845), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 133, "NOK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(2485), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 134, "OMR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(4136), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 135, "PKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(5770), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 136, "XPD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(7449), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 137, "PAB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 57, DateTimeKind.Utc).AddTicks(9112), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 138, "PGK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(755), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 139, "PYG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(2479), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 112, "MGA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(7898), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 140, "PEN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(4129), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 111, "MWK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(6261), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 109, "MKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(2993), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 82, "HUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(8617), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 83, "ISK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(277), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 84, "INR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(1907), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 85, "IDR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(3541), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 86, "IRR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(5174), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 87, "IQD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(6800), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 88, "IEP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 49, DateTimeKind.Utc).AddTicks(8440), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 89, "ILS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(94), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 90, "ITL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(1774), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 91, "JMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(3414), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 92, "JPY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(5054), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 93, "JOD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(6689), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 94, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(8321), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 95, "KZT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 50, DateTimeKind.Utc).AddTicks(9974), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 96, "KES", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(1607), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 97, "KRW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(3239), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 98, "KWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(4875), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 99, "KGS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(6553), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 100, "LAK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(8191), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 101, "LVL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 51, DateTimeKind.Utc).AddTicks(9843), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 102, "LBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(1482), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 103, "LSL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(3124), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 104, "LRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(4787), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 105, "LYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(6435), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 106, "LTL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(8067), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 107, "LUF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 52, DateTimeKind.Utc).AddTicks(9724), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 108, "MOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(1360), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 110, "MGF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 53, DateTimeKind.Utc).AddTicks(4632), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 81, "HKD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(6944), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 141, "PHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(5767), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 143, "Mexico", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(9165), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 175, "SYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(3377), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 176, "TWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(5046), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 177, "TJS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(6769), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 178, "TZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(8406), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 179, "THB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(41), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 180, "TMM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(1690), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 181, "TMT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(3374), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 182, "TOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(5033), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 183, "TTD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(6694), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 184, "TND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(8337), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 185, "TRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 65, DateTimeKind.Utc).AddTicks(9984), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 186, "TRY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(1625), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 187, "UGX", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(3271), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 188, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(4929), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 189, "UAH", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(6587), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 190, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(8243), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 191, "USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 66, DateTimeKind.Utc).AddTicks(9896), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 192, "UYU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(1539), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 193, "UZS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(3186), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 194, "AED", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(4855), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 195, "VUV", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(6501), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 196, "VEB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(8153), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 197, "VEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 67, DateTimeKind.Utc).AddTicks(9806), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 198, "VND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(1448), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 199, "YER", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(3103), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 200, "YUN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(4779), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 201, "ZMK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(6428), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 174, "CHF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(1744), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 142, "XPT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 58, DateTimeKind.Utc).AddTicks(7406), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 173, "SEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 64, DateTimeKind.Utc).AddTicks(100), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 171, "SRG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(6812), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 144, "PLN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(808), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 145, "PTE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(2464), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 146, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(4105), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 147, "ROL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(5823), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 148, "RON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(7473), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 149, "RUB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 59, DateTimeKind.Utc).AddTicks(9292), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 150, "RWF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(937), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 151, "WST", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(2582), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 152, "STD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(4222), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 153, "SAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(5878), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 154, "RSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(7530), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 155, "SCR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 60, DateTimeKind.Utc).AddTicks(9190), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 156, "SLL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(845), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 157, "XAG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(2487), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 158, "SGD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(4125), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 159, "SKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(5820), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 160, "SIT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(7459), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 161, "SBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 61, DateTimeKind.Utc).AddTicks(9109), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 162, "SOS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(750), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 163, "ZAR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(2406), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 164, "ESP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(4071), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 165, "LKR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(6520), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 166, "SHP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(8175), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 167, "SDD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 62, DateTimeKind.Utc).AddTicks(9827), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 168, "SDG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(1475), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 169, "SDP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(3134), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 170, "SRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(5163), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 172, "SZL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 63, DateTimeKind.Utc).AddTicks(8447), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 80, "HNL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(5303), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 203, "ZWD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(9733), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 78, "GYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(2041), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 21, "BZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(8109), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 22, "BMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(9754), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 23, "BTN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(1374), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 24, "BOB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(3018), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 25, "BAM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(4689), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 26, "BWP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(6314), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 27, "BRL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(7935), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 20, "BEF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(6483), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 28, "GBP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 39, DateTimeKind.Utc).AddTicks(9575), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 30, "BGN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(2840), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 31, "BGL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(4468), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 32, "BIF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(6087), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 33, "BYR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(7728), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 34, "XOF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(9379), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 35, "XAF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(1013), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 36, "XPF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(2659), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 29, "BND", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 40, DateTimeKind.Utc).AddTicks(1198), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 37, "KHR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(4290), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 19, "BBD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(4860), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 202, "ZMW", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 68, DateTimeKind.Utc).AddTicks(8082), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "AFN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(4897), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "AFA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(6806), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "ALL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(8560), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "DZD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(240), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "ADF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(1896), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "ADP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(3548), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "AOA", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(5197), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 18, "BDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(3224), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "AON", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(6835), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "AMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(142), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "AWG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(1772), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "AUD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(3412), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "ATS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(5047), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "AZM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(6683), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "AZN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(8319), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 16, "BSD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 37, DateTimeKind.Utc).AddTicks(9960), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "ARS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 36, DateTimeKind.Utc).AddTicks(8490), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 38, "CAD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(5914), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 17, "BHD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 38, DateTimeKind.Utc).AddTicks(1597), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 40, "KYD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(9178), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 62, "ETB", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(5326), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 63, "EUR", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(6971), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 64, "FKP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(8602), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 65, "FJD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(266), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 66, "FIM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(2332), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 67, "FRF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(3976), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 68, "GMD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(5654), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 61, "EEK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(3691), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 69, "GEL", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(7301), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 71, "GHC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(592), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 72, "GHS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(2229), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 73, "GIP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(3866), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 75, "GRD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(7126), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 76, "GTQ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(8760), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 77, "GNF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 48, DateTimeKind.Utc).AddTicks(408), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 39, "CVE", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 41, DateTimeKind.Utc).AddTicks(7536), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 70, "DEM", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 46, DateTimeKind.Utc).AddTicks(8956), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 60, "SVC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(2032), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 74, "XAU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 47, DateTimeKind.Utc).AddTicks(5493), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 58, "ECS", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(8734), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 41, "CLP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(812), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 42, "CNY", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(2434), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 43, "COP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(4072), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 44, "KMF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(5695), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 45, "CDF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(7347), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 59, "EGP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 45, DateTimeKind.Utc).AddTicks(390), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 47, "HRK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(673), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 48, "CUC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(2311), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 46, "CRC", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 42, DateTimeKind.Utc).AddTicks(8998), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 50, "CYP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(5585), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 51, "CZK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(7214), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 52, "DKK", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(8847), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 53, "DJF", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(509), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 54, "DOP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(2176), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 55, "NLG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(3843), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 56, "XCD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(5477), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 57, "XEU", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 44, DateTimeKind.Utc).AddTicks(7108), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 49, "CUP", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 43, DateTimeKind.Utc).AddTicks(3949), "", false, false, "", "", "22222222-2222-2222-2222-222222222222", null, null, null, "" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "M30", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(6030), "", false, false, "30 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "1800" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "H1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(7707), "", false, false, "1 Hour", "", "22222222-2222-2222-2222-222222222222", null, null, null, "3600" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "H4", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(9386), "", false, false, "4 Hour", "", "22222222-2222-2222-2222-222222222222", null, null, null, "14400" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "D1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(1074), "", false, false, "Daily", "", "22222222-2222-2222-2222-222222222222", null, null, null, "86400" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "W", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(2748), "", false, false, "Weekly", "", "22222222-2222-2222-2222-222222222222", null, null, null, "604800" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "MN", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(4423), "", false, false, "Monthly", "", "22222222-2222-2222-2222-222222222222", null, null, null, "2592000" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "M1", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(564), "", false, false, "1 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "60" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "M5", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(2581), "", false, false, "5 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "300" });

            migrationBuilder.InsertData(
                table: "CurrencyPeriod",
                columns: new[] { "CurrencyPeriodId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "M15", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 17, DateTimeKind.Utc).AddTicks(4331), "", false, false, "15 Minute", "", "22222222-2222-2222-2222-222222222222", null, null, null, "900" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 15, "Fifteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(9393), "", false, false, "Fifteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "15" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "One", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(7072), "", false, false, "One", "", "22222222-2222-2222-2222-222222222222", null, null, null, "1" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "Two", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 18, DateTimeKind.Utc).AddTicks(8908), "", false, false, "Two", "", "22222222-2222-2222-2222-222222222222", null, null, null, "2" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "Three", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(649), "", false, false, "Three", "", "22222222-2222-2222-2222-222222222222", null, null, null, "3" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 5, "Five", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(3968), "", false, false, "Five", "", "22222222-2222-2222-2222-222222222222", null, null, null, "5" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 6, "Six", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(5639), "", false, false, "Six", "", "22222222-2222-2222-2222-222222222222", null, null, null, "6" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 7, "Seven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(7262), "", false, false, "Seven", "", "22222222-2222-2222-2222-222222222222", null, null, null, "7" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "Four", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(2296), "", false, false, "Four", "", "22222222-2222-2222-2222-222222222222", null, null, null, "4" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 9, "Nine", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 20, DateTimeKind.Utc).AddTicks(609), "", false, false, "Nine", "", "22222222-2222-2222-2222-222222222222", null, null, null, "9" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 10, "Ten", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(1112), "", false, false, "Ten", "", "22222222-2222-2222-2222-222222222222", null, null, null, "10" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 11, "Eleven", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(2913), "", false, false, "Eleven", "", "22222222-2222-2222-2222-222222222222", null, null, null, "11" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 12, "Twelve", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(4571), "", false, false, "Twelve", "", "22222222-2222-2222-2222-222222222222", null, null, null, "12" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 13, "Thirteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(6181), "", false, false, "Thirteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "13" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 14, "Fourteen", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 23, DateTimeKind.Utc).AddTicks(7787), "", false, false, "Fourteen", "", "22222222-2222-2222-2222-222222222222", null, null, null, "14" });

            migrationBuilder.InsertData(
                table: "CurrencySpread",
                columns: new[] { "CurrencySpreadId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "Symbol", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 8, "Eight", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 19, DateTimeKind.Utc).AddTicks(8950), "", false, false, "Eight", "", "22222222-2222-2222-2222-222222222222", null, null, null, "8" });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 505, "ASSBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(1567), "Assembled Builder", false, false, "AssembledBuilder", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 503, "EXT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(8132), "Extractor", false, false, "Extractor", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 502, "CONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(6443), "Project Global Configuration", false, false, "ProjectGlobalConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 501, "PROJCONFIG", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(4788), "Project Configuration", false, false, "ProjectConfiguration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 500, "PROJ", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(3079), "Project", false, false, "Project", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 400, "MKDT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(1231), "Market Data", false, false, "MarketData", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 504, "STRBUILD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 14, DateTimeKind.Utc).AddTicks(9879), "Strategy Builder", false, false, "StrategyBuilder", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "EntityTypeId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "SETT", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 13, DateTimeKind.Utc).AddTicks(1029), "Setting", false, false, "Setting", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "MarketId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "Forex", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(2100), "", false, false, "Forex", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "America", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(4810), "", false, false, "America", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "Europe", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(6656), "", false, false, "Europe", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "MarketRegion",
                columns: new[] { "MarketRegionId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "Asia", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 24, DateTimeKind.Utc).AddTicks(8277), "", false, false, "Asia", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "LegalName", "Name", "ParentOrganizationId", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 33, DateTimeKind.Utc).AddTicks(751), false, false, "AdionFA", "AdionFA", null, "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "DataExtractor", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(8223), "", false, false, "Data Extractor", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "MacroTransformation", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(9922), "", false, false, "Macro Transformation", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 4, "ChileanTrees", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 35, DateTimeKind.Utc).AddTicks(1557), "", false, false, "Chilean Trees", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectStep",
                columns: new[] { "ProjectStepId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "InitialConfiguration", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(6311), "", false, false, "Initial Configuration", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 4, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(175), false, false, "DefaultWorkspace", "22222222-2222-2222-2222-222222222222", null, null, null, "DefaultWorkspace" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(8412), false, false, "Color", "22222222-2222-2222-2222-222222222222", null, null, null, "Color" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(6694), false, false, "Theme", "22222222-2222-2222-2222-222222222222", null, null, null, "Theme" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "CreatedById", "CreatedByUserName", "CreatedOn", "Inaccesible", "IsDeleted", "Key", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Value" },
                values: new object[] { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 15, DateTimeKind.Utc).AddTicks(4818), false, false, "Culture", "22222222-2222-2222-2222-222222222222", null, null, null, "Culture" });

            migrationBuilder.InsertData(
                table: "CurrencyPair",
                columns: new[] { "CurrencyPairId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencyFromId", "CurrencyToId", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "EUR-USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(4751), 63, 191, "", false, false, "EURUSD", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "CurrencyPair",
                columns: new[] { "CurrencyPairId", "Code", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencyFromId", "CurrencyToId", "Description", "Inaccesible", "IsDeleted", "Name", "TenantId", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "GBP-USD", "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 16, DateTimeKind.Utc).AddTicks(6750), 28, 191, "", false, false, "GBPUSD", "22222222-2222-2222-2222-222222222222", null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalConfiguration",
                columns: new[] { "ProjectGlobalConfigurationId", "AsynchronousMode", "AutoAdjustConfig", "CreatedById", "CreatedByUserName", "CreatedOn", "CurrencyPairId", "CurrencyPeriodId", "CurrencySpreadId", "DepthWeka", "Description", "EndDate", "FromDateIS", "FromDateOS", "Inaccesible", "IsDeleted", "IsProgressiveness", "MaxAdjustConfig", "MaxPercentCorrelation", "MaxRatioTree", "MaximumSeed", "MinAdjustDepthWeka", "MinAdjustMaxRatioTree", "MinAdjustMinPercentSuccessIS", "MinAdjustMinPercentSuccessOS", "MinAdjustMinTransactionCountIS", "MinAdjustMinTransactionCountOS", "MinAdjustNTotalTree", "MinAdjustProgressiveness", "MinAdjustVariationTransaction", "MinAssemblyPercent", "MinPercentSuccessIS", "MinPercentSuccessOS", "MinTransactionCountIS", "MinTransactionCountOS", "MinimalSeed", "NTotalTree", "Progressiveness", "StartDate", "TenantId", "ToDateIS", "ToDateOS", "TotalAssemblyIterations", "TotalDecimalWeka", "TotalInstanceWeka", "TransactionTarget", "UpdatedById", "UpdatedByUserName", "UpdatedOn", "Variation", "VariationTransaction", "WinningStrategyTotalDOWN", "WinningStrategyTotalUP", "WithoutSchedule" },
                values: new object[] { 1, false, false, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(1193), 1, 5, 5, 10, "Default Configuration", null, null, null, false, false, false, 5, 2m, 1.5m, 1000000, 5, 1m, 50m, 50m, 360, 180, 150m, 2m, 4m, 5m, 55m, 55m, 600, 360, 100, 300m, 2m, new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(713), "22222222-2222-2222-2222-222222222222", null, null, 1, 8, 1, 600, null, null, null, 50, 4m, 6, 6, true });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 1, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2700), "Default Schedule America", null, 54000, false, false, 1, 1, new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2697), "22222222-2222-2222-2222-222222222222", 82800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 2, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2705), "Default Schedule Europe", null, 32400, false, false, 2, 1, new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2704), "22222222-2222-2222-2222-222222222222", 64800, null, null, null });

            migrationBuilder.InsertData(
                table: "ProjectGlobalScheduleConfiguration",
                columns: new[] { "ProjectGlobalScheduleConfigurationId", "CreatedById", "CreatedByUserName", "CreatedOn", "Description", "EndDate", "FromTimeInSeconds", "Inaccesible", "IsDeleted", "MarketRegionId", "ProjectGlobalConfigurationId", "StartDate", "TenantId", "ToTimeInSeconds", "UpdatedById", "UpdatedByUserName", "UpdatedOn" },
                values: new object[] { 3, "11111111-1111-1111-11111111111111111", "sysadmin", new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2707), "Default Schedule Asia", null, 3600, false, false, 3, 1, new DateTime(2023, 1, 5, 13, 18, 49, 34, DateTimeKind.Utc).AddTicks(2706), "22222222-2222-2222-2222-222222222222", 32400, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPair_CurrencyFromId",
                table: "CurrencyPair",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyPair_CurrencyToId",
                table: "CurrencyPair",
                column: "CurrencyToId");

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
                name: "IX_MarketData_CurrencyPairId",
                table: "MarketData",
                column: "CurrencyPairId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketData_CurrencyPeriodId",
                table: "MarketData",
                column: "CurrencyPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketData_MarketId",
                table: "MarketData",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketDataDetail_MarketDataId",
                table: "MarketDataDetail",
                column: "MarketDataId");

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
                name: "IX_ProjectConfiguration_CurrencyPairId",
                table: "ProjectConfiguration",
                column: "CurrencyPairId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_CurrencyPeriodId",
                table: "ProjectConfiguration",
                column: "CurrencyPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_CurrencySpreadId",
                table: "ProjectConfiguration",
                column: "CurrencySpreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_MarketDataId",
                table: "ProjectConfiguration",
                column: "MarketDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectConfiguration_ProjectId",
                table: "ProjectConfiguration",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalConfiguration_CurrencyPairId",
                table: "ProjectGlobalConfiguration",
                column: "CurrencyPairId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalConfiguration_CurrencyPeriodId",
                table: "ProjectGlobalConfiguration",
                column: "CurrencyPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGlobalConfiguration_CurrencySpreadId",
                table: "ProjectGlobalConfiguration",
                column: "CurrencySpreadId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Status_EntityTypeId",
                table: "Status",
                column: "EntityTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityServiceHost");

            migrationBuilder.DropTable(
                name: "ExpertAdvisor");

            migrationBuilder.DropTable(
                name: "MarketDataDetail");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "ProjectGlobalScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "ProjectScheduleConfiguration");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "ProjectGlobalConfiguration");

            migrationBuilder.DropTable(
                name: "MarketRegion");

            migrationBuilder.DropTable(
                name: "ProjectConfiguration");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.DropTable(
                name: "CurrencySpread");

            migrationBuilder.DropTable(
                name: "MarketData");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "CurrencyPair");

            migrationBuilder.DropTable(
                name: "CurrencyPeriod");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropTable(
                name: "ProjectStep");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
