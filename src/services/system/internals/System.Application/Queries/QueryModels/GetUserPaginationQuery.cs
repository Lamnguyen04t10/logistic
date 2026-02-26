using System.Application.ResponseModels;
using Core.Abstractions;
using MediatR;

namespace System.Application.Queries.QueryModels
{
    public sealed class GetUserPaginationQuery
        : PaginationRequestModel,
            IRequest<PaginationResponseModel<UserPaginationResponseModel>> { }
}
