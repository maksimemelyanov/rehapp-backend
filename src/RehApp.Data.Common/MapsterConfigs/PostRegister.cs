using Mapster;
using RehApp.Data.Common.DTOs;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.Common.MapsterConfigs;

public class PostRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Post, PostDTO>()
            .Ignore(x => x.AudioUrls!)
            .Ignore(x => x.VideoUrls!)
            .Ignore(x => x.ImageUrls!);
    }
}
