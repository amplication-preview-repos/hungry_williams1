using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[ApiController()]
public class EnrollmentsController : EnrollmentsControllerBase
{
    public EnrollmentsController(IEnrollmentsService service)
        : base(service) { }
}
