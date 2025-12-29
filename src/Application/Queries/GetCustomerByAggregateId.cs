using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.CustomerAggregate.Repositories;
using Domain.Shared.Exceptions;
using MediatR;

namespace Application.Queries;

public class GetCustomerByAggregateId : IRequest<CustomerReadModel>
{
    public required Guid CustomerAggregateId { get; init; }
}

internal class GetCustomerByAggregateIdRequestHandler : IRequestHandler<GetCustomerByAggregateId, CustomerReadModel>
{
    private readonly ICustomerQueryStore _queryStore;

    public GetCustomerByAggregateIdRequestHandler(ICustomerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<CustomerReadModel> Handle(GetCustomerByAggregateId request, CancellationToken cancellationToken)
    {
        var customerReadModel = await _queryStore.GetCustomerByAggregateId(request.CustomerAggregateId, cancellationToken);
        if (customerReadModel == null) 
            throw new EntityNotFoundException($"Customer with aggregate ID {request.CustomerAggregateId} was not found");

        return customerReadModel;
    }
}

