using Mapster;
using RehApp.Data.Common.DTOs;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;

namespace RehApp.Data.Common.MapsterConfigs;

public class AppealRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AppealStatus, AppealStatusDTO>()
            .Map(x => x.Value, y => y)
            .Map(x => x.Description, y => y);

        config.NewConfig<AppealType, AppealTypeDTO>()
            .Map(x => x.Value, y => y)
            .Map(x => x.Description, y => y);

        config.NewConfig<Appeal, AppealDTO>()
            .Ignore(x => x.VideoUrls!)
            .Ignore(x => x.ImageUrls!);
    }
}