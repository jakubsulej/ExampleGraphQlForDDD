using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DummyDataSeed : Migration
    {
                protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Customers
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Customer].[Customer] ON;
                
                INSERT INTO [Customer].[Customer] (
                    [Id], [CustomerAggregateId], [Name], [Email], [PhoneNumber], [Address], 
                    [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived]
                )
                VALUES
                    (1, '11111111-1111-1111-1111-111111111111', 'John Smith', 'john.smith@example.com', '+1-555-0101', '123 Main St, New York, NY 10001', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (2, '22222222-2222-2222-2222-222222222222', 'Emily Johnson', 'emily.johnson@example.com', '+1-555-0102', '456 Oak Ave, Los Angeles, CA 90001', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (3, '33333333-3333-3333-3333-333333333333', 'Michael Brown', 'michael.brown@example.com', '+1-555-0103', '789 Pine Rd, Chicago, IL 60601', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (4, '44444444-4444-4444-4444-444444444444', 'Sarah Davis', 'sarah.davis@example.com', '+1-555-0104', '321 Elm St, Houston, TX 77001', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (5, '55555555-5555-5555-5555-555555555555', 'David Wilson', 'david.wilson@example.com', '+1-555-0105', '654 Maple Dr, Phoenix, AZ 85001', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0);
                
                SET IDENTITY_INSERT [Customer].[Customer] OFF;
            ");

            // Insert Cleaners
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Cleaner].[Cleaner] ON;
                
                INSERT INTO [Cleaner].[Cleaner] (
                    [Id], [CleanerAggregateId], [Name], [Description], [PhoneNumber], [Email], 
                    [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived]
                )
                VALUES
                    (1, 'AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA', 'Sparkle Clean Services', 'Professional cleaning service with 10+ years of experience. Specializing in residential and commercial cleaning.', '+1-555-1001', 'info@sparkleclean.com', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (2, 'BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB', 'EcoClean Solutions', 'Eco-friendly cleaning services using only green products. Certified and insured.', '+1-555-1002', 'contact@ecoclean.com', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (3, 'CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC', 'Premium Housekeeping', 'Luxury housekeeping services for high-end homes and offices. Attention to detail guaranteed.', '+1-555-1003', 'hello@premiumhousekeeping.com', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (4, 'DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD', 'Quick Clean Express', 'Fast and reliable cleaning services. Same-day service available.', '+1-555-1004', 'service@quickclean.com', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0);
                
                SET IDENTITY_INSERT [Cleaner].[Cleaner] OFF;
            ");

            // Insert Service Offers
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [ServiceOffer].[ServiceOffer] ON;
                
                INSERT INTO [ServiceOffer].[ServiceOffer] (
                    [Id], [ServiceOfferAggregateId], [CleanerAggregateId], [Title], [Description], 
                    [IsActive], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived]
                )
                VALUES
                    (1, 'SO111111-1111-1111-1111-111111111111', 'AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA', 'Standard House Cleaning', 'Complete house cleaning including all rooms, bathrooms, kitchen, and living areas. Dusting, vacuuming, mopping, and sanitizing included.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (2, 'SO222222-2222-2222-2222-222222222222', 'AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA', 'Deep Cleaning Service', 'Intensive deep cleaning service for move-in/move-out or seasonal cleaning. Includes inside cabinets, behind appliances, and detailed scrubbing.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (3, 'SO333333-3333-3333-3333-333333333333', 'BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB', 'Eco-Friendly Office Cleaning', 'Green cleaning service for offices and commercial spaces. Uses only environmentally safe products.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (4, 'SO444444-4444-4444-4444-444444444444', 'BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB', 'Window Cleaning', 'Professional window cleaning service for residential and commercial properties. Includes interior and exterior cleaning.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (5, 'SO555555-5555-5555-5555-555555555555', 'CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC', 'Luxury Home Cleaning', 'Premium cleaning service for luxury homes. Includes detailed attention to high-end finishes and materials.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (6, 'SO666666-6666-6666-6666-666666666666', 'DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD', 'Quick Clean Service', 'Fast cleaning service for busy professionals. Basic cleaning completed in 2 hours.', 1, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0);
                
                SET IDENTITY_INSERT [ServiceOffer].[ServiceOffer] OFF;
            ");

            // Insert Service Pricings
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [ServiceOffer].[ServicePricing] ON;
                
                INSERT INTO [ServiceOffer].[ServicePricing] ([Id], [Price], [PricingModel], [ServiceOfferId])
                VALUES
                    (1, 5000, 'Hourly', 1),   -- $50.00 per hour
                    (2, 15000, 'Fixed', 1),  -- $150.00 fixed price
                    (3, 30000, 'Fixed', 2),  -- $300.00 fixed price
                    (4, 6000, 'Hourly', 3),  -- $60.00 per hour
                    (5, 20000, 'Fixed', 4),  -- $200.00 fixed price
                    (6, 50000, 'Fixed', 5),  -- $500.00 fixed price
                    (7, 8000, 'Hourly', 5),  -- $80.00 per hour
                    (8, 10000, 'Fixed', 6);  -- $100.00 fixed price
                
                SET IDENTITY_INSERT [ServiceOffer].[ServicePricing] OFF;
            ");

            // Insert Cleaner Offered Services
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Cleaner].[CleanerOfferedService] ON;
                
                INSERT INTO [Cleaner].[CleanerOfferedService] ([Id], [OfferedServiceAggregateId], [CleanerId])
                VALUES
                    (1, 'SO111111-1111-1111-1111-111111111111', 1),
                    (2, 'SO222222-2222-2222-2222-222222222222', 1),
                    (3, 'SO333333-3333-3333-3333-333333333333', 2),
                    (4, 'SO444444-4444-4444-4444-444444444444', 2),
                    (5, 'SO555555-5555-5555-5555-555555555555', 3),
                    (6, 'SO666666-6666-6666-6666-666666666666', 4);
                
                SET IDENTITY_INSERT [Cleaner].[CleanerOfferedService] OFF;
            ");

            // Insert Bookings
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[Booking] ON;
                
                INSERT INTO [Booking].[Booking] (
                    [Id], [BookingAggregateId], [ServiceOfferAggregateId], [CustomerAggregateId], 
                    [Status], [ScheduledDate], [CompletedDate], [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived]
                )
                VALUES
                    (1, 'B1111111-1111-1111-1111-111111111111', 'SO111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111', 'Completed', DATEADD(day, -7, GETUTCDATE()), DATEADD(day, -7, GETUTCDATE()), DATEADD(day, -14, GETUTCDATE()), DATEADD(day, -7, GETUTCDATE()), '0001-01-01', 0),
                    (2, 'B2222222-2222-2222-2222-222222222222', 'SO333333-3333-3333-3333-333333333333', '22222222-2222-2222-2222-222222222222', 'Completed', DATEADD(day, -5, GETUTCDATE()), DATEADD(day, -5, GETUTCDATE()), DATEADD(day, -10, GETUTCDATE()), DATEADD(day, -5, GETUTCDATE()), '0001-01-01', 0),
                    (3, 'B3333333-3333-3333-3333-333333333333', 'SO555555-5555-5555-5555-555555555555', '33333333-3333-3333-3333-333333333333', 'Completed', DATEADD(day, -3, GETUTCDATE()), DATEADD(day, -3, GETUTCDATE()), DATEADD(day, -8, GETUTCDATE()), DATEADD(day, -3, GETUTCDATE()), '0001-01-01', 0),
                    (4, 'B4444444-4444-4444-4444-444444444444', 'SO222222-2222-2222-2222-222222222222', '44444444-4444-4444-4444-444444444444', 'Confirmed', DATEADD(day, 3, GETUTCDATE()), NULL, DATEADD(day, -2, GETUTCDATE()), GETUTCDATE(), '0001-01-01', 0),
                    (5, 'B5555555-5555-5555-5555-555555555555', 'SO444444-4444-4444-4444-444444444444', '11111111-1111-1111-1111-111111111111', 'Confirmed', DATEADD(day, 5, GETUTCDATE()), NULL, DATEADD(day, -1, GETUTCDATE()), GETUTCDATE(), '0001-01-01', 0),
                    (6, 'B6666666-6666-6666-6666-666666666666', 'SO666666-6666-6666-6666-666666666666', '55555555-5555-5555-5555-555555555555', 'InProgress', GETUTCDATE(), NULL, DATEADD(day, -1, GETUTCDATE()), GETUTCDATE(), '0001-01-01', 0),
                    (7, 'B7777777-7777-7777-7777-777777777777', 'SO111111-1111-1111-1111-111111111111', '22222222-2222-2222-2222-222222222222', 'Pending', DATEADD(day, 7, GETUTCDATE()), NULL, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0),
                    (8, 'B8888888-8888-8888-8888-888888888888', 'SO333333-3333-3333-3333-333333333333', '33333333-3333-3333-3333-333333333333', 'Pending', DATEADD(day, 10, GETUTCDATE()), NULL, GETUTCDATE(), GETUTCDATE(), '0001-01-01', 0);
                
                SET IDENTITY_INSERT [Booking].[Booking] OFF;
            ");

            // Insert Service Pricing Snapshots
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[ServicePricingSnapshot] ON;
                
                INSERT INTO [Booking].[ServicePricingSnapshot] ([Id], [Price], [PricingModel], [SnapshotDate], [BookingId])
                VALUES
                    (1, 15000, 'Fixed', DATEADD(day, -14, GETUTCDATE()), 1),
                    (2, 6000, 'Hourly', DATEADD(day, -10, GETUTCDATE()), 2),
                    (3, 50000, 'Fixed', DATEADD(day, -8, GETUTCDATE()), 3),
                    (4, 30000, 'Fixed', DATEADD(day, -2, GETUTCDATE()), 4),
                    (5, 20000, 'Fixed', DATEADD(day, -1, GETUTCDATE()), 5),
                    (6, 10000, 'Fixed', DATEADD(day, -1, GETUTCDATE()), 6),
                    (7, 5000, 'Hourly', GETUTCDATE(), 7),
                    (8, 6000, 'Hourly', GETUTCDATE(), 8);
                
                SET IDENTITY_INSERT [Booking].[ServicePricingSnapshot] OFF;
            ");

            // Insert Booking Reviews
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Booking].[BookingReview] ON;
                
                INSERT INTO [Booking].[BookingReview] (
                    [Id], [BookingId], [ReviewAggregateId], [Comment], [Rating], 
                    [CreatedAt], [UpdatedAt], [ArchivedAt], [IsArchived]
                )
                VALUES
                    (1, 1, 'R1111111-1111-1111-1111-111111111111', 'Excellent service! The team was professional, thorough, and left my house sparkling clean. Highly recommend!', 5, DATEADD(day, -6, GETUTCDATE()), DATEADD(day, -6, GETUTCDATE()), '0001-01-01', 0),
                    (2, 2, 'R2222222-2222-2222-2222-222222222222', 'Great eco-friendly cleaning service. The office looks amazing and I appreciate the use of green products.', 5, DATEADD(day, -4, GETUTCDATE()), DATEADD(day, -4, GETUTCDATE()), '0001-01-01', 0),
                    (3, 3, 'R3333333-3333-3333-3333-333333333333', 'Premium service lived up to its name. Every detail was perfect. Worth every penny!', 5, DATEADD(day, -2, GETUTCDATE()), DATEADD(day, -2, GETUTCDATE()), '0001-01-01', 0);
                
                SET IDENTITY_INSERT [Booking].[BookingReview] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seed data
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
