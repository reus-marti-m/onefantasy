using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class FullDomainModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Format = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    CompetitionSeasonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_CompetitionSeasons_CompetitionSeasonId",
                        column: x => x.CompetitionSeasonId,
                        principalTable: "CompetitionSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinigameGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParticipationId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    MinigameScoresId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayersId = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    VisitingTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameMatchId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch2B_MinigamePlayersId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch2B_HomeTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch2B_VisitingTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch3_MinigameScoresId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayers1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayers2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch3_HomeTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch3_VisitingTeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    Match1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Match2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Match3Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinigameGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_HomeTeamId",
                        column: x => x.MinigameGroupMatch2B_HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_VisitingTeamId",
                        column: x => x.MinigameGroupMatch2B_VisitingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_HomeTeamId",
                        column: x => x.MinigameGroupMatch3_HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_VisitingTeamId",
                        column: x => x.MinigameGroupMatch3_VisitingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameGroups_Teams_VisitingTeamId",
                        column: x => x.VisitingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompetitionSeasonId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipationType = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false),
                    MinigameGroupMatch2AId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch2BId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMultiId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameGroupMatch3Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participations_CompetitionSeasons_CompetitionSeasonId",
                        column: x => x.CompetitionSeasonId,
                        principalTable: "CompetitionSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participations_MinigameGroups_MinigameGroupMatch2AId",
                        column: x => x.MinigameGroupMatch2AId,
                        principalTable: "MinigameGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participations_MinigameGroups_MinigameGroupMatch2BId",
                        column: x => x.MinigameGroupMatch2BId,
                        principalTable: "MinigameGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participations_MinigameGroups_MinigameGroupMatch3Id",
                        column: x => x.MinigameGroupMatch3Id,
                        principalTable: "MinigameGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participations_MinigameGroups_MinigameGroupMultiId",
                        column: x => x.MinigameGroupMultiId,
                        principalTable: "MinigameGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Minigames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayers_Type = table.Column<int>(type: "INTEGER", nullable: true),
                    DrawId = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeVictoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    VisitingVictoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minigames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minigames_MinigameGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MinigameGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinigameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Min = table.Column<int>(type: "INTEGER", nullable: true),
                    Max = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameMatchId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayersId = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeGoals = table.Column<int>(type: "INTEGER", nullable: true),
                    AwayGoals = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameScoresId = table.Column<int>(type: "INTEGER", nullable: true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Minigames_MinigameId",
                        column: x => x.MinigameId,
                        principalTable: "Minigames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Options_Minigames_MinigameMatchId",
                        column: x => x.MinigameMatchId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Minigames_MinigamePlayersId",
                        column: x => x.MinigamePlayersId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Minigames_MinigameScoresId",
                        column: x => x.MinigameScoresId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Options_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionSeasons_CompetitionId",
                table: "CompetitionSeasons",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_HomeTeamId",
                table: "MinigameGroups",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match1Id",
                table: "MinigameGroups",
                column: "Match1Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match2Id",
                table: "MinigameGroups",
                column: "Match2Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match3Id",
                table: "MinigameGroups",
                column: "Match3Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_MinigamePlayersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_VisitingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_MinigameScoresId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_VisitingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameMatchId",
                table: "MinigameGroups",
                column: "MinigameMatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayers1Id",
                table: "MinigameGroups",
                column: "MinigamePlayers1Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayers2Id",
                table: "MinigameGroups",
                column: "MinigamePlayers2Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigamePlayersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameScoresId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_ParticipationId",
                table: "MinigameGroups",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_VisitingTeamId",
                table: "MinigameGroups",
                column: "VisitingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_DrawId",
                table: "Minigames",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_GroupId",
                table: "Minigames",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_HomeVictoryId",
                table: "Minigames",
                column: "HomeVictoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigames_VisitingVictoryId",
                table: "Minigames",
                column: "VisitingVictoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MinigameId",
                table: "Options",
                column: "MinigameId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MinigameMatchId",
                table: "Options",
                column: "MinigameMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MinigamePlayersId",
                table: "Options",
                column: "MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MinigameScoresId",
                table: "Options",
                column: "MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_PlayerId",
                table: "Options",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_TeamId",
                table: "Options",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_CompetitionSeasonId",
                table: "Participations",
                column: "CompetitionSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_MinigameGroupMatch2AId",
                table: "Participations",
                column: "MinigameGroupMatch2AId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_MinigameGroupMatch2BId",
                table: "Participations",
                column: "MinigameGroupMatch2BId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_MinigameGroupMatch3Id",
                table: "Participations",
                column: "MinigameGroupMatch3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_MinigameGroupMultiId",
                table: "Participations",
                column: "MinigameGroupMultiId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetitionSeasonId",
                table: "Teams",
                column: "CompetitionSeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_Match1Id",
                table: "MinigameGroups",
                column: "Match1Id",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_Match2Id",
                table: "MinigameGroups",
                column: "Match2Id",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_Match3Id",
                table: "MinigameGroups",
                column: "Match3Id",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_MinigamePlayersId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_MinigameScoresId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameMatchId",
                table: "MinigameGroups",
                column: "MinigameMatchId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayers1Id",
                table: "MinigameGroups",
                column: "MinigamePlayers1Id",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayers2Id",
                table: "MinigameGroups",
                column: "MinigamePlayers2Id",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigamePlayersId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameScoresId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_Options_DrawId",
                table: "Minigames",
                column: "DrawId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_Options_HomeVictoryId",
                table: "Minigames",
                column: "HomeVictoryId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_Options_VisitingVictoryId",
                table: "Minigames",
                column: "VisitingVictoryId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetitionSeasons_Competitions_CompetitionId",
                table: "CompetitionSeasons");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_Match1Id",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_Match2Id",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_Match3Id",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameMatchId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayers1Id",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayers2Id",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Minigames_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Minigames_MinigameId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Minigames_MinigameMatchId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Minigames_MinigamePlayersId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Minigames_MinigameScoresId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId",
                table: "MinigameGroups");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Minigames");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "MinigameGroups");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "CompetitionSeasons");
        }
    }
}
