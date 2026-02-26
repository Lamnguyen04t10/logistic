using System.Application.ResponseModels;
using Core.Abstractions;
using MediatR;

namespace System.Application.Queries.QueryModels
{
    public sealed class GetTenantPaginationQuery
        : PaginationRequestModel,
            IRequest<PaginationResponseModel<TenantPaginationResponseModel>> { }
}
