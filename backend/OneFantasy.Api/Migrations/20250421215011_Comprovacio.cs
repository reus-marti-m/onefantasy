using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class Comprovacio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_MinigameGroups_Participations_ParticipationId",
                table: "MinigameGroups");

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
                name: "FK_MinigameGroups_Participations_ParticipationId5",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId6",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_HomeTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Teams_VisitingTeamId",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameMatchId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigamePlayersId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameScoresId",
                table: "MinigameOptions");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Minigames",
                table: "Minigames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MinigameGroups",
                table: "MinigameGroups");

            migrationBuilder.RenameTable(
                name: "Minigames",
                newName: "Minigame");

            migrationBuilder.RenameTable(
                name: "MinigameGroups",
                newName: "MinigameGroup");

            migrationBuilder.RenameIndex(
                name: "IX_Minigames_VisitingVictoryId",
                table: "Minigame",
                newName: "IX_Minigame_VisitingVictoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigames_HomeVictoryId",
                table: "Minigame",
                newName: "IX_Minigame_HomeVictoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigames_GroupId",
                table: "Minigame",
                newName: "IX_Minigame_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigames_DrawId",
                table: "Minigame",
                newName: "IX_Minigame_DrawId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_VisitingTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_ParticipationId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_ParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameScoresId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameScoresId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigamePlayersId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigamePlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigamePlayers2Id",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigamePlayers2Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigamePlayers1Id",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigamePlayers1Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameMatchId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch3_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch3_MinigameScoresId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch3_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch2B_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch2B_MinigamePlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_MinigameGroupMatch2B_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_Match3Id",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_Match3Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_Match2Id",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_Match2Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_Match1Id",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_Match1Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroups_HomeTeamId",
                table: "MinigameGroup",
                newName: "IX_MinigameGroup_HomeTeamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Minigame",
                table: "Minigame",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MinigameGroup",
                table: "MinigameGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameOptions_DrawId",
                table: "Minigame",
                column: "DrawId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameOptions_HomeVictoryId",
                table: "Minigame",
                column: "HomeVictoryId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameOptions_VisitingVictoryId",
                table: "Minigame",
                column: "VisitingVictoryId",
                principalTable: "MinigameOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_Match1Id",
                table: "MinigameGroup",
                column: "Match1Id",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_Match2Id",
                table: "MinigameGroup",
                column: "Match2Id",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_Match3Id",
                table: "MinigameGroup",
                column: "Match3Id",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch2B_MinigamePlayersId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch3_MinigameScoresId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameMatchId",
                table: "MinigameGroup",
                column: "MinigameMatchId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers1Id",
                table: "MinigameGroup",
                column: "MinigamePlayers1Id",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers2Id",
                table: "MinigameGroup",
                column: "MinigamePlayers2Id",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayersId",
                table: "MinigameGroup",
                column: "MinigamePlayersId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameScoresId",
                table: "MinigameGroup",
                column: "MinigameScoresId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId1",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId2",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId3",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId4",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId5",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId6",
                table: "MinigameGroup",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_HomeTeamId",
                table: "MinigameGroup",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch2B_HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch2B_VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch3_HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch3_VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Teams_VisitingTeamId",
                table: "MinigameGroup",
                column: "VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameMatchId",
                table: "MinigameOptions",
                column: "MinigameMatchId",
                principalTable: "Minigame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigamePlayersId",
                table: "MinigameOptions",
                column: "MinigamePlayersId",
                principalTable: "Minigame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameScoresId",
                table: "MinigameOptions",
                column: "MinigameScoresId",
                principalTable: "Minigame",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameOptions_DrawId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameOptions_HomeVictoryId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameOptions_VisitingVictoryId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_Match1Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_Match2Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_Match3Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameMatchId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers1Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers2Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId1",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId2",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId3",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId4",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId5",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Participations_ParticipationId6",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_HomeTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Teams_VisitingTeamId",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameMatchId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigamePlayersId",
                table: "MinigameOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameOptions_Minigame_MinigameScoresId",
                table: "MinigameOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MinigameGroup",
                table: "MinigameGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Minigame",
                table: "Minigame");

            migrationBuilder.RenameTable(
                name: "MinigameGroup",
                newName: "MinigameGroups");

            migrationBuilder.RenameTable(
                name: "Minigame",
                newName: "Minigames");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_VisitingTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_ParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameScoresId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameScoresId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigamePlayersId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigamePlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigamePlayers2Id",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigamePlayers2Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigamePlayers1Id",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigamePlayers1Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameMatchId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch3_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch3_MinigameScoresId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch3_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch2B_VisitingTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch2B_MinigamePlayersId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_MinigameGroupMatch2B_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_Match3Id",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_Match3Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_Match2Id",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_Match2Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_Match1Id",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_Match1Id");

            migrationBuilder.RenameIndex(
                name: "IX_MinigameGroup_HomeTeamId",
                table: "MinigameGroups",
                newName: "IX_MinigameGroups_HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigame_VisitingVictoryId",
                table: "Minigames",
                newName: "IX_Minigames_VisitingVictoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigame_HomeVictoryId",
                table: "Minigames",
                newName: "IX_Minigames_HomeVictoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigames",
                newName: "IX_Minigames_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Minigame_DrawId",
                table: "Minigames",
                newName: "IX_Minigames_DrawId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MinigameGroups",
                table: "MinigameGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Minigames",
                table: "Minigames",
                column: "Id");

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
                name: "FK_MinigameGroups_Participations_ParticipationId5",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId6",
                table: "MinigameGroups",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_HomeTeamId",
                table: "MinigameGroups",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_HomeTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch2B_VisitingTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch2B_VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_HomeTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_MinigameGroupMatch3_VisitingTeamId",
                table: "MinigameGroups",
                column: "MinigameGroupMatch3_VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroups_Teams_VisitingTeamId",
                table: "MinigameGroups",
                column: "VisitingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId",
                principalTable: "Minigames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameMatchId",
                table: "MinigameOptions",
                column: "MinigameMatchId",
                principalTable: "Minigames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigamePlayersId",
                table: "MinigameOptions",
                column: "MinigamePlayersId",
                principalTable: "Minigames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameOptions_Minigames_MinigameScoresId",
                table: "MinigameOptions",
                column: "MinigameScoresId",
                principalTable: "Minigames",
                principalColumn: "Id");

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
        }
    }
}
