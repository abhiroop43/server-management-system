namespace ServerManagement.Infrastructure.Data.Configurations;

public class DiskConfiguration : IEntityTypeConfiguration<Disk>
{
    public void Configure(EntityTypeBuilder<Disk> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(disk => disk.Value, val => DiskId.Of(val));

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
