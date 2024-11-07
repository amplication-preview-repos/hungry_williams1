using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Dsgn.APIs.Extensions;
using Dsgn.Infrastructure;
using Dsgn.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dsgn.APIs;

public abstract class LessonsServiceBase : ILessonsService
{
    protected readonly DsgnDbContext _context;

    public LessonsServiceBase(DsgnDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Lesson
    /// </summary>
    public async Task<Lesson> CreateLesson(LessonCreateInput createDto)
    {
        var lesson = new LessonDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            lesson.Id = createDto.Id;
        }

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<LessonDbModel>(lesson.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Lesson
    /// </summary>
    public async Task DeleteLesson(LessonWhereUniqueInput uniqueId)
    {
        var lesson = await _context.Lessons.FindAsync(uniqueId.Id);
        if (lesson == null)
        {
            throw new NotFoundException();
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Lessons
    /// </summary>
    public async Task<List<Lesson>> Lessons(LessonFindManyArgs findManyArgs)
    {
        var lessons = await _context
            .Lessons.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return lessons.ConvertAll(lesson => lesson.ToDto());
    }

    /// <summary>
    /// Meta data about Lesson records
    /// </summary>
    public async Task<MetadataDto> LessonsMeta(LessonFindManyArgs findManyArgs)
    {
        var count = await _context.Lessons.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Lesson
    /// </summary>
    public async Task<Lesson> Lesson(LessonWhereUniqueInput uniqueId)
    {
        var lessons = await this.Lessons(
            new LessonFindManyArgs { Where = new LessonWhereInput { Id = uniqueId.Id } }
        );
        var lesson = lessons.FirstOrDefault();
        if (lesson == null)
        {
            throw new NotFoundException();
        }

        return lesson;
    }

    /// <summary>
    /// Update one Lesson
    /// </summary>
    public async Task UpdateLesson(LessonWhereUniqueInput uniqueId, LessonUpdateInput updateDto)
    {
        var lesson = updateDto.ToModel(uniqueId);

        _context.Entry(lesson).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Lessons.Any(e => e.Id == lesson.Id))
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
