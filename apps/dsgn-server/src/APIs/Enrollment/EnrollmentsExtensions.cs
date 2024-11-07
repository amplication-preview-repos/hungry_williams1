using Dsgn.APIs.Dtos;
using Dsgn.Infrastructure.Models;

namespace Dsgn.APIs.Extensions;

public static class EnrollmentsExtensions
{
    public static Enrollment ToDto(this EnrollmentDbModel model)
    {
        return new Enrollment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EnrollmentDbModel ToModel(
        this EnrollmentUpdateInput updateDto,
        EnrollmentWhereUniqueInput uniqueId
    )
    {
        var enrollment = new EnrollmentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            enrollment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            enrollment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return enrollment;
    }
}
