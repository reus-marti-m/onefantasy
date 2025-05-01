using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class Correccions8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId1",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId2",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId3",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId4",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId5",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId6",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId7",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameOptions_DrawId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_Minigame_MinigameOptions_HomeVictoryId",
                table: "Minigame");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_Match1Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_Match2Id",
                table: "MinigameGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers1Id",
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
                name: "FK_MinigameOptions_Minigame_MinigameId1",
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

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameMatchId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigamePlayersId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameScoresId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_Match1Id",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_Match2Id",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigamePlayers1Id",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_DrawId",
                table: "Minigame");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_HomeVictoryId",
                table: "Minigame");

            migrationBuilder.DropColumn(
                name: "MinigameMatchId",
                table: "MinigameOptions");

            migrationBuilder.DropColumn(
                name: "MinigamePlayersId",
                table: "MinigameOptions");

            migrationBuilder.DropColumn(
                name: "MinigameScoresId",
                table: "MinigameOptions");

            migrationBuilder.DropColumn(
                name: "Match1Id",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "Match2Id",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigamePlayers1Id",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "DrawId",
                table: "Minigame");

            migrationBuilder.DropColumn(
                name: "HomeVictoryId",
                table: "Minigame");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame");

            migrationBuilder.AddColumn<int>(
                name: "MinigameMatchId",
                table: "MinigameOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigamePlayersId",
                table: "MinigameOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameScoresId",
                table: "MinigameOptions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Match1Id",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Match2Id",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigamePlayers1Id",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrawId",
                table: "Minigame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeVictoryId",
                table: "Minigame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId",
                unique: true);

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
                name: "IX_MinigameGroup_Match1Id",
                table: "MinigameGroup",
                column: "Match1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_Match2Id",
                table: "MinigameGroup",
                column: "Match2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigamePlayers1Id",
                table: "MinigameGroup",
                column: "MinigamePlayers1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup",
                column: "ParticipationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_DrawId",
                table: "Minigame",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_HomeVictoryId",
                table: "Minigame",
                column: "HomeVictoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId1",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId2",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId3",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId4",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId5",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId6",
                table: "Minigame",
                column: "GroupId",
                principalTable: "MinigameGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameGroup_GroupId7",
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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Minigame_MinigameOptions_HomeVictoryId",
                table: "Minigame",
                column: "HomeVictoryId",
                principalTable: "MinigameOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_Match1Id",
                table: "MinigameGroup",
                column: "Match1Id",
                principalTable: "Minigame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_Match2Id",
                table: "MinigameGroup",
                column: "Match2Id",
                principalTable: "Minigame",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MinigameGroup_Minigame_MinigamePlayers1Id",
                table: "MinigameGroup",
                column: "MinigamePlayers1Id",
                principalTable: "Minigame",
                principalColumn: "Id");

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
                name: "FK_MinigameOptions_Minigame_MinigameId1",
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
    }
}
