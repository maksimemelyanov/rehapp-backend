using RehApp.Data.Common;
using RehApp.Data.Common.DTOs;
using RehApp.Data.EntityFrameworkCore.Translators.Base;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.Translators;

public class ReviewDtoTranslator : Translator<Review, ReviewDTO>
{
    public ReviewDtoTranslator(ApplicationDbContext context) : base(context) { }

    public override async Task<ReviewDTO> BusinessToDTOAsync(Review entity)
    {
        var getDescValues = async (long descTypeCode) => await GetDescriptionValues<PostDesc>(
            parentId: entity.Id,
            descriptionTypeCode: descTypeCode,
            orderBy: (x) => entity.Date);

        var images = await getDescValues(DescriptionTypeConst.Image);
        var videos = await getDescValues(DescriptionTypeConst.Video);

        var dto = new ReviewDTO
        {
            Date = entity.Date,
            Author = new ApplicationUserMinDTO
            {
                AvatarUrl = entity.Author.AvatarUrl,
                Email = entity.Author.Email!,
                FirstName = entity.Author.FirstName,
                LastName = entity.Author.LastName,
                UserName = entity.Author.UserName!
            },
            Evaluation = entity.Evaluation,
            Text = entity.Text,
            ImageUrls = images.Select(x => x.Value).ToList(),
            VideoUrls = videos.Select(x => x.Value).ToList()
        };

        return dto;
    }
}
