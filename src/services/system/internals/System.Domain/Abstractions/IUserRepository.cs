using System.Domain.Entities.UserAgr;
using Core.Abstractions;

namespace System.Domain.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByUserNameAsync(string userName);
    }
}
