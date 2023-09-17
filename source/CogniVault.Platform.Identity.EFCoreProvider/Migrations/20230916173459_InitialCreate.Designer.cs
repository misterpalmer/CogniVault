﻿// <auto-generated />
using System;
using CogniVault.Platform.Identity.EFCoreProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CogniVault.Platform.Identity.EFCoreProvider.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20230916173459_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformInterface", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DisabledOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("InterfaceIdentifier")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<Uri>("LogoUri")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ModifiedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PlatformTenantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlatformTenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("Interfaces", (string)null);

                    b.HasAnnotation("Relational:JsonPropertyName", "Interfaces");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<Uri>("LogoUri")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ModifiedOnUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Organizations", (string)null);
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformTenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AdminEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectionString")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DisabledOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<Uri>("LogoUri")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ModifiedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PlatformOrganizationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantIdentifier")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("PlatformOrganizationId");

                    b.ToTable("Tenants", (string)null);

                    b.HasAnnotation("Relational:JsonPropertyName", "Tenants");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastLoginAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ModifiedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PlatformUser");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformInterface", b =>
                {
                    b.HasOne("CogniVault.Platform.Identity.Entities.PlatformTenant", null)
                        .WithMany("InterfacesReadOnly")
                        .HasForeignKey("PlatformTenantId");

                    b.HasOne("CogniVault.Platform.Identity.Entities.PlatformTenant", "Tenant")
                        .WithMany("Interfaces")
                        .HasForeignKey("TenantId");

                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.InterfaceName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PlatformInterfaceId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("InterfaceName");

                            b1.HasKey("PlatformInterfaceId");

                            b1.ToTable("Interfaces");

                            b1.WithOwner()
                                .HasForeignKey("PlatformInterfaceId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformOrganization", b =>
                {
                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.OrganizationName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PlatformOrganizationId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("OrganizationName");

                            b1.HasKey("PlatformOrganizationId");

                            b1.ToTable("Organizations");

                            b1.WithOwner()
                                .HasForeignKey("PlatformOrganizationId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformTenant", b =>
                {
                    b.HasOne("CogniVault.Platform.Identity.Entities.PlatformOrganization", "Organization")
                        .WithMany("Tenants")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CogniVault.Platform.Identity.Entities.PlatformOrganization", null)
                        .WithMany("TenantsReadOnly")
                        .HasForeignKey("PlatformOrganizationId");

                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.TenantName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PlatformTenantId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.HasKey("PlatformTenantId");

                            b1.ToTable("Tenants");

                            b1.WithOwner()
                                .HasForeignKey("PlatformTenantId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformUser", b =>
                {
                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PlatformUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");

                            b1.HasKey("PlatformUserId");

                            b1.ToTable("PlatformUser");

                            b1.WithOwner()
                                .HasForeignKey("PlatformUserId");
                        });

                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.EncryptedPassword", "Password", b1 =>
                        {
                            b1.Property<Guid>("PlatformUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("Password");

                            b1.HasKey("PlatformUserId");

                            b1.ToTable("PlatformUser");

                            b1.WithOwner()
                                .HasForeignKey("PlatformUserId");
                        });

                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.Quota", "Quota", b1 =>
                        {
                            b1.Property<Guid>("PlatformUserId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Value")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Quota");

                            b1.HasKey("PlatformUserId");

                            b1.ToTable("PlatformUser");

                            b1.WithOwner()
                                .HasForeignKey("PlatformUserId");
                        });

                    b.OwnsOne("CogniVault.Platform.Identity.ValueObjects.Username", "Username", b1 =>
                        {
                            b1.Property<Guid>("PlatformUserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("TEXT")
                                .HasColumnName("Username");

                            b1.HasKey("PlatformUserId");

                            b1.ToTable("PlatformUser");

                            b1.WithOwner()
                                .HasForeignKey("PlatformUserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Quota")
                        .IsRequired();

                    b.Navigation("Username")
                        .IsRequired();
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformOrganization", b =>
                {
                    b.Navigation("Tenants");

                    b.Navigation("TenantsReadOnly");
                });

            modelBuilder.Entity("CogniVault.Platform.Identity.Entities.PlatformTenant", b =>
                {
                    b.Navigation("Interfaces");

                    b.Navigation("InterfacesReadOnly");
                });
#pragma warning restore 612, 618
        }
    }
}