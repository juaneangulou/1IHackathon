using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneInc.Hackathon.ESBIntegrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endpoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HttpVerb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endpoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESBTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESBTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasRetries = table.Column<bool>(type: "bit", nullable: false),
                    RetryInterval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxRetries = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndpointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrphanMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingAttempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrphanMessages_ESBTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "ESBTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Endpoints",
                columns: new[] { "Id", "Alias", "HttpVerb", "Url" },
                values: new object[,]
                {
                    { new Guid("20ce2306-b423-4b16-bc6c-db776412856e"), "Notifications", "POST", "http://localhost:5101/api/webhook" },
                    { new Guid("68409100-2390-4666-8c18-c5ef76d6cda6"), "Webhooks", "POST", "http://localhost:5102/api/webhook" },
                    { new Guid("eb7b263d-d38e-47b1-8277-bd2429025285"), "Reporting", "POST", "http://localhost:5103/api/webhook" }
                });

            migrationBuilder.InsertData(
                table: "MessageTypes",
                columns: new[] { "Id", "HasRetries", "MaxRetries", "Name", "RetryInterval" },
                values: new object[,]
                {
                    { new Guid("282e0058-6d1e-4812-8e2b-523fde31b04e"), false, 0, "Broadcast", "1M" },
                    { new Guid("3c63493a-159c-4c92-8a1c-e4305d0d5870"), true, 3, "Notification", "1M,2M,3M" },
                    { new Guid("5643e06e-ff1e-4233-b9b4-a34fe62a17ec"), true, 2, "Error", "5M,10M" }
                });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "Id", "EndpointId", "MessageType" },
                values: new object[,]
                {
                    { new Guid("294676e2-c3ef-44d5-a84e-67080c7321f0"), new Guid("68409100-2390-4666-8c18-c5ef76d6cda6"), "Broadcast" },
                    { new Guid("3240ba42-c3fc-4d38-9307-1f1a6e1e3dfe"), new Guid("20ce2306-b423-4b16-bc6c-db776412856e"), "Error" },
                    { new Guid("3987be89-cca3-43e4-9654-c1be3b89d65d"), new Guid("68409100-2390-4666-8c18-c5ef76d6cda6"), "Error" },
                    { new Guid("48344421-da25-4c08-9135-d1738fc8ee66"), new Guid("eb7b263d-d38e-47b1-8277-bd2429025285"), "Broadcast" },
                    { new Guid("9e7795f8-d934-4482-adc4-89d5bc9cae63"), new Guid("20ce2306-b423-4b16-bc6c-db776412856e"), "Notification" },
                    { new Guid("fbf6afa8-b950-414b-9aea-6718ae2c364a"), new Guid("20ce2306-b423-4b16-bc6c-db776412856e"), "Broadcast" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrphanMessages_TransactionId",
                table: "OrphanMessages",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endpoints");

            migrationBuilder.DropTable(
                name: "MessageTypes");

            migrationBuilder.DropTable(
                name: "OrphanMessages");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "ESBTransactions");
        }
    }
}
