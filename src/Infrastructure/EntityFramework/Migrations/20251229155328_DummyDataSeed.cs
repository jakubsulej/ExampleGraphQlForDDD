using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DummyDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =========================
            // CUSTOMERS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Customer].[Customer] ON;

                INSERT INTO [Customer].[Customer]
                ([Id], [CustomerAggregateId], [Name], [Email], [PhoneNumber], [Address],
                 [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived])
                VALUES
                (1, '11111111-1111-1111-1111-111111111111', 'John Smith', 'john.smith@example.com', '+1-555-0101', '123 Main St, New York, NY 10001', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (2, '22222222-2222-2222-2222-222222222222', 'Emily Johnson', 'emily.johnson@example.com', '+1-555-0102', '456 Oak Ave, Los Angeles, CA 90001', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (3, '33333333-3333-3333-3333-333333333333', 'Michael Brown', 'michael.brown@example.com', '+1-555-0103', '789 Pine Rd, Chicago, IL 60601', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (4, '44444444-4444-4444-4444-444444444444', 'Sarah Davis', 'sarah.davis@example.com', '+1-555-0104', '321 Elm St, Houston, TX 77001', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (5, '55555555-5555-5555-5555-555555555555', 'David Wilson', 'david.wilson@example.com', '+1-555-0105', '654 Maple Dr, Phoenix, AZ 85001', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0);

                SET IDENTITY_INSERT [Customer].[Customer] OFF;
            ");

            // =========================
            // CLEANERS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Cleaner].[Cleaner] ON;

                INSERT INTO [Cleaner].[Cleaner]
                ([Id], [CleanerAggregateId], [Name], [Description], [PhoneNumber], [Email],
                 [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived])
                VALUES
                (1, 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Sparkle Clean Services', 'Professional cleaning service with 10+ years experience.', '+1-555-1001', 'info@sparkleclean.com', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (2, 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'EcoClean Solutions', 'Eco-friendly cleaning services using only green products.', '+1-555-1002', 'contact@ecoclean.com', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (3, 'cccccccc-cccc-cccc-cccc-cccccccccccc', 'Premium Housekeeping', 'Luxury housekeeping services for high-end homes and offices.', '+1-555-1003', 'hello@premiumhousekeeping.com', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                (4, 'dddddddd-dddd-dddd-dddd-dddddddddddd', 'Quick Clean Express', 'Fast and reliable cleaning services. Same-day service available.', '+1-555-1004', 'service@quickclean.com', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0);

                SET IDENTITY_INSERT [Cleaner].[Cleaner] OFF;
            ");

            // =========================
            // SERVICE OFFERS
            // =========================
            migrationBuilder.Sql(@"
                INSERT INTO [ServiceOffer].[ServiceOffer]
                ([ServiceOfferAggregateId], [CleanerAggregateId], [Title], [Description],
                 [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived])
                VALUES
                ('11111111-aaaa-aaaa-aaaa-111111111111', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Standard House Cleaning', 'Complete house cleaning including all rooms, bathrooms, kitchen, and living areas.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                ('22222222-aaaa-aaaa-aaaa-222222222222', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Deep Cleaning Service', 'Intensive deep cleaning for move-in/move-out or seasonal cleaning.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                ('33333333-bbbb-bbbb-bbbb-333333333333', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Eco-Friendly Office Cleaning', 'Green cleaning service for offices and commercial spaces.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                ('44444444-bbbb-bbbb-bbbb-444444444444', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Window Cleaning', 'Professional window cleaning service for residential and commercial properties.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                ('55555555-cccc-cccc-cccc-555555555555', 'cccccccc-cccc-cccc-cccc-cccccccccccc', 'Luxury Home Cleaning', 'Premium cleaning service for luxury homes with attention to high-end finishes.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0),
                ('66666666-dddd-dddd-dddd-666666666666', 'dddddddd-dddd-dddd-dddd-dddddddddddd', 'Quick Clean Service', 'Fast cleaning service for busy professionals.', 1, SYSUTCDATETIME(), SYSUTCDATETIME(), NULL, 0);
            ");

            // =========================
            // SERVICE PRICINGS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [ServiceOffer].[ServicePricing] ON;

                INSERT INTO [ServiceOffer].[ServicePricing] ([Id], [Price], [PricingModel], [ServiceOfferId])
                VALUES
                (1, 5000, 'Hourly', 1),
                (2, 15000, 'Fixed', 1),
                (3, 30000, 'Fixed', 2),
                (4, 6000, 'Hourly', 3),
                (5, 20000, 'Fixed', 4),
                (6, 50000, 'Fixed', 5),
                (7, 8000, 'Hourly', 5),
                (8, 10000, 'Fixed', 6);

                SET IDENTITY_INSERT [ServiceOffer].[ServicePricing] OFF;
            ");

            // =========================
            // CLEANER OFFERED SERVICES
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Cleaner].[CleanerOfferedService] ON;

                INSERT INTO [Cleaner].[CleanerOfferedService] ([Id], [OfferedServiceAggregateId], [CleanerId])
                VALUES
                (1, '11111111-aaaa-aaaa-aaaa-111111111111', 1),
                (2, '22222222-aaaa-aaaa-aaaa-222222222222', 1),
                (3, '33333333-bbbb-bbbb-bbbb-333333333333', 2),
                (4, '44444444-bbbb-bbbb-bbbb-444444444444', 2),
                (5, '55555555-cccc-cccc-cccc-555555555555', 3),
                (6, '66666666-dddd-dddd-dddd-666666666666', 4);

                SET IDENTITY_INSERT [Cleaner].[CleanerOfferedService] OFF;
            ");

            // =========================
            // BOOKINGS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[Booking] ON;

                INSERT INTO [Booking].[Booking]
                ([Id], [BookingAggregateId], [ServiceOfferAggregateId], [CustomerAggregateId],
                 [Status], [ScheduledDate], [CompletedDate], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived])
                VALUES
                (1, 'aaaaaaaa-1111-1111-1111-111111111111', '11111111-aaaa-aaaa-aaaa-111111111111', '11111111-1111-1111-1111-111111111111', 'Completed', DATEADD(day,-7,SYSUTCDATETIME()), DATEADD(day,-7,SYSUTCDATETIME()), DATEADD(day,-14,SYSUTCDATETIME()), DATEADD(day,-7,SYSUTCDATETIME()), NULL, 0),
                (2, 'bbbbbbbb-2222-2222-2222-222222222222', '33333333-bbbb-bbbb-bbbb-333333333333', '22222222-2222-2222-2222-222222222222', 'Completed', DATEADD(day,-5,SYSUTCDATETIME()), DATEADD(day,-5,SYSUTCDATETIME()), DATEADD(day,-10,SYSUTCDATETIME()), DATEADD(day,-5,SYSUTCDATETIME()), NULL, 0);

                SET IDENTITY_INSERT [Booking].[Booking] OFF;
            ");

            // =========================
            // SERVICE PRICING SNAPSHOTS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[ServicePricingSnapshot] ON;

                INSERT INTO [Booking].[ServicePricingSnapshot] ([Id], [Price], [PricingModel], [SnapshotDate], [BookingId])
                VALUES
                (1, 15000, 'Fixed', DATEADD(day,-14,SYSUTCDATETIME()), 1),
                (2, 6000, 'Hourly', DATEADD(day,-10,SYSUTCDATETIME()), 2);

                SET IDENTITY_INSERT [Booking].[ServicePricingSnapshot] OFF;
            ");

            // =========================
            // BOOKING REVIEWS
            // =========================
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[BookingReview] ON;

                INSERT INTO [Booking].[BookingReview] ([Id], [BookingId], [ReviewAggregateId], [Comment], [Rating], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived])
                VALUES
                (1, 1, 'aaaaaaaa-aaaa-aaaa-aaaa-111111111111', 'Excellent service! Highly recommend.', 5, DATEADD(day,-6,SYSUTCDATETIME()), DATEADD(day,-6,SYSUTCDATETIME()), NULL, 0),
                (2, 2, 'bbbbbbbb-bbbb-bbbb-bbbb-222222222222', 'Great eco-friendly cleaning service.', 5, DATEADD(day,-4,SYSUTCDATETIME()), DATEADD(day,-4,SYSUTCDATETIME()), NULL, 0);

                SET IDENTITY_INSERT [Booking].[BookingReview] OFF;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [Booking].[BookingReview];
                DELETE FROM [Booking].[ServicePricingSnapshot];
                DELETE FROM [Booking].[Booking];
                DELETE FROM [Cleaner].[CleanerOfferedService];
                DELETE FROM [ServiceOffer].[ServicePricing];
                DELETE FROM [ServiceOffer].[ServiceOffer];
                DELETE FROM [Cleaner].[Cleaner];
                DELETE FROM [Customer].[Customer];
            ");
        }
    }
}
