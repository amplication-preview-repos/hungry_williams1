using Dsgn.APIs.Dtos;
using Dsgn.Infrastructure.Models;

namespace Dsgn.APIs.Extensions;

public static class DiscussionsExtensions
{
    public static Discussion ToDto(this DiscussionDbModel model)
    {
        return new Discussion
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DiscussionDbModel ToModel(
        this DiscussionUpdateInput updateDto,
        DiscussionWhereUniqueInput uniqueId
    )
    {
        var discussion = new DiscussionDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            discussion.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            discussion.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return discussion;
    }
}
