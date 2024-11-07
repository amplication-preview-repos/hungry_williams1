using Dsgn.Infrastructure;

namespace Dsgn.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(DsgnDbContext context)
        : base(context) { }
}
