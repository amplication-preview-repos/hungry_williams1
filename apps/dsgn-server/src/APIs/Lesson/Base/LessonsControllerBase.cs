using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LessonsControllerBase : ControllerBase
{
    protected readonly ILessonsService _service;

    public LessonsControllerBase(ILessonsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Lesson
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Lesson>> CreateLesson(LessonCreateInput input)
    {
        var lesson = await _service.CreateLesson(input);

        return CreatedAtAction(nameof(Lesson), new { id = lesson.Id }, lesson);
    }

    /// <summary>
    /// Delete one Lesson
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteLesson([FromRoute()] LessonWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteLesson(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Lessons
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Lesson>>> Lessons([FromQuery()] LessonFindManyArgs filter)
    {
        return Ok(await _service.Lessons(filter));
    }

    /// <summary>
    /// Meta data about Lesson records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LessonsMeta(
        [FromQuery()] LessonFindManyArgs filter
    )
    {
        return Ok(await _service.LessonsMeta(filter));
    }

    /// <summary>
    /// Get one Lesson
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Lesson>> Lesson([FromRoute()] LessonWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Lesson(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Lesson
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateLesson(
        [FromRoute()] LessonWhereUniqueInput uniqueId,
        [FromQuery()] LessonUpdateInput lessonUpdateDto
    )
    {
        try
        {
            await _service.UpdateLesson(uniqueId, lessonUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
