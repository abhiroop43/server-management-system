namespace ServerManagement.API.Features.Server.Mapping;

public class HostedServiceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Domain.Entities.HostedService, HostedServiceDto>()
            .Map(dto => dto.Id, ent => ent.Id.Value)
            .Map(dto => dto.ServiceName, ent => ent.HostedServiceName.Value);
    }
}
