using Core.Abstractions;

namespace System.Application.ResponseModels
{
    public sealed class UserPaginationResponseModel : IResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; }
    }
}
