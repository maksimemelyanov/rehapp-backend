﻿using RehApp.Infrastructure.Common.Interfaces;

namespace RehApp.Domain.RelationalDatabase.Entities;

public class DescriptionType : IIdentified
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}