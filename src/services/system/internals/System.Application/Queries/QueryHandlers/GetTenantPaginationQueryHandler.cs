using System.Application.Queries.QueryModels;
using System.Application.ResponseModels;
using System.Domain.Abstractions;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace System.Application.Queries.QueryHandlers
{
    public class GetTenantPaginationQueryHandler(ITenantRepository tenantRepository)
        : IRequestHandler<
            GetTenantPaginationQuery,
            PaginationResponseModel<TenantPaginationResponseModel>
        >
    {
        public async Task<PaginationResponseModel<TenantPaginationResponseModel>> Handle(
            GetTenantPaginationQuery request,
            CancellationToken cancellationToken
        )
        {
            var query = tenantRepository
                .GetAllAsQueryable()
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new TenantPaginationResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                });

            var total = await query.CountAsync(cancellationToken);

            var data = await query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

            return new PaginationResponseModel<TenantPaginationResponseModel>(
                request.PageIndex,
                request.PageSize,
                total,
                data
            );
        }
    }
}
