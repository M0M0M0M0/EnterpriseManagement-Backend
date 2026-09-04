using EnterpriseManagement.Domain.Entities.Leave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.Leave;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveTypes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LeaveTypeCode).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LeaveTypeName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.DefaultDays).HasPrecision(5, 2);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasIndex(x => x.LeaveTypeCode).IsUnique();
    }
}
