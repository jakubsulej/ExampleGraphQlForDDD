using Application.Queries;
using MediatR;

namespace WebApi.Graph;

public class Query
{
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
}
