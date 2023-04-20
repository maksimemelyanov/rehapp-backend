using RehApp.Data.Common;
using RehApp.Data.Common.DTOs;
using RehApp.Data.EntityFrameworkCore.Translators.Base;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.Translators;

public class PostDtoTranslator : Translator<Post, PostDTO>
{
    public PostDtoTranslator(ApplicationDbContext context) : base(context) { }

    public override async Task<PostDTO> BusinessToDTOAsync(Post entity)
    {
        var getDescValues = async (long descTypeCode) => await GetDescriptionValues<PostDesc>(
            parentId: entity.Id,
            descriptionTypeCode: descTypeCode,
            orderBy: (x) => entity.Date);

        var audios = await getDescValues(DescriptionTypeConst.Audio);
        var images = await getDescValues(DescriptionTypeConst.Image);
        var videos = await getDescValues(DescriptionTypeConst.Video);

        var dto = new PostDTO
        {
            Date = entity.Date,
            Text = entity.Text,
            AudioUrls = audios.Select(x => x.Value).ToList(),
            VideoUrls = videos.Select(x => x.Value).ToList(),
            ImageUrls = images.Select(x => x.Value).ToList(),
        };

        return dto;
    }
}
