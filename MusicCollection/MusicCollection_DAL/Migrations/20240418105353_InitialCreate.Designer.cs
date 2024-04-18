﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicCollection_DAL;

#nullable disable

namespace MusicCollection_DAL.Migrations
{
    [DbContext(typeof(SpotifyContext))]
    [Migration("20240418105353_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("M_DbEntitySequence");

            modelBuilder.Entity("M_AlbumM_Artist", b =>
                {
                    b.Property<int>("AlbumsId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("M_AlbumM_Artist");
                });

            modelBuilder.Entity("M_ArtistM_Song", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("M_ArtistM_Song");
                });

            modelBuilder.Entity("M_GenreM_Song", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("GenresId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("M_GenreM_Song");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_DbEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [M_DbEntitySequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<DateOnly>("DataUpdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateOnly>("UploadDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("UploaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Album", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://img.freepik.com/free-vector/realistic-music-record-label-disk-mockup_1017-33906.jpg?size=626&ext=jpg&ga=GA1.1.1908636980.1711929600&semt=ais");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("UploaderId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Artist", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("UploaderId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Genre", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("#fff");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://cdn.venngage.com/template/thumbnail/small/bf008bfe-9bf6-4511-b795-e86f070bfff5.webp");

                    b.HasIndex("UploaderId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Song", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("Downloads")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Plays")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Poster")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://cdn.pixabay.com/photo/2014/04/02/14/04/vinyl-306070_1280.png");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AlbumId");

                    b.HasIndex("UploaderId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_User", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasIndex("StatusId");

                    b.HasIndex("UploaderId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_UserStatus", b =>
                {
                    b.HasBaseType("MusicCollection_DAL.Models.M_DbEntity");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("UploaderId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("M_AlbumM_Artist", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MusicCollection_DAL.Models.M_Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("M_ArtistM_Song", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MusicCollection_DAL.Models.M_Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("M_GenreM_Song", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MusicCollection_DAL.Models.M_Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Album", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany("UploadedAlbums")
                        .HasForeignKey("UploaderId");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Artist", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany("UploadedArtists")
                        .HasForeignKey("UploaderId");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Genre", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany("UploadedGenres")
                        .HasForeignKey("UploaderId");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Song", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany("UploadedSongs")
                        .HasForeignKey("UploaderId");

                    b.Navigation("Album");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_User", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_UserStatus", "Status")
                        .WithMany("Users")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany()
                        .HasForeignKey("UploaderId");

                    b.Navigation("Status");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_UserStatus", b =>
                {
                    b.HasOne("MusicCollection_DAL.Models.M_User", "Uploader")
                        .WithMany("UploadedStatuses")
                        .HasForeignKey("UploaderId");

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_User", b =>
                {
                    b.Navigation("UploadedAlbums");

                    b.Navigation("UploadedArtists");

                    b.Navigation("UploadedGenres");

                    b.Navigation("UploadedSongs");

                    b.Navigation("UploadedStatuses");
                });

            modelBuilder.Entity("MusicCollection_DAL.Models.M_UserStatus", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
