using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetitionSeasonToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_CompetitionSeasons_CompetitionSeasonId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_CompetitionSeasons_CompetitionSeasonId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "CompetitionSeasons");

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_CompetitionId",
                table: "Seasons",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Seasons_CompetitionSeasonId",
                table: "Participations",
                column: "CompetitionSeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Seasons_CompetitionSeasonId",
                table: "Teams",
                column: "CompetitionSeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Seasons_CompetitionSeasonId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Seasons_CompetitionSeasonId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.CreateTable(
                name: "CompetitionSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitionSeasons_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionSeasons_CompetitionId",
                table: "CompetitionSeasons",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_CompetitionSeasons_CompetitionSeasonId",
                table: "Participations",
                column: "CompetitionSeasonId",
                principalTable: "CompetitionSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_CompetitionSeasons_CompetitionSeasonId",
                table: "Teams",
                column: "CompetitionSeasonId",
                principalTable: "CompetitionSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
