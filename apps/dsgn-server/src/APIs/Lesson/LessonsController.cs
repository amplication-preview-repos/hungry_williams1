using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[ApiController()]
public class LessonsController : LessonsControllerBase
{
    public LessonsController(ILessonsService service)
        : base(service) { }
}
