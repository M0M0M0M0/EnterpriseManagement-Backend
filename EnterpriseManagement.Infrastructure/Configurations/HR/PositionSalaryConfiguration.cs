using EnterpriseManagement.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.HR;

public class PositionSalaryConfiguration : IEntityTypeConfiguration<PositionSalary>
{
    public void Configure(EntityTypeBuilder<PositionSalary> builder)
    {
        builder.ToTable("PositionSalaries");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StandardSalary).HasPrecision(18, 2);

        builder.HasIndex(x => x.PositionId).IsUnique();
    }
}
