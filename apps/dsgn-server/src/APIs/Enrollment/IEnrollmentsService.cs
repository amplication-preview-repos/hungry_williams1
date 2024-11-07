using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;

namespace Dsgn.APIs;

public interface IEnrollmentsService
{
    /// <summary>
    /// Create one Enrollment
    /// </summary>
    public Task<Enrollment> CreateEnrollment(EnrollmentCreateInput enrollment);

    /// <summary>
    /// Delete one Enrollment
    /// </summary>
    public Task DeleteEnrollment(EnrollmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Enrollments
    /// </summary>
    public Task<List<Enrollment>> Enrollments(EnrollmentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Enrollment records
    /// </summary>
    public Task<MetadataDto> EnrollmentsMeta(EnrollmentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Enrollment
    /// </summary>
    public Task<Enrollment> Enrollment(EnrollmentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Enrollment
    /// </summary>
    public Task UpdateEnrollment(
        EnrollmentWhereUniqueInput uniqueId,
        EnrollmentUpdateInput updateDto
    );
}
