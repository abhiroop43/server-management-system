namespace ServerManagement.Infrastructure.Data.Configurations;

public class HostedServiceConfiguration : IEntityTypeConfiguration<HostedService>
{
    public void Configure(EntityTypeBuilder<HostedService> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(
            x => x.HostedServiceName,
            bld =>
            {
                bld.Property(x => x.Value)
                    .HasColumnName(nameof(HostedService.HostedServiceName))
                    .HasMaxLength(128)
                    .IsRequired();
            }
        );
    }
}
