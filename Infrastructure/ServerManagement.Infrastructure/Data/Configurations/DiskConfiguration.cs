using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServerManagement.Infrastructure.Data.Configurations;

public class DiskConfiguration : IEntityTypeConfiguration<Disk>
{
    public void Configure(EntityTypeBuilder<Disk> builder)
    {
        builder.HasKey(x => x.Id);

        var diskIdConverter = new ValueConverter<DiskId, Guid>(
            diskId => diskId.Value,
            dbId => DiskId.Of(dbId)
        );

        builder.Property(x => x.Id).HasConversion(diskIdConverter);

        var serverIdConverter = new ValueConverter<ServerId, Guid>(
            serverId => serverId.Value,
            dbId => ServerId.Of(dbId)
        );

        builder.Property(x => x.ServerId).HasConversion(serverIdConverter);

        builder.ComplexProperty(
            x => x.Name,
            bld =>
            {
                bld.Property(x => x.Value)
                    .HasColumnName(nameof(Disk.Name))
                    .HasMaxLength(128)
                    .IsRequired();
            }
        );

        builder
            .Property(x => x.DiskType)
            .HasDefaultValue(DiskType.SSD)
            .HasConversion(
                dType => dType.ToString(),
                dType => (DiskType)Enum.Parse(typeof(DiskType), dType)
            );
    }
}
