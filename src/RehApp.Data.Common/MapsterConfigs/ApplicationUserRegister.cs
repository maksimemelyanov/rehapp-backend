using Mapster;
using RehApp.Data.Common.DTOs;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.Common.MapsterConfigs;

public class ApplicationUserRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, ApplicationUserMinDTO>();
    }
}
