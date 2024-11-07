using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Dsgn.APIs.Extensions;
using Dsgn.Infrastructure;
using Dsgn.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dsgn.APIs;

public abstract class EnrollmentsServiceBase : IEnrollmentsService
{
    protected readonly DsgnDbContext _context;

    public EnrollmentsServiceBase(DsgnDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Enrollment
    /// </summary>
    public async Task<Enrollment> CreateEnrollment(EnrollmentCreateInput createDto)
    {
        var enrollment = new EnrollmentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            enrollment.Id = createDto.Id;
        }

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EnrollmentDbModel>(enrollment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Enrollment
    /// </summary>
    public async Task DeleteEnrollment(EnrollmentWhereUniqueInput uniqueId)
    {
        var enrollment = await _context.Enrollments.FindAsync(uniqueId.Id);
        if (enrollment == null)
        {
            throw new NotFoundException();
        }

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Enrollments
    /// </summary>
    public async Task<List<Enrollment>> Enrollments(EnrollmentFindManyArgs findManyArgs)
    {
        var enrollments = await _context
            .Enrollments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return enrollments.ConvertAll(enrollment => enrollment.ToDto());
    }

    /// <summary>
    /// Meta data about Enrollment records
    /// </summary>
    public async Task<MetadataDto> EnrollmentsMeta(EnrollmentFindManyArgs findManyArgs)
    {
        var count = await _context.Enrollments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Enrollment
    /// </summary>
    public async Task<Enrollment> Enrollment(EnrollmentWhereUniqueInput uniqueId)
    {
        var enrollments = await this.Enrollments(
            new EnrollmentFindManyArgs { Where = new EnrollmentWhereInput { Id = uniqueId.Id } }
        );
        var enrollment = enrollments.FirstOrDefault();
        if (enrollment == null)
        {
            throw new NotFoundException();
        }

        return enrollment;
    }

    /// <summary>
    /// Update one Enrollment
    /// </summary>
    public async Task UpdateEnrollment(
        EnrollmentWhereUniqueInput uniqueId,
        EnrollmentUpdateInput updateDto
    )
    {
        var enrollment = updateDto.ToModel(uniqueId);

        _context.Entry(enrollment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Enrollments.Any(e => e.Id == enrollment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
