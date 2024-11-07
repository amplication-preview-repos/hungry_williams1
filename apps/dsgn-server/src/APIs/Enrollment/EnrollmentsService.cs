using Dsgn.Infrastructure;

namespace Dsgn.APIs;

public class EnrollmentsService : EnrollmentsServiceBase
{
    public EnrollmentsService(DsgnDbContext context)
        : base(context) { }
}
