using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicCollection_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "M_DbEntitySequence");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "https://img.freepik.com/free-vector/realistic-music-record-label-disk-mockup_1017-33906.jpg?size=626&ext=jpg&ga=GA1.1.1908636980.1711929600&semt=ais")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_AlbumM_Artist",
                columns: table => new
                {
                    AlbumsId = table.Column<int>(type: "int", nullable: false),
                    ArtistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_AlbumM_Artist", x => new { x.AlbumsId, x.ArtistsId });
                    table.ForeignKey(
                        name: "FK_M_AlbumM_Artist_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_M_AlbumM_Artist_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "https://cdn.venngage.com/template/thumbnail/small/bf008bfe-9bf6-4511-b795-e86f070bfff5.webp"),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "#fff")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_ArtistM_Song",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_ArtistM_Song", x => new { x.ArtistsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_M_ArtistM_Song_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "M_GenreM_Song",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    SongsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_GenreM_Song", x => new { x.GenresId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_M_GenreM_Song_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "https://cdn.pixabay.com/photo/2014/04/02/14/04/vinyl-306070_1280.png"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Plays = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Downloads = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_DbEntitySequence]"),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    DataUpdate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UploaderId",
                table: "Albums",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UploaderId",
                table: "Artists",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_UploaderId",
                table: "Genres",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_M_AlbumM_Artist_ArtistsId",
                table: "M_AlbumM_Artist",
                column: "ArtistsId");

            migrationBuilder.CreateIndex(
                name: "IX_M_ArtistM_Song_SongsId",
                table: "M_ArtistM_Song",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_M_GenreM_Song_SongsId",
                table: "M_GenreM_Song",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UploaderId",
                table: "Songs",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_UploaderId",
                table: "Statuses",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatusId",
                table: "Users",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UploaderId",
                table: "Users",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Users_UploaderId",
                table: "Albums",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UploaderId",
                table: "Artists",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_UploaderId",
                table: "Genres",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_M_ArtistM_Song_Songs_SongsId",
                table: "M_ArtistM_Song",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_M_GenreM_Song_Songs_SongsId",
                table: "M_GenreM_Song",
                column: "SongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Users_UploaderId",
                table: "Songs",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Users_UploaderId",
                table: "Statuses",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id");


            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Name", "Poster", "BackgroundColor", "UploadDate", "DataUpdate" },
                values: new object[,]
                {
                    { "rock", "rock_poster.jpg", "#006450", DateTime.Now, DateTime.Now },
                    { "pop", "pop_poster.jpg", "#148a08", DateTime.Now, DateTime.Now },
                    { "hip_hop", "hiphop_poster.jpg", "#503750", DateTime.Now, DateTime.Now },
                    { "music", "music_poster.jpg", "#dc148c", DateTime.Now, DateTime.Now },
                    { "podcasts", "podcasts_poster.jpg", "#006450", DateTime.Now, DateTime.Now },
                    { "live events", "live_events_poster.jpg", "#8400e7", DateTime.Now, DateTime.Now },
                    { "made foryou", "made_foryou_poster.jpg", "#1e3264", DateTime.Now, DateTime.Now },
                    { "new releases", "new_releases_poster.jpg", "#e8115b", DateTime.Now, DateTime.Now },
                    { "merch", "merch_poster.jpg", "#27856a", DateTime.Now, DateTime.Now },
                    { "mood", "mood_poster.jpg", "#e1118c", DateTime.Now, DateTime.Now },
                    { "comedy", "comedy_poster.jpg", "#477d95", DateTime.Now, DateTime.Now },
                    { "educational", "educational_poster.jpg", "#dc148c", DateTime.Now, DateTime.Now },
                    { "true crime", "true_crime_poster.jpg", "#af2896", DateTime.Now, DateTime.Now },
                    { "sports", "sports_poster.jpg", "#dc148c", DateTime.Now, DateTime.Now },
                    { "charts", "charts_poster.jpg", "#8d67ab", DateTime.Now, DateTime.Now },
                    { "dance electronic", "dance_electronic_poster.jpg", "#d84000", DateTime.Now, DateTime.Now },
                    { "chill", "chill_poster.jpg", "#d84000", DateTime.Now, DateTime.Now }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Name", "UploadDate", "DataUpdate" },
                values: new object[,]
                {
                    { "David Kushner", DateTime.Now, DateTime.Now }, //18
                    { "YAKTAK", DateTime.Now, DateTime.Now }, //19
                    { "MiyaGi", DateTime.Now, DateTime.Now }, //20
                    { "Endspiel", DateTime.Now, DateTime.Now }, // 21
                    { "Artem Pivovarov", DateTime.Now, DateTime.Now } // 22
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Title", "Poster", "UploadDate", "DataUpdate" },
                values: new object[,]
                {
                    {
                        "Daylight",
                        "https://upload.wikimedia.org/wikipedia/en/1/1e/David_Kushner-_Daylight.png",
                        DateTime.Now,
                        DateTime.Now
                    },
                });

            migrationBuilder.InsertData(
                table: "M_AlbumM_Artist",
                columns: new[] { "AlbumsId", "ArtistsId" },
                values: new object[,]
                {
                    {
                        23, // Daylight
                        18, // David Kushner
                    },
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Title", "Poster", "FilePath", "Duration", "AlbumId", "UploadDate", "DataUpdate" },
                values: new object[,]
                {
                {
                    "Daylight",
                    "https://cdn.pixabay.com/photo/2014/04/02/14/04/vinyl-306070_1280.png",
                    "david_kushner_daylight.mp3",
                    TimeSpan.FromMinutes(3).Add(TimeSpan.FromSeconds(33)),
                    23, // AlbumId of the "Daylight" album
                    DateTime.Now,
                    DateTime.Now
                }
                });

            migrationBuilder.InsertData(
               table: "M_ArtistM_Song",
               columns: new[] { "SongsId", "ArtistsId" },
               values: new object[,]
               {
                {
                    24, 18
                }
               });


            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Status", "UploadDate", "DataUpdate" },
                values: new object[,]
                {
                    { "Active", DateTime.Now, DateTime.Now },
                    { "Inactive", DateTime.Now, DateTime.Now },
                    { "Premium", DateTime.Now, DateTime.Now },
                    { "Free", DateTime.Now, DateTime.Now },
                    { "Artist", DateTime.Now, DateTime.Now },
                    { "Listener", DateTime.Now, DateTime.Now },
                    { "Moderator", DateTime.Now, DateTime.Now },
                    { "Editor", DateTime.Now, DateTime.Now },
                    { "Administrator", DateTime.Now, DateTime.Now },
                    // Add more statuses as needed
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Users_UploaderId",
                table: "Statuses");

            migrationBuilder.DropTable(
                name: "M_AlbumM_Artist");

            migrationBuilder.DropTable(
                name: "M_ArtistM_Song");

            migrationBuilder.DropTable(
                name: "M_GenreM_Song");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropSequence(
                name: "M_DbEntitySequence");
        }
    }
}
