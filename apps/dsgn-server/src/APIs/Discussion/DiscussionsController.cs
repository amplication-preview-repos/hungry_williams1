using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[ApiController()]
public class DiscussionsController : DiscussionsControllerBase
{
    public DiscussionsController(IDiscussionsService service)
        : base(service) { }
}
