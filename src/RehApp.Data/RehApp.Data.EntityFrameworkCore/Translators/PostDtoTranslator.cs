using MapsterMapper;
using RehApp.Data.Common;
using RehApp.Data.Common.DTOs;
using RehApp.Data.EntityFrameworkCore.Translators.Base;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.Translators;

public class PostDtoTranslator : Translator<Post, PostDTO>
{
    public PostDtoTranslator(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public override async Task<PostDTO> BusinessToDTOAsync(Post entity)
    {
        var dto = mapper.Map<PostDTO>(entity);

        var videos = await GetDescriptionValues<PostDesc>(entity.Id, DescriptionTypeConst.Video);
        dto.VideoUrls = videos.Select(x => x.Value).ToList();

        var images = await GetDescriptionValues<PostDesc>(entity.Id, DescriptionTypeConst.Image);
        dto.ImageUrls = images.Select(x => x.Value).ToList();

        var audios = await GetDescriptionValues<PostDesc>(entity.Id, DescriptionTypeConst.Audio);
        dto.AudioUrls = audios.Select(x => x.Value).ToList();

        return dto;
    }
}
