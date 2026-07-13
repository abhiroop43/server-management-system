using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServerManagement.Infrastructure.Data.Configurations;

public class HostedServiceConfiguration : IEntityTypeConfiguration<HostedService>
{
    public void Configure(EntityTypeBuilder<HostedService> builder)
    {
        builder.HasKey(x => x.Id);

        var hostedServiceIdConverter = new ValueConverter<HostedServiceId, Guid>(
            hostedServiceId => hostedServiceId.Value,
            dbId => HostedServiceId.Of(dbId)
        );

        builder.Property(x => x.Id).HasConversion(hostedServiceIdConverter);

        var serverIdConverter = new ValueConverter<ServerId, Guid>(
            serverId => serverId.Value,
            dbId => ServerId.Of(dbId)
        );

        builder.Property(x => x.ServerId).HasConversion(serverIdConverter);

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
