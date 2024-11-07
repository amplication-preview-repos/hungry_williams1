using Dsgn.Infrastructure;

namespace Dsgn.APIs;

public class CoursesService : CoursesServiceBase
{
    public CoursesService(DsgnDbContext context)
        : base(context) { }
}
