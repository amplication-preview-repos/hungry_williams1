using Dsgn.Infrastructure;

namespace Dsgn.APIs;

public class LessonsService : LessonsServiceBase
{
    public LessonsService(DsgnDbContext context)
        : base(context) { }
}
