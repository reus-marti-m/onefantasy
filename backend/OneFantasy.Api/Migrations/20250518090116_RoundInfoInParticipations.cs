using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class RoundInfoInParticipations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberInRound",
                table: "Participations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Round",
                table: "Participations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberInRound",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Participations");
        }
    }
}
