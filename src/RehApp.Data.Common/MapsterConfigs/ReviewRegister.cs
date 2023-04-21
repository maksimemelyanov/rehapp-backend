using Mapster;
using RehApp.Data.Common.DTOs;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.Common.MapsterConfigs;

public class ReviewRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Review, ReviewDTO>()
            .Ignore(x => x.ImageUrls!)
            .Ignore(x => x.VideoUrls!);
    }
}
