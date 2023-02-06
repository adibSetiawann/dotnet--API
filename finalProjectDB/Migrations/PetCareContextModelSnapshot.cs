﻿// <auto-generated />
using System;
using FinalProjectDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace finalProjectDB.Migrations
{
    [DbContext(typeof(PetCareContext))]
    partial class PetCareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinalProjectDB.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BillId"));

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BillDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("BillNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BillStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BranchOfficeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VaNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BillId");

                    b.HasIndex("BranchOfficeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("FinalProjectDB.BillDetail", b =>
                {
                    b.Property<Guid>("BillDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BillId")
                        .HasColumnType("integer");

                    b.Property<int>("CarePriceId")
                        .HasColumnType("integer");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PetId")
                        .HasColumnType("integer");

                    b.HasKey("BillDetailId");

                    b.HasIndex("BillId");

                    b.HasIndex("CarePriceId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PetId");

                    b.ToTable("BillDetail");
                });

            modelBuilder.Entity("FinalProjectDB.BranchOffice", b =>
                {
                    b.Property<int>("BranchOfficeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BranchOfficeId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BranchOffice_name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<int>("CodePosId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobilePhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(12)");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("BranchOfficeId");

                    b.HasIndex("CityId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("BranchOffice");
                });

            modelBuilder.Entity("FinalProjectDB.CarePrice", b =>
                {
                    b.Property<int>("CarePriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarePriceId"));

                    b.Property<int>("CareTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("CarePriceId");

                    b.HasIndex("CareTypeId");

                    b.ToTable("CarePrice");
                });

            modelBuilder.Entity("FinalProjectDB.CareType", b =>
                {
                    b.Property<int>("CareTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CareTypeId"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CareTypeId");

                    b.ToTable("CareType");
                });

            modelBuilder.Entity("FinalProjectDB.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CityId"));

                    b.Property<string>("Descriptionn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("CityId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("FinalProjectDB.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<int>("CodePosId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MobilePhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(12)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("CustomerId");

                    b.HasIndex("CityId");

                    b.HasIndex("GenderId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("FinalProjectDB.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("CodePosId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("MobilePhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(10)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PetTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("DoctorId");

                    b.HasIndex("CityId");

                    b.HasIndex("GenderId");

                    b.HasIndex("PetTypeId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("FinalProjectDB.EmailStatus", b =>
                {
                    b.Property<int>("ValidationUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ValidationUserId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("OtpCode")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("ValidationUserId");

                    b.ToTable("EmailStatus");
                });

            modelBuilder.Entity("FinalProjectDB.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenderId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenderId");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("FinalProjectDB.Pet", b =>
                {
                    b.Property<int>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PetId"));

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<int>("GenderId")
                        .HasColumnType("integer");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PetTypeId")
                        .HasColumnType("integer");

                    b.HasKey("PetId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("GenderId");

                    b.HasIndex("PetTypeId");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("FinalProjectDB.PetType", b =>
                {
                    b.Property<int>("PetTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PetTypeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PetTypeId");

                    b.ToTable("PetType");
                });

            modelBuilder.Entity("FinalProjectDB.Province", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProvinceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProvinceId");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("FinalProjectDB.Bill", b =>
                {
                    b.HasOne("FinalProjectDB.BranchOffice", "BranchOffice")
                        .WithMany()
                        .HasForeignKey("BranchOfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Customer", null)
                        .WithMany("Bill")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BranchOffice");
                });

            modelBuilder.Entity("FinalProjectDB.BillDetail", b =>
                {
                    b.HasOne("FinalProjectDB.Bill", null)
                        .WithMany("BillDetail")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.CarePrice", null)
                        .WithMany("BillDetail")
                        .HasForeignKey("CarePriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Doctor", null)
                        .WithMany("BillDetail")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Pet", null)
                        .WithMany("BillDetail")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalProjectDB.BranchOffice", b =>
                {
                    b.HasOne("FinalProjectDB.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("FinalProjectDB.CarePrice", b =>
                {
                    b.HasOne("FinalProjectDB.CareType", "CareType")
                        .WithMany()
                        .HasForeignKey("CareTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CareType");
                });

            modelBuilder.Entity("FinalProjectDB.City", b =>
                {
                    b.HasOne("FinalProjectDB.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("FinalProjectDB.Customer", b =>
                {
                    b.HasOne("FinalProjectDB.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Gender");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("FinalProjectDB.Doctor", b =>
                {
                    b.HasOne("FinalProjectDB.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Gender");

                    b.Navigation("PetType");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("FinalProjectDB.Pet", b =>
                {
                    b.HasOne("FinalProjectDB.Customer", null)
                        .WithMany("Pet")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProjectDB.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("PetType");
                });

            modelBuilder.Entity("FinalProjectDB.Bill", b =>
                {
                    b.Navigation("BillDetail");
                });

            modelBuilder.Entity("FinalProjectDB.CarePrice", b =>
                {
                    b.Navigation("BillDetail");
                });

            modelBuilder.Entity("FinalProjectDB.Customer", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("FinalProjectDB.Doctor", b =>
                {
                    b.Navigation("BillDetail");
                });

            modelBuilder.Entity("FinalProjectDB.Pet", b =>
                {
                    b.Navigation("BillDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
