using EnterpriseManagement.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseManagement.Infrastructure.Configurations.Identity;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MenuCode).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MenuName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Icon).HasMaxLength(100);
        builder.Property(x => x.Route).HasMaxLength(255);

        builder.HasIndex(x => x.MenuCode).IsUnique();

        builder.HasOne(x => x.ParentMenu)
            .WithMany(x => x.ChildMenus)
            .HasForeignKey(x => x.ParentMenuId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
