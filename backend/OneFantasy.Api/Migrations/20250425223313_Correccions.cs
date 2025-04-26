using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class Correccions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup");

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

            migrationBuilder.AddColumn<int>(
                name: "ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations",
                type: "INTEGER",
                nullable: true);

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
                name: "IX_Participations_ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations",
                column: "ParticipationSpecial_MinigameGroupMatch2AId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations",
                column: "ParticipationSpecial_MinigameGroupMatch2BId");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup",
                column: "ParticipationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch2AId",
                table: "Participations",
                column: "MinigameGroupMatch2AId",
                principalTable: "MinigameGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch2BId",
                table: "Participations",
                column: "MinigameGroupMatch2BId",
                principalTable: "MinigameGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch3Id",
                table: "Participations",
                column: "MinigameGroupMatch3Id",
                principalTable: "MinigameGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMultiId",
                table: "Participations",
                column: "MinigameGroupMultiId",
                principalTable: "MinigameGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations",
                column: "ParticipationSpecial_MinigameGroupMatch2AId",
                principalTable: "MinigameGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_MinigameGroup_ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations",
                column: "ParticipationSpecial_MinigameGroupMatch2BId",
                principalTable: "MinigameGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMatch3Id",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_MinigameGroupMultiId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_MinigameGroup_ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations");

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
                name: "IX_Participations_ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup");

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

            migrationBuilder.DropColumn(
                name: "ParticipationSpecial_MinigameGroupMatch2AId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "ParticipationSpecial_MinigameGroupMatch2BId",
                table: "Participations");

            migrationBuilder.CreateIndex(
                name: "IX_MinigameGroup_ParticipationId",
                table: "MinigameGroup",
                column: "ParticipationId",
                unique: true);

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
        }
    }
}
