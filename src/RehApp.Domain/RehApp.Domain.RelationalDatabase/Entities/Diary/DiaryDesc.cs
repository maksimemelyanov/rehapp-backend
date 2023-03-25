﻿using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class DiaryDesc : IIdentified, IDescription
{
    public Guid Id { get; set; }

    public Guid DiaryId { get; set; }
    public virtual Diary Diary { get; set; } = null!;

    public Guid DescriptionTypeId { get; set; }
    public virtual DescriptionType DescriptionType { get; set; } = null!;

    public Guid DescriptionValueId { get; set; }
    public virtual DescriptionValue DescriptionValue { get; set; } = null!;
}
