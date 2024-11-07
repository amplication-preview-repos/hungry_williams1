using Dsgn.APIs.Dtos;
using Dsgn.Infrastructure.Models;

namespace Dsgn.APIs.Extensions;

public static class CoursesExtensions
{
    public static Course ToDto(this CourseDbModel model)
    {
        return new Course
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CourseDbModel ToModel(
        this CourseUpdateInput updateDto,
        CourseWhereUniqueInput uniqueId
    )
    {
        var course = new CourseDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            course.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            course.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return course;
    }
}
