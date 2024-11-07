using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CoursesControllerBase : ControllerBase
{
    protected readonly ICoursesService _service;

    public CoursesControllerBase(ICoursesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Course
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Course>> CreateCourse(CourseCreateInput input)
    {
        var course = await _service.CreateCourse(input);

        return CreatedAtAction(nameof(Course), new { id = course.Id }, course);
    }

    /// <summary>
    /// Delete one Course
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCourse([FromRoute()] CourseWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCourse(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Courses
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Course>>> Courses([FromQuery()] CourseFindManyArgs filter)
    {
        return Ok(await _service.Courses(filter));
    }

    /// <summary>
    /// Meta data about Course records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CoursesMeta(
        [FromQuery()] CourseFindManyArgs filter
    )
    {
        return Ok(await _service.CoursesMeta(filter));
    }

    /// <summary>
    /// Get one Course
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Course>> Course([FromRoute()] CourseWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Course(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Course
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCourse(
        [FromRoute()] CourseWhereUniqueInput uniqueId,
        [FromQuery()] CourseUpdateInput courseUpdateDto
    )
    {
        try
        {
            await _service.UpdateCourse(uniqueId, courseUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
