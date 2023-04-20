namespace RehApp.Infrastructure.Common.Interfaces;

public interface ITranslator<Entity, DTO>
    where Entity : IIdentified
    where DTO : IDTO
{
    public Task<DTO> BusinessToDTOAsync(Entity entity);
}
