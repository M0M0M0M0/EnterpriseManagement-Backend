using EnterpriseManagement.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.Sales;

public class SalesKpiTargetConfiguration : IEntityTypeConfiguration<SalesKpiTarget>
{
    public void Configure(EntityTypeBuilder<SalesKpiTarget> builder)
    {
        builder.ToTable("SalesKpiTargets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TargetName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.MinimumRevenue).HasPrecision(18, 2);
        builder.Property(x => x.CommissionRate).HasPrecision(5, 2);
    }
}
