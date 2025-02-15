using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realtime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTable2SQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicSQLs");

            migrationBuilder.DropTable(
                name: "TopicSQL2s");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicSQL2s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameSQL2 = table.Column<string>(type: "text", nullable: false)
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
                    TopicSQL2Id1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    NameSQL = table.Column<string>(type: "text", nullable: false),
                    TopicSQL2Id = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicSQLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id1",
                        column: x => x.TopicSQL2Id1,
                        principalTable: "TopicSQL2s",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQLs_TopicSQL2Id1",
                table: "TopicSQLs",
                column: "TopicSQL2Id1");
        }
    }
}
