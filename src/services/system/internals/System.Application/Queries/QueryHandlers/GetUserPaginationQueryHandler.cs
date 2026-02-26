using System.Application.Queries.QueryModels;
using System.Application.ResponseModels;
using System.Data;
using System.Domain.Abstractions;
using Core.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace System.Application.Queries.QueryHandlers
{
    public sealed class GetUserPaginationQueryHandler(IUserRepository userRepository)
        : IRequestHandler<
            GetUserPaginationQuery,
            PaginationResponseModel<UserPaginationResponseModel>
        >,
            IMediatRHandler
    {
        public async Task<PaginationResponseModel<UserPaginationResponseModel>> Handle(
            GetUserPaginationQuery request,
            CancellationToken cancellationToken
        )
        {
            var query = userRepository
                .GetAllAsQueryable()
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new UserPaginationResponseModel { UserName = x.UserName });

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword));
            }

            var total = await query.CountAsync(cancellationToken);

            var users = await query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);
            return new PaginationResponseModel<UserPaginationResponseModel>(
                request.PageIndex,
                request.PageSize,
                total,
                users
            );
        }
    }
}
