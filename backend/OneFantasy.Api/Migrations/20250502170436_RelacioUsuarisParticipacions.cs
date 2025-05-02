using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFantasy.Api.Migrations
{
    /// <inheritdoc />
    public partial class RelacioUsuarisParticipacions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserParticipations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ParticipationId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserParticipations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserParticipations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserParticipations_Participations_ParticipationId",
                        column: x => x.ParticipationId,
                        principalTable: "Participations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMinigameGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserParticipationId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinigameGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMinigameGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMinigameGroup_MinigameGroup_MinigameGroupId",
                        column: x => x.MinigameGroupId,
                        principalTable: "MinigameGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMinigameGroup_UserParticipations_UserParticipationId",
                        column: x => x.UserParticipationId,
                        principalTable: "UserParticipations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMinigame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserMinigameGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinigameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMinigame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMinigame_Minigame_MinigameId",
                        column: x => x.MinigameId,
                        principalTable: "Minigame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMinigame_UserMinigameGroup_UserMinigameGroupId",
                        column: x => x.UserMinigameGroupId,
                        principalTable: "UserMinigameGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOptions",
                columns: table => new
                {
                    UserMinigameId = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOptions", x => new { x.UserMinigameId, x.OptionId });
                    table.ForeignKey(
                        name: "FK_UserOptions_MinigameOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "MinigameOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOptions_UserMinigame_UserMinigameId",
                        column: x => x.UserMinigameId,
                        principalTable: "UserMinigame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMinigame_MinigameId",
                table: "UserMinigame",
                column: "MinigameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMinigame_UserMinigameGroupId",
                table: "UserMinigame",
                column: "UserMinigameGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMinigameGroup_MinigameGroupId",
                table: "UserMinigameGroup",
                column: "MinigameGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMinigameGroup_UserParticipationId",
                table: "UserMinigameGroup",
                column: "UserParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOptions_OptionId",
                table: "UserOptions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserParticipations_ParticipationId",
                table: "UserParticipations",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserParticipations_UserId_ParticipationId",
                table: "UserParticipations",
                columns: new[] { "UserId", "ParticipationId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOptions");

            migrationBuilder.DropTable(
                name: "UserMinigame");

            migrationBuilder.DropTable(
                name: "UserMinigameGroup");

            migrationBuilder.DropTable(
                name: "UserParticipations");
        }
    }
}
