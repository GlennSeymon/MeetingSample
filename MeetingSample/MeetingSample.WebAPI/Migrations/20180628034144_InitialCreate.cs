using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetingSample.WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    StateDesc = table.Column<string>(nullable: true),
                    MeetDate = table.Column<DateTime>(nullable: false),
                    VenueCode = table.Column<int>(nullable: false),
                    MeetCatArea = table.Column<string>(nullable: true),
                    MeetCatCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetCode);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingMeetCode = table.Column<int>(nullable: true),
                    RaceNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Distance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceCode);
                    table.ForeignKey(
                        name: "FK_Races_Meetings_MeetingMeetCode",
                        column: x => x.MeetingMeetCode,
                        principalTable: "Meetings",
                        principalColumn: "MeetCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "MeetCode", "MeetCatArea", "MeetCatCode", "MeetDate", "StateDesc", "Title", "VenueCode" },
                values: new object[,]
                {
                    { 1, "MC", 0, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIC", "Meeting 1", 151 },
                    { 2, "MC", 0, new DateTime(2018, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIC", "Meeting 2", 151 },
                    { 3, "MC", 0, new DateTime(2018, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIC", "Meeting 3", 151 },
                    { 4, "MC", 0, new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIC", "Meeting 4", 151 },
                    { 5, "MC", 0, new DateTime(2018, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "VIC", "Meeting 5", 151 }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceCode", "Distance", "MeetingMeetCode", "Name", "RaceNumber" },
                values: new object[,]
                {
                    { 1, 1000, null, "Meeting 1, Race 1", 1 },
                    { 2, 1200, null, "Meeting 1, Race 2", 2 },
                    { 3, 1300, null, "Meeting 1, Race 3", 3 },
                    { 4, 1000, null, "Meeting 2, Race 1", 1 },
                    { 5, 1200, null, "Meeting 2, Race 2", 2 },
                    { 6, 1300, null, "Meeting 2, Race 3", 3 },
                    { 7, 1000, null, "Meeting 2, Race 4", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Races_MeetingMeetCode",
                table: "Races",
                column: "MeetingMeetCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
