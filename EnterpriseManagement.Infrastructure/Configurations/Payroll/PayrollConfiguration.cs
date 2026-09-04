using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayrollEntity = EnterpriseManagement.Domain.Entities.Payroll.Payroll;

namespace EnterpriseManagement.Infrastructure.Configurations.Payroll;

public class PayrollConfiguration : IEntityTypeConfiguration<PayrollEntity>
{
    public void Configure(EntityTypeBuilder<PayrollEntity> builder)
    {
        builder.ToTable("Payrolls");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BaseSalary).HasPrecision(18, 2);
        builder.Property(x => x.Allowances).HasPrecision(18, 2);
        builder.Property(x => x.OvertimePay).HasPrecision(18, 2);
        builder.Property(x => x.Deductions).HasPrecision(18, 2);
        builder.Property(x => x.GrossSalary).HasPrecision(18, 2);
        builder.Property(x => x.NetSalary).HasPrecision(18, 2);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();

        builder.HasOne(x => x.PayrollPeriod)
            .WithMany(x => x.Payrolls)
            .HasForeignKey(x => x.PayrollPeriodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Approver)
            .WithMany()
            .HasForeignKey(x => x.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
