using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjustModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_MinigameGroups_GroupId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_Options_DrawId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_Options_HomeVictoryId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_Options_VisitingVictoryId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch3Id",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMultiId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Seasons_CompetitionSeasonId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Seasons_CompetitionSeasonId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Participations_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_MinigameGroupMatch3Id",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_MinigameGroupMultiId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match1Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match2Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match3Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameMatchId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayers1Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayers2Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_ParticipationId",
                table: "MinigameGroups");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMatch3Id",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMultiId",
                table: "Participations");

            migrationBuilder.RenameColumn(
                name: "CompetitionSeasonId",
                table: "Teams",
                newName: "SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CompetitionSeasonId",
                table: "Teams",
                newName: "IX_Teams_SeasonId");

            migrationBuilder.RenameColumn(
                name: "CompetitionSeasonId",
                table: "Participations",
                newName: "SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_CompetitionSeasonId",
                table: "Participations",
                newName: "IX_Participations_SeasonId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Minigames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competitions",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MinigameOptions",
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
                    table.PrimaryKey("PK_MinigameOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Minigames_MinigameId",
                        column: x => x.MinigameId,
                        principalTable: "Minigames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Minigames_MinigameMatchId",
                        column: x => x.MinigameMatchId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Minigames_MinigamePlayersId",
                        column: x => x.MinigamePlayersId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Minigames_MinigameScoresId",
                        column: x => x.MinigameScoresId,
                        principalTable: "Minigames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinigameOptions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match1Id",
                table: "MinigameGroups",
                column: "Match1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match2Id",
                table: "MinigameGroups",
                column: "Match2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_Match3Id",
                table: "MinigameGroups",
                column: "Match3Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameMatchId",
                table: "MinigameGroups",
                column: "MinigameMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayers1Id",
                table: "MinigameGroups",
                column: "MinigamePlayers1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayers2Id",
                table: "MinigameGroups",
                column: "MinigamePlayers2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_ParticipationId",
                table: "MinigameGroups",
                column: "ParticipationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameMatchId",
                table: "MinigameOptions",
                column: "MinigameMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigamePlayersId",
                table: "MinigameOptions",
                column: "MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameScoresId",
                table: "MinigameOptions",
                column: "MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_PlayerId",
                table: "MinigameOptions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_TeamId",
                table: "MinigameOptions",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId1",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId2",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId3",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId4",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_MinigameGroups_GroupId",
                table: "Minigames",
                column: "GroupId",
                principalTable: "MinigameGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_MinigameOptions_DrawId",
                table: "Minigames",
                column: "DrawId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_MinigameOptions_HomeVictoryId",
                table: "Minigames",
                column: "HomeVictoryId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_MinigameOptions_VisitingVictoryId",
                table: "Minigames",
                column: "VisitingVictoryId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Seasons_SeasonId",
                table: "Participations",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Seasons_SeasonId",
                table: "Teams",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId1",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId2",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId3",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId4",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_MinigameGroups_GroupId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_MinigameOptions_DrawId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_MinigameOptions_HomeVictoryId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigames_MinigameOptions_VisitingVictoryId",
                table: "Minigames");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Seasons_SeasonId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Seasons_SeasonId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match1Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match2Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_Match3Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameMatchId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayers1Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayers2Id",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigamePlayersId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_MinigameScoresId",
                table: "MinigameGroups");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroups_ParticipationId",
                table: "MinigameGroups");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Teams",
                newName: "CompetitionSeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_SeasonId",
                table: "Teams",
                newName: "IX_Teams_CompetitionSeasonId");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Participations",
                newName: "CompetitionSeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_SeasonId",
                table: "Participations",
                newName: "IX_Participations_CompetitionSeasonId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMatch2AId",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMatch2BId",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMatch3Id",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMultiId",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Minigames",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Competitions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinigameId = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Max = table.Column<int>(type: "INTEGER", nullable: true),
                    Min = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigameMatchId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinigamePlayersId = table.Column<int>(type: "INTEGER", nullable: true),
                    AwayGoals = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeGoals = table.Column<int>(type: "INTEGER", nullable: true),
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
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_MinigamePlayersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_MinigameScoresId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Minigames_MinigameGroups_GroupId",
                table: "Minigames",
                column: "GroupId",
                principalTable: "MinigameGroups",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch2AId",
                table: "Participations",
                column: "MinigameGroupMatch2AId",
                principalTable: "MinigameGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch2BId",
                table: "Participations",
                column: "MinigameGroupMatch2BId",
                principalTable: "MinigameGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMatch3Id",
                table: "Participations",
                column: "MinigameGroupMatch3Id",
                principalTable: "MinigameGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroups_MinigameGroupMultiId",
                table: "Participations",
                column: "MinigameGroupMultiId",
                principalTable: "MinigameGroups",
                principalColumn: "Id");

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
    }
}
