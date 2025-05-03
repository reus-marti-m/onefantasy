using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class AfegidaPropUsedBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsedBudget",
                table: "UserParticipations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedBudget",
                table: "UserParticipations");
        }
    }
}
