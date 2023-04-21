using MapsterMapper;
using RehApp.Data.Common;
using RehApp.Data.Common.DTOs;
using RehApp.Data.EntityFrameworkCore.Translators.Base;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.Translators;

public class ReviewDtoTranslator : Translator<Review, ReviewDTO>
{
    public ReviewDtoTranslator(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }

    public override async Task<ReviewDTO> BusinessToDTOAsync(Review entity)
    {
        var dto = mapper.Map<ReviewDTO>(entity);

        var videos = await GetDescriptionValues<ReviewDesc>(entity.Id, DescriptionTypeConst.Video);
        dto.VideoUrls = videos.Select(x => x.Value).ToList();

        var images = await GetDescriptionValues<ReviewDesc>(entity.Id, DescriptionTypeConst.Image);
        dto.ImageUrls = images.Select(x => x.Value).ToList();

        return dto;
    }
}
