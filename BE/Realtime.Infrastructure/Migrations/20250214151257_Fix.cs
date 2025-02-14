using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realtime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicSQL2s_TopicSQLs_TopicSQLId",
                table: "TopicSQL2s");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id",
                table: "TopicSQLs");

            migrationBuilder.DropIndex(
                name: "IX_TopicSQLs_TopicSQL2Id",
                table: "TopicSQLs");

            migrationBuilder.DropIndex(
                name: "IX_TopicSQL2s_TopicSQLId",
                table: "TopicSQL2s");

            migrationBuilder.AddColumn<Guid>(
                name: "TopicSQL2Id1",
                table: "TopicSQLs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicSQLId",
                table: "TopicSQL2s",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQLs_TopicSQL2Id1",
                table: "TopicSQLs",
                column: "TopicSQL2Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id1",
                table: "TopicSQLs",
                column: "TopicSQL2Id1",
                principalTable: "TopicSQL2s",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id1",
                table: "TopicSQLs");

            migrationBuilder.DropIndex(
                name: "IX_TopicSQLs_TopicSQL2Id1",
                table: "TopicSQLs");

            migrationBuilder.DropColumn(
                name: "TopicSQL2Id1",
                table: "TopicSQLs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicSQLId",
                table: "TopicSQL2s",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQLs_TopicSQL2Id",
                table: "TopicSQLs",
                column: "TopicSQL2Id");

            migrationBuilder.CreateIndex(
                name: "IX_TopicSQL2s_TopicSQLId",
                table: "TopicSQL2s",
                column: "TopicSQLId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSQL2s_TopicSQLs_TopicSQLId",
                table: "TopicSQL2s",
                column: "TopicSQLId",
                principalTable: "TopicSQLs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSQLs_TopicSQL2s_TopicSQL2Id",
                table: "TopicSQLs",
                column: "TopicSQL2Id",
                principalTable: "TopicSQL2s",
                principalColumn: "Id");
        }
    }
}
