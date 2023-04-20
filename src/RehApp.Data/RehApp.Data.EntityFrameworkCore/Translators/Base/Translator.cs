using Microsoft.EntityFrameworkCore;
using RehApp.Domain.RelationalDatabase.Abstractions;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Infrastructure.Common.Enums;
using RehApp.Infrastructure.Common.Interfaces;
using System.Linq.Expressions;

namespace RehApp.Data.EntityFrameworkCore.Translators.Base;

public abstract class Translator<Entity, DTO> : ITranslator<Entity, DTO>
    where Entity : IIdentified
    where DTO : IDTO
{
    protected readonly ApplicationDbContext context;

    public Translator(ApplicationDbContext context)
    {
        this.context = context;
    }

    protected async Task<DescriptionValue?> GetDescriptionValue<T>(
        Guid parentId,
        long descriptionTypeCode,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? orderBy = null,
        SortingDirection sortingDirection = SortingDirection.Ascending)
        where T : class, IDescriptionEntity
    {
        var descriptionValues = await GetDescriptionValues(
            parentId: parentId,
            descriptionTypeCode: descriptionTypeCode,
            filter: filter,
            orderBy: orderBy,
            sortingDirection: sortingDirection);

        return descriptionValues.Count > 1 ? null : descriptionValues.SingleOrDefault();
    }

    protected async Task<List<DescriptionValue>> GetDescriptionValues<T>(
        Guid parentId,
        long descriptionTypeCode,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? orderBy = null,
        SortingDirection sortingDirection = SortingDirection.Ascending)
        where T : class, IDescriptionEntity
    {
        var query = context.Set<T>()
            .Where(x => x.ParentId == parentId && x.DescriptionType.Code == descriptionTypeCode);

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = sortingDirection == SortingDirection.Ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
        }

        var descriptionValues = await query.Select(x => x.DescriptionValue).ToListAsync();
        return descriptionValues ?? new();
    }

    public abstract Task<DTO> BusinessToDTOAsync(Entity entity);
}
