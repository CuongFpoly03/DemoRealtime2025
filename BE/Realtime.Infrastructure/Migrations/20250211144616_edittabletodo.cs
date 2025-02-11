using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realtime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edittabletodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Todos",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Todos",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Todos",
                type: "uuid",
                nullable: true);
        }
    }
}
