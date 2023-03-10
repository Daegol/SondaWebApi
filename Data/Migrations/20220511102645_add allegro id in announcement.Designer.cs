// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220511102645_add allegro id in announcement")]
    partial class addallegroidinannouncement
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.DbEntities.AllegroToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AllegroApi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AllegroTokenId")
                        .HasColumnType("int");

                    b.Property<int?>("AnnouncementCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("AnnouncementId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateUTC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("ExpiresIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WebServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AllegroTokenId");

                    b.HasIndex("AnnouncementCategoryId");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("UserId");

                    b.HasIndex("WebServiceId");

                    b.ToTable("AllegroTokens");
                });

            modelBuilder.Entity("Models.DbEntities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AllegroId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnnouncementCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateUTC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReaded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementCategoryId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("Models.DbEntities.AnnouncementCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateUTC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WebServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WebServiceId");

                    b.ToTable("AnnouncementCategories");
                });

            modelBuilder.Entity("Models.DbEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateUTC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.DbEntities.WebService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateUTC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("WebServices");
                });

            modelBuilder.Entity("Models.DbEntities.AllegroToken", b =>
                {
                    b.HasOne("Models.DbEntities.AllegroToken", null)
                        .WithMany("AllegroTokens")
                        .HasForeignKey("AllegroTokenId");

                    b.HasOne("Models.DbEntities.AnnouncementCategory", null)
                        .WithMany("AllegroTokens")
                        .HasForeignKey("AnnouncementCategoryId");

                    b.HasOne("Models.DbEntities.Announcement", null)
                        .WithMany("AllegroTokens")
                        .HasForeignKey("AnnouncementId");

                    b.HasOne("Models.DbEntities.User", "User")
                        .WithMany("AllegroTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.WebService", null)
                        .WithMany("AllegroTokens")
                        .HasForeignKey("WebServiceId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.DbEntities.Announcement", b =>
                {
                    b.HasOne("Models.DbEntities.AnnouncementCategory", "AnnouncementCategory")
                        .WithMany("Announcements")
                        .HasForeignKey("AnnouncementCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnnouncementCategory");
                });

            modelBuilder.Entity("Models.DbEntities.AnnouncementCategory", b =>
                {
                    b.HasOne("Models.DbEntities.WebService", "WebService")
                        .WithMany("AnnouncementCategories")
                        .HasForeignKey("WebServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebService");
                });

            modelBuilder.Entity("Models.DbEntities.AllegroToken", b =>
                {
                    b.Navigation("AllegroTokens");
                });

            modelBuilder.Entity("Models.DbEntities.Announcement", b =>
                {
                    b.Navigation("AllegroTokens");
                });

            modelBuilder.Entity("Models.DbEntities.AnnouncementCategory", b =>
                {
                    b.Navigation("AllegroTokens");

                    b.Navigation("Announcements");
                });

            modelBuilder.Entity("Models.DbEntities.User", b =>
                {
                    b.Navigation("AllegroTokens");
                });

            modelBuilder.Entity("Models.DbEntities.WebService", b =>
                {
                    b.Navigation("AllegroTokens");

                    b.Navigation("AnnouncementCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
