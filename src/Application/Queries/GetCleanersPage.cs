using Domain.Aggregates.CleanerAggregate.ReadModels;
using Domain.Aggregates.CleanerAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetCleanersPage : IRequest<GetCleanersPageResponse>
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}

public record GetCleanersPageResponse
{
    public required IEnumerable<CleanerReadModel> Cleaners { get; init; }
    public required int TotalCount { get; init; }
}

internal class GetCleanersPageRequestHandler : IRequestHandler<GetCleanersPage, GetCleanersPageResponse>
{
    private readonly ICleanerQueryStore _queryStore;

    public GetCleanersPageRequestHandler(ICleanerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<GetCleanersPageResponse> Handle(GetCleanersPage request, CancellationToken cancellationToken)
    {
        var cleaners = await _queryStore.GetCleaners(request.Page, request.PageSize, cancellationToken);
        var cleanersCount = await _queryStore.GetCleanersCount(cancellationToken);

        return new GetCleanersPageResponse
        {
            Cleaners = cleaners,
            TotalCount = cleanersCount,
        };
    }
}
