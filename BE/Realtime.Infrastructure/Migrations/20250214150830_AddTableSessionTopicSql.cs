using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realtime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSessionTopicSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicSQL2s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameSQL2 = table.Column<string>(type: "text", nullable: false),
                    TopicSQLId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicSQL2s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopicSQLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TopicSQL2Id = table.Column<Guid>(type: "uuid", nullable: true),
                    NameSQL = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicSQLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id",
                        column: x => x.TopicSQL2Id,
                        principalTable: "TopicSQL2s",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQL2s_TopicSQLId",
                table: "TopicSQL2s",
                column: "TopicSQLId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQLs_TopicSQL2Id",
                table: "TopicSQLs",
                column: "TopicSQL2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSQL2s_TopicSQLs_TopicSQLId",
                table: "TopicSQL2s",
                column: "TopicSQLId",
                principalTable: "TopicSQLs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicSQL2s_TopicSQLs_TopicSQLId",
                table: "TopicSQL2s");

            migrationBuilder.DropTable(
                name: "TopicSQLs");

            migrationBuilder.DropTable(
                name: "TopicSQL2s");
        }
    }
}
