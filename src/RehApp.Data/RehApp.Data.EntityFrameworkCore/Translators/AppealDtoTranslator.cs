using MapsterMapper;
using RehApp.Data.Common;
using RehApp.Data.Common.DTOs;
using RehApp.Data.EntityFrameworkCore.Translators.Base;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.Translators;

public class AppealDtoTranslator : Translator<Appeal, AppealDTO>
{
    public AppealDtoTranslator(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public override async Task<AppealDTO> BusinessToDTOAsync(Appeal entity)
    {
        var dto = mapper.Map<AppealDTO>(entity);

        var videos = await GetDescriptionValues<AppealDesc>(entity.Id, DescriptionTypeConst.Video);
        dto.VideoUrls = videos.Select(x => x.Value).ToList();

        var images = await GetDescriptionValues<AppealDesc>(entity.Id, DescriptionTypeConst.Image);
        dto.ImageUrls = images.Select(x => x.Value).ToList();

        return dto;
    }
}
