using EnterpriseManagement.Domain.Entities.Attendance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.Attendance;

public class WorkShiftConfiguration : IEntityTypeConfiguration<WorkShift>
{
    public void Configure(EntityTypeBuilder<WorkShift> builder)
    {
        builder.ToTable("WorkShifts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ShiftCode).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ShiftName).HasMaxLength(100).IsRequired();

        builder.HasIndex(x => x.ShiftCode).IsUnique();
    }
}
