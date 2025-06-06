﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class Correccions3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_Match3Id",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigameMatchId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigamePlayers2Id",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_VisitingVictoryId",
                table: "Minigame");

            migrationBuilder.DropColumn(
                name: "Match3Id",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigameMatchId",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigamePlayers2Id",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigamePlayersId",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "MinigameScoresId",
                table: "MinigameGroup");

            migrationBuilder.DropColumn(
                name: "VisitingVictoryId",
                table: "Minigame");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame",
                column: "GroupId",
                unique: true);

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
                name: "FK_MinigameOptions_Minigame_MinigameId1",
                table: "MinigameOptions",
                column: "MinigameId",
                principalTable: "Minigame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_MinigameOptions_Minigame_MinigameId1",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions");

            migrationBuilder.DropIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame");

            migrationBuilder.AddColumn<int>(
                name: "Match3Id",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameMatchId",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigamePlayers2Id",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigamePlayersId",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinigameScoresId",
                table: "MinigameGroup",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitingVictoryId",
                table: "Minigame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinigameOptions_MinigameId",
                table: "MinigameOptions",
                column: "MinigameId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_Match3Id",
                table: "MinigameGroup",
                column: "Match3Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch2B_MinigamePlayersId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch2B_MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigameGroupMatch3_MinigameScoresId",
                table: "MinigameGroup",
                column: "MinigameGroupMatch3_MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigameMatchId",
                table: "MinigameGroup",
                column: "MinigameMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigamePlayers2Id",
                table: "MinigameGroup",
                column: "MinigamePlayers2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigamePlayersId",
                table: "MinigameGroup",
                column: "MinigamePlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_MinigameScoresId",
                table: "MinigameGroup",
                column: "MinigameScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_GroupId",
                table: "Minigame",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Minigame_VisitingVictoryId",
                table: "Minigame",
                column: "VisitingVictoryId");

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
        }
    }
}
