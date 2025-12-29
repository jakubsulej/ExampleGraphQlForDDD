using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Booking");

            migrationBuilder.EnsureSchema(
                name: "Cleaner");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.EnsureSchema(
                name: "ServiceOffer");

            migrationBuilder.CreateTable(
                name: "Booking",
                schema: "Booking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceOfferAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CompletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cleaner",
                schema: "Cleaner",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CleanerAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cleaner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOffer",
                schema: "ServiceOffer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceOfferAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CleanerAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOffer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingReview",
                schema: "Booking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<long>(type: "bigint", nullable: false),
                    ReviewAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingReview_Booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "Booking",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePricingSnapshot",
                schema: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    PricingModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SnapshotDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BookingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePricingSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePricingSnapshot_Booking_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "Booking",
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CleanerOfferedService",
                schema: "Cleaner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferedServiceAggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CleanerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleanerOfferedService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleanerOfferedService_Cleaner_CleanerId",
                        column: x => x.CleanerId,
                        principalSchema: "Cleaner",
                        principalTable: "Cleaner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePricing",
                schema: "ServiceOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    PricingModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceOfferId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePricing_ServiceOffer_ServiceOfferId",
                        column: x => x.ServiceOfferId,
                        principalSchema: "ServiceOffer",
                        principalTable: "ServiceOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingAggregateId",
                schema: "Booking",
                table: "Booking",
                column: "BookingAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingReview_BookingId",
                schema: "Booking",
                table: "BookingReview",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReview_ReviewAggregateId",
                schema: "Booking",
                table: "BookingReview",
                column: "ReviewAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cleaner_CleanerAggregateId",
                schema: "Cleaner",
                table: "Cleaner",
                column: "CleanerAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CleanerOfferedService_CleanerId",
                schema: "Cleaner",
                table: "CleanerOfferedService",
                column: "CleanerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerAggregateId",
                schema: "Customer",
                table: "Customer",
                column: "CustomerAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                schema: "Customer",
                table: "Customer",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffer_ServiceOfferAggregateId",
                schema: "ServiceOffer",
                table: "ServiceOffer",
                column: "ServiceOfferAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePricing_ServiceOfferId",
                schema: "ServiceOffer",
                table: "ServicePricing",
                column: "ServiceOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePricingSnapshot_BookingId",
                schema: "Booking",
                table: "ServicePricingSnapshot",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingReview",
                schema: "Booking");

            migrationBuilder.DropTable(
                name: "CleanerOfferedService",
                schema: "Cleaner");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "ServicePricing",
                schema: "ServiceOffer");

            migrationBuilder.DropTable(
                name: "ServicePricingSnapshot",
                schema: "Booking");

            migrationBuilder.DropTable(
                name: "Cleaner",
                schema: "Cleaner");

            migrationBuilder.DropTable(
                name: "ServiceOffer",
                schema: "ServiceOffer");

            migrationBuilder.DropTable(
                name: "Booking",
                schema: "Booking");
        }
    }
}
