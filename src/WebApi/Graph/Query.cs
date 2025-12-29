using Application.Queries;
using HotChocolate;
using MediatR;

namespace WebApi.Graph;

public class Query
{
    [GraphQLName("serviceOffersPage")]
    public Task<GetServiceOffersPageResponse> GetServiceOffersPage(
        int page,
        int pageSize,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetServiceOffersPage
        {
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

    [GraphQLName("cleanersPage")]
    public Task<GetCleanersPageResponse> GetCleanersPage(
        int page,
        int pageSize,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetCleanersPage
        {
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

    [GraphQLName("bookingsPage")]
    public Task<GetBookingsPageResponse> GetBookingsPage(
        int page,
        int pageSize,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetBookingsPage
        {
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

    [GraphQLName("customersPage")]
    public Task<GetCustomersPageResponse> GetCustomersPage(
        int page,
        int pageSize,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetCustomersPage
        {
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

    [GraphQLName("cleaner")]
    public Task<Domain.Aggregates.CleanerAggregate.ReadModels.CleanerReadModel> GetCleaner(
        Guid cleanerAggregateId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetCleanerByAggregateId
        {
            CleanerAggregateId = cleanerAggregateId
        }, cancellationToken);

    [GraphQLName("serviceOffer")]
    public Task<Domain.Aggregates.ServiceOfferAggregate.ReadModels.ServiceOfferReadModel> GetServiceOffer(
        Guid serviceOfferAggregateId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetServiceOfferByAggregateId
        {
            ServiceOfferAggregateId = serviceOfferAggregateId
        }, cancellationToken);

    [GraphQLName("booking")]
    public Task<Domain.Aggregates.BookingAggregate.ReadModels.BookingReadModel> GetBooking(
        Guid bookingAggregateId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetBookingByAggregateId
        {
            BookingAggregateId = bookingAggregateId
        }, cancellationToken);

    [GraphQLName("customer")]
    public Task<Domain.Aggregates.CustomerAggregate.ReadModels.CustomerReadModel> GetCustomer(
        Guid customerAggregateId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetCustomerByAggregateId
        {
            CustomerAggregateId = customerAggregateId
        }, cancellationToken);
}
