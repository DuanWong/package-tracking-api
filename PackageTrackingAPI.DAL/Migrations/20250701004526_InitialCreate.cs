using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PackageTrackingAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    PackageID = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrackingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    SenderID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverName = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverAddress = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentStatus = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageID);
                    table.ForeignKey(
                        name: "FK_Packages_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackingEvents",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PackageID = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingEvents", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_TrackingEvents_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "alice.johnson@example.com", "Alice Johnson", "hashed_password_1", "Admin" },
                    { 2, "bob.smith@example.com", "Bob Smith", "hashed_password_2", "User" },
                    { 3, "charlie.brown@example.com", "Charlie Brown", "hashed_password_3", "User" },
                    { 4, "david.wilson@example.com", "David Wilson", "hashed_password_4", "Manager" },
                    { 5, "emma.davis@example.com", "Emma Davis", "hashed_password_5", "User" }
                });

            migrationBuilder.InsertData(
                table: "Alerts",
                columns: new[] { "AlertID", "Message", "PackageID", "Timestamp", "UserID" },
                values: new object[,]
                {
                    { 1, "Package delayed due to weather conditions.", 101, new DateTime(2025, 4, 11, 5, 30, 0, 0, DateTimeKind.Local), 1 },
                    { 2, "Package out for delivery.", 102, new DateTime(2025, 4, 11, 7, 45, 0, 0, DateTimeKind.Local), 2 },
                    { 3, "Package delivered successfully.", 103, new DateTime(2025, 4, 10, 11, 20, 0, 0, DateTimeKind.Local), 3 },
                    { 4, "Package returned to sender.", 104, new DateTime(2025, 4, 9, 9, 5, 0, 0, DateTimeKind.Local), 4 },
                    { 5, "Package pending at pickup location.", 105, new DateTime(2025, 4, 8, 13, 30, 0, 0, DateTimeKind.Local), 5 }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageID", "CreatedAt", "CurrentStatus", "ReceiverAddress", "ReceiverName", "SenderID", "TrackingNumber" },
                values: new object[,]
                {
                    { 101, new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6348), "Created", "123 Main St, City, Country", "John Doe", 1, "TRACK1234" },
                    { 102, new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6353), "Shipped", "456 Oak St, City, Country", "Jane Smith", 2, "TRACK5678" },
                    { 103, new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6355), "Delivered", "789 Pine St, City, Country", "Alice Johnson", 3, "TRACK9101" },
                    { 104, new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6357), "In Transit", "101 Maple St, City, Country", "Bob Williams", 4, "TRACK1121" },
                    { 105, new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6359), "Returned", "202 Birch St, City, Country", "Charlie Brown", 5, "TRACK3141" }
                });

            migrationBuilder.InsertData(
                table: "TrackingEvents",
                columns: new[] { "EventID", "Location", "PackageID", "Status", "Timestamp" },
                values: new object[,]
                {
                    { 1, "Warehouse A", 101, "Package created", new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6393) },
                    { 2, "Warehouse B", 102, "Package shipped", new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6403) },
                    { 3, "Delivery Point", 103, "Package delivered", new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6405) },
                    { 4, "Distribution Center", 104, "Package in transit", new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6407) },
                    { 5, "Returned to sender", 105, "Package returned", new DateTime(2025, 7, 1, 0, 45, 24, 638, DateTimeKind.Utc).AddTicks(6408) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserID",
                table: "Alerts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_SenderID",
                table: "Packages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingEvents_PackageID",
                table: "TrackingEvents",
                column: "PackageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "TrackingEvents");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
