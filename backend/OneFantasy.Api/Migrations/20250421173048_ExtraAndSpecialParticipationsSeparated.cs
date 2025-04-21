using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class ExtraAndSpecialParticipationsSeparated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId5",
                table: "MinigameGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_MinigameGroups_Participations_ParticipationId6",
                table: "MinigameGroups");
        }
    }
}
