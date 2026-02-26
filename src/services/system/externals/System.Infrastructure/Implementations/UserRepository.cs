using System.Domain.Abstractions;
using System.Domain.Entities.UserAgr;
using Microsoft.EntityFrameworkCore;

namespace System.Infrastructure.Implementations
{
    public class UserRepository(ApplicationDbContext context)
        : Repository<User>(context),
            IUserRepository
    {
        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await context.Entity<User>().FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
