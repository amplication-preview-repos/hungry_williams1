using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;

namespace Dsgn.APIs;

public interface ICoursesService
{
    /// <summary>
    /// Create one Course
    /// </summary>
    public Task<Course> CreateCourse(CourseCreateInput course);

    /// <summary>
    /// Delete one Course
    /// </summary>
    public Task DeleteCourse(CourseWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Courses
    /// </summary>
    public Task<List<Course>> Courses(CourseFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Course records
    /// </summary>
    public Task<MetadataDto> CoursesMeta(CourseFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Course
    /// </summary>
    public Task<Course> Course(CourseWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Course
    /// </summary>
    public Task UpdateCourse(CourseWhereUniqueInput uniqueId, CourseUpdateInput updateDto);
}
