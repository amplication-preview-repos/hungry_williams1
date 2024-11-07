using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EnrollmentsControllerBase : ControllerBase
{
    protected readonly IEnrollmentsService _service;

    public EnrollmentsControllerBase(IEnrollmentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Enrollment
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Enrollment>> CreateEnrollment(EnrollmentCreateInput input)
    {
        var enrollment = await _service.CreateEnrollment(input);

        return CreatedAtAction(nameof(Enrollment), new { id = enrollment.Id }, enrollment);
    }

    /// <summary>
    /// Delete one Enrollment
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteEnrollment(
        [FromRoute()] EnrollmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteEnrollment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Enrollments
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Enrollment>>> Enrollments(
        [FromQuery()] EnrollmentFindManyArgs filter
    )
    {
        return Ok(await _service.Enrollments(filter));
    }

    /// <summary>
    /// Meta data about Enrollment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EnrollmentsMeta(
        [FromQuery()] EnrollmentFindManyArgs filter
    )
    {
        return Ok(await _service.EnrollmentsMeta(filter));
    }

    /// <summary>
    /// Get one Enrollment
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Enrollment>> Enrollment(
        [FromRoute()] EnrollmentWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Enrollment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Enrollment
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateEnrollment(
        [FromRoute()] EnrollmentWhereUniqueInput uniqueId,
        [FromQuery()] EnrollmentUpdateInput enrollmentUpdateDto
    )
    {
        try
        {
            await _service.UpdateEnrollment(uniqueId, enrollmentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
