using EnterpriseManagement.Domain.Entities.Payroll;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.Payroll;

public class PayrollDetailConfiguration : IEntityTypeConfiguration<PayrollDetail>
{
    public void Configure(EntityTypeBuilder<PayrollDetail> builder)
    {
        builder.ToTable("PayrollDetails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ComponentName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.ComponentType).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(x => x.Amount).HasPrecision(18, 2);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasOne(x => x.Payroll)
            .WithMany(x => x.PayrollDetails)
            .HasForeignKey(x => x.PayrollId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
