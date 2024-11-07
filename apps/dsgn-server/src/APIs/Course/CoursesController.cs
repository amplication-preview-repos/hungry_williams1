using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[ApiController()]
public class CoursesController : CoursesControllerBase
{
    public CoursesController(ICoursesService service)
        : base(service) { }
}
