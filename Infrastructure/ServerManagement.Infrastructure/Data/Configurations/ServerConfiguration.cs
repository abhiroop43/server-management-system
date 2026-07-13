using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ServerManagement.Infrastructure.Data.Configurations;

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasKey(x => x.Id);

        var serverIdConverter = new ValueConverter<ServerId, Guid>(
            serverId => serverId.Value,
            dbId => ServerId.Of(dbId)
        );

        builder.Property(x => x.Id).HasConversion(serverIdConverter);

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

        builder
            .Property(x => x.Tags)
            .HasConversion(
                tags => string.Join(',', tags),
                str => str.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder
            .Property(x => x.IpAddresses)
            .HasConversion(
                ips => string.Join(',', ips),
                str => str.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder
            .Property(x => x.Metadata)
            .HasConversion(
                dict => JsonSerializer.Serialize(dict, (JsonSerializerOptions?)null),
                str =>
                    JsonSerializer.Deserialize<Dictionary<string, string>>(
                        str,
                        (JsonSerializerOptions?)null
                    )!
            )
            .HasColumnType("nvarchar(max)");
    }
}
