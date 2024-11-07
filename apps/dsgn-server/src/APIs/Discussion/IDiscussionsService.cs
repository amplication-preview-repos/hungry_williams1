using Dsgn.APIs.Common;
using Dsgn.APIs.Dtos;

namespace Dsgn.APIs;

public interface IDiscussionsService
{
    /// <summary>
    /// Create one Discussion
    /// </summary>
    public Task<Discussion> CreateDiscussion(DiscussionCreateInput discussion);

    /// <summary>
    /// Delete one Discussion
    /// </summary>
    public Task DeleteDiscussion(DiscussionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Discussions
    /// </summary>
    public Task<List<Discussion>> Discussions(DiscussionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Discussion records
    /// </summary>
    public Task<MetadataDto> DiscussionsMeta(DiscussionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Discussion
    /// </summary>
    public Task<Discussion> Discussion(DiscussionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Discussion
    /// </summary>
    public Task UpdateDiscussion(
        DiscussionWhereUniqueInput uniqueId,
        DiscussionUpdateInput updateDto
    );
}
