using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore.EntityConfigurations;

public class DiaryEntriesConfiguration : IEntityTypeConfiguration<DiaryEntry>
{
    public void Configure(EntityTypeBuilder<DiaryEntry> builder)
    {
        builder.OwnsOne(o => o.Pressure, p =>
        {
            p.Property(x => x.UpperBloodPressure);
            p.Property(x => x.LowerBloodPressure);
        });
    }
}
