using Domain.Aggregates.CleanerAggregate.ReadModels;
using Domain.Aggregates.CleanerAggregate.Repositories;
using Domain.Shared.Exceptions;
using MediatR;

namespace Application.Queries;

public class GetCleanerByAggregateId : IRequest<CleanerReadModel>
{
    public required Guid CleanerAggregateId { get; init; }
}

internal class GetCleanerByAggregateIdRequestHandler : IRequestHandler<GetCleanerByAggregateId, CleanerReadModel>
{
    private readonly ICleanerQueryStore _queryStore;

    public GetCleanerByAggregateIdRequestHandler(ICleanerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<CleanerReadModel> Handle(GetCleanerByAggregateId request, CancellationToken cancellationToken)
    {
        var cleanerReadModel = await _queryStore.GetCleanerByAggregateId(request.CleanerAggregateId, cancellationToken);
        if (cleanerReadModel == null) throw new EntityNotFoundException($"Cleaner with aggregate was not found");

        return cleanerReadModel;
    }
}
