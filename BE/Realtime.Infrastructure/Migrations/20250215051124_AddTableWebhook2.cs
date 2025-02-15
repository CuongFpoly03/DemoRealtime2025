using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Realtime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableWebhook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentWeekHooks");

            migrationBuilder.DropTable(
                name: "WebHookDatas");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.CreateTable(
                name: "WebhookEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WebhookEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriberName = table.Column<string>(type: "text", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    SecretKey = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookEvents");

            migrationBuilder.DropTable(
                name: "WebhookLogs");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptions");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    ProductOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebHookDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHookDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebHookDatas_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentWeekHooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentWeekHooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentWeekHooks_WebHookDatas_DataId",
                        column: x => x.DataId,
                        principalTable: "WebHookDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWeekHooks_DataId",
                table: "PaymentWeekHooks",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_WebHookDatas_PaymentId",
                table: "WebHookDatas",
                column: "PaymentId");
        }
    }
}
