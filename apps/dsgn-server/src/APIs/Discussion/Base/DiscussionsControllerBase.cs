using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DiscussionsControllerBase : ControllerBase
{
    protected readonly IDiscussionsService _service;

    public DiscussionsControllerBase(IDiscussionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Discussion
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Discussion>> CreateDiscussion(DiscussionCreateInput input)
    {
        var discussion = await _service.CreateDiscussion(input);

        return CreatedAtAction(nameof(Discussion), new { id = discussion.Id }, discussion);
    }

    /// <summary>
    /// Delete one Discussion
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteDiscussion(
        [FromRoute()] DiscussionWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteDiscussion(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Discussions
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Discussion>>> Discussions(
        [FromQuery()] DiscussionFindManyArgs filter
    )
    {
        return Ok(await _service.Discussions(filter));
    }

    /// <summary>
    /// Meta data about Discussion records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DiscussionsMeta(
        [FromQuery()] DiscussionFindManyArgs filter
    )
    {
        return Ok(await _service.DiscussionsMeta(filter));
    }

    /// <summary>
    /// Get one Discussion
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Discussion>> Discussion(
        [FromRoute()] DiscussionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Discussion(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Discussion
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateDiscussion(
        [FromRoute()] DiscussionWhereUniqueInput uniqueId,
        [FromQuery()] DiscussionUpdateInput discussionUpdateDto
    )
    {
        try
        {
            await _service.UpdateDiscussion(uniqueId, discussionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
