namespace ServerManagement.Infrastructure.Data.Configurations;

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(
            x => x.Name,
            bld =>
            {
                bld.Property(x => x.Value)
                    .HasColumnName(nameof(Server.Name))
                    .HasMaxLength(128)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            x => x.HostName,
            bld =>
            {
                bld.Property(x => x.Value)
                    .HasColumnName(nameof(Server.HostName))
                    .HasMaxLength(128)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            x => x.PrimaryIpAddress,
            bld =>
            {
                bld.Property(x => x.Value)
                    .HasColumnName(nameof(Server.PrimaryIpAddress))
                    .HasMaxLength(15);
            }
        );

        builder.Property(x => x.MemoryInGb).HasColumnType("decimal(18,2)");
        builder.Property(x => x.HealthScore).HasColumnType("decimal(18,2)");

        builder
            .Property(x => x.Status)
            .HasDefaultValue(OperationStatus.Running)
            .HasConversion(
                status => status.ToString(),
                status => (OperationStatus)Enum.Parse(typeof(OperationStatus), status)
            );
    }
}
