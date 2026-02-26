using System.Domain.Entities.TenantAgr;
using Core.Abstractions;

namespace System.Application.ResponseModels
{
    public sealed class TenantPaginationResponseModel : IResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TenanType Type { get; set; }
    }
}
