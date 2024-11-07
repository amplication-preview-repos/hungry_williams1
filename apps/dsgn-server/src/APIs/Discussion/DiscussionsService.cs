using Dsgn.Infrastructure;

namespace Dsgn.APIs;

public class DiscussionsService : DiscussionsServiceBase
{
    public DiscussionsService(DsgnDbContext context)
        : base(context) { }
}
