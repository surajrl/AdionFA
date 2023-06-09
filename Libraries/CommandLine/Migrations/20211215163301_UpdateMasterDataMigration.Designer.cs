﻿// <auto-generated />
using System;
using CommandLine.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommandLine.Migrations
{
    [DbContext(typeof(GeocodingDBContext))]
    [Migration("20211215163301_UpdateMasterDataMigration")]
    partial class UpdateMasterDataMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DPA_PPGA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExternalId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double?>("Lat")
                        .HasColumnType("float");

                    b.Property<double?>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City", "Generic");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.CityGeocoding", b =>
                {
                    b.Property<int>("CityGeocodingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lng")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CityGeocodingId");

                    b.HasIndex("CityId");

                    b.ToTable("CityGeocoding", "Generic");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("A3Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BDCommercial")
                        .HasColumnType("bit");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeoAreaId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SaCode")
                        .HasColumnType("int");

                    b.Property<bool>("ShowBDAcademy")
                        .HasColumnType("bit");

                    b.Property<bool>("ValidateCodeOfConduct")
                        .HasColumnType("bit");

                    b.HasKey("CountryId");

                    b.HasIndex("GeoAreaId");

                    b.ToTable("Country", "Generic");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.GeoArea", b =>
                {
                    b.Property<int>("GeoAreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GeoAreaId");

                    b.ToTable("GeoArea", "Generic");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DPA_PPGA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExternalId")
                        .HasColumnType("int");

                    b.Property<string>("ISO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("IssuerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProvinceId");

                    b.HasIndex("CountryId");

                    b.ToTable("Province", "Generic");
                });

            modelBuilder.Entity("CommandLine.Entities.MasterData", b =>
                {
                    b.Property<int>("MasterDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddresType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("ContactType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContactTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IssuerId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LineOfBusinessId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Pais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PolicyId")
                        .HasColumnType("int");

                    b.Property<string>("PolicyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyStatusName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MasterDataId");

                    b.ToTable("MasterData");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.City", b =>
                {
                    b.HasOne("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId");

                    b.Navigation("Country");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.CityGeocoding", b =>
                {
                    b.HasOne("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Country", b =>
                {
                    b.HasOne("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.GeoArea", "GeoArea")
                        .WithMany("Countries")
                        .HasForeignKey("GeoAreaId");

                    b.Navigation("GeoArea");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Province", b =>
                {
                    b.HasOne("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Country", "Country")
                        .WithMany("Provinces")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.GeoArea", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("BestDoctors.DirectInsurance.Core.Domain.Entities.Generic.Province", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
