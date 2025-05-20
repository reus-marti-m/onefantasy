using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class RoundInfoInParticipations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoundAbbreviation",
                table: "Participations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundAbbreviation",
                table: "Participations");
        }
    }
}
