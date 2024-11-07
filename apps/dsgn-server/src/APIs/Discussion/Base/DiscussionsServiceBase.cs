using Dsgn.APIs;
using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;
using Dsgn.APIs.Errors;
using Dsgn.APIs.Extensions;
using Dsgn.Infrastructure;
using Dsgn.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dsgn.APIs;

public abstract class DiscussionsServiceBase : IDiscussionsService
{
    protected readonly DsgnDbContext _context;

    public DiscussionsServiceBase(DsgnDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Discussion
    /// </summary>
    public async Task<Discussion> CreateDiscussion(DiscussionCreateInput createDto)
    {
        var discussion = new DiscussionDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            discussion.Id = createDto.Id;
        }

        _context.Discussions.Add(discussion);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DiscussionDbModel>(discussion.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Discussion
    /// </summary>
    public async Task DeleteDiscussion(DiscussionWhereUniqueInput uniqueId)
    {
        var discussion = await _context.Discussions.FindAsync(uniqueId.Id);
        if (discussion == null)
        {
            throw new NotFoundException();
        }

        _context.Discussions.Remove(discussion);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Discussions
    /// </summary>
    public async Task<List<Discussion>> Discussions(DiscussionFindManyArgs findManyArgs)
    {
        var discussions = await _context
            .Discussions.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return discussions.ConvertAll(discussion => discussion.ToDto());
    }

    /// <summary>
    /// Meta data about Discussion records
    /// </summary>
    public async Task<MetadataDto> DiscussionsMeta(DiscussionFindManyArgs findManyArgs)
    {
        var count = await _context.Discussions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Discussion
    /// </summary>
    public async Task<Discussion> Discussion(DiscussionWhereUniqueInput uniqueId)
    {
        var discussions = await this.Discussions(
            new DiscussionFindManyArgs { Where = new DiscussionWhereInput { Id = uniqueId.Id } }
        );
        var discussion = discussions.FirstOrDefault();
        if (discussion == null)
        {
            throw new NotFoundException();
        }

        return discussion;
    }

    /// <summary>
    /// Update one Discussion
    /// </summary>
    public async Task UpdateDiscussion(
        DiscussionWhereUniqueInput uniqueId,
        DiscussionUpdateInput updateDto
    )
    {
        var discussion = updateDto.ToModel(uniqueId);

        _context.Entry(discussion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Discussions.Any(e => e.Id == discussion.Id))
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
