using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.CustomerAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public record GetCustomersPage : IRequest<GetCustomersPageResponse>
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}

public record GetCustomersPageResponse
{
    public required IEnumerable<CustomerReadModel> Customers { get; init; }
    public required int TotalCount { get; init; }
}

internal class GetCustomersPageRequestHandler : IRequestHandler<GetCustomersPage, GetCustomersPageResponse>
{
    private readonly ICustomerQueryStore _queryStore;

    public GetCustomersPageRequestHandler(ICustomerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<GetCustomersPageResponse> Handle(GetCustomersPage request, CancellationToken cancellationToken)
    {
        var customers = await _queryStore.GetCustomers(request.Page, request.PageSize, cancellationToken);
        var customersCount = await _queryStore.GetCustomersCount(cancellationToken);

        return new GetCustomersPageResponse
        {
            Customers = customers,
            TotalCount = customersCount,
        };
    }
}

