using ServerManagement.Domain.Events;

namespace ServerManagement.Domain.Entities;

public class HostedService : Aggregate<HostedServiceId>
{
    public ServerId ServerId { get; private set; } = null!;
    public HostedServiceName HostedServiceName { get; private set; } = null!;
    public int Port { get; private set; }
    public bool IsListening { get; private set; }
    public DateTimeOffset LastChecked { get; private set; }
    public bool IsActive { get; private set; }

    public static HostedService Add(
        HostedServiceId id,
        ServerId serverId,
        HostedServiceName hostedServiceName,
        int port,
        bool isListening,
        DateTimeOffset lastChecked
    )
    {
        if (port <= 0)
        {
            throw new DomainException("Port must be greater than 0");
        }

        var hostedService = new HostedService
        {
            Id = id,
            ServerId = serverId,
            HostedServiceName = hostedServiceName,
            Port = port,
            IsListening = isListening,
            LastChecked = lastChecked,
            IsActive = true,
        };

        hostedService.AddDomainEvent(new ServiceCreatedEvent(hostedService));

        return hostedService;
    }

    public void Update(
        HostedServiceName hostedServiceName,
        int port,
        bool isListening,
        DateTimeOffset lastChecked
    )
    {
        if (port <= 0)
        {
            throw new DomainException("Port must be greater than 0");
        }

        HostedServiceName = hostedServiceName;
        Port = port;
        IsListening = isListening;
        LastChecked = lastChecked;
        IsActive = true;

        AddDomainEvent(new ServiceUpdatedEvent(this));
    }

    public void Remove()
    {
        IsActive = false;
        IsListening = false;

        AddDomainEvent(new ServiceRemovedEvent(this));
    }
}
