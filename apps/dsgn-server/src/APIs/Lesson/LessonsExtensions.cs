using Dsgn.APIs.Dtos;
using Dsgn.Infrastructure.Models;

namespace Dsgn.APIs.Extensions;

public static class LessonsExtensions
{
    public static Lesson ToDto(this LessonDbModel model)
    {
        return new Lesson
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static LessonDbModel ToModel(
        this LessonUpdateInput updateDto,
        LessonWhereUniqueInput uniqueId
    )
    {
        var lesson = new LessonDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            lesson.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            lesson.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return lesson;
    }
}
