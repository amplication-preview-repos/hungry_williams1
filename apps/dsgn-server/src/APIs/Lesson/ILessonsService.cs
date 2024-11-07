using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;

namespace Dsgn.APIs;

public interface ILessonsService
{
    /// <summary>
    /// Create one Lesson
    /// </summary>
    public Task<Lesson> CreateLesson(LessonCreateInput lesson);

    /// <summary>
    /// Delete one Lesson
    /// </summary>
    public Task DeleteLesson(LessonWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Lessons
    /// </summary>
    public Task<List<Lesson>> Lessons(LessonFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Lesson records
    /// </summary>
    public Task<MetadataDto> LessonsMeta(LessonFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Lesson
    /// </summary>
    public Task<Lesson> Lesson(LessonWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Lesson
    /// </summary>
    public Task UpdateLesson(LessonWhereUniqueInput uniqueId, LessonUpdateInput updateDto);
}
