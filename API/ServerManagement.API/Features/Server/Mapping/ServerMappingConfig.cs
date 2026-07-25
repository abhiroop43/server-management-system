using ServerManagement.API.Features.Server.GetServerDetails;

namespace ServerManagement.API.Features.Server.Mapping;

public class ServerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Domain.Entities.Server, ServerDto>()
            .Map(dto => dto.Id, ent => ent.Id.Value)
            .Map(dto => dto.Name, ent => ent.Name.Value)
            .Map(dto => dto.HostName, ent => ent.HostName.Value)
            .Map(dto => dto.PrimaryIpAddress, ent => ent.PrimaryIpAddress.Value);

        config
            .NewConfig<Domain.Entities.Server, GetServerDetailsResult>()
            .Map(dto => dto.Id, ent => ent.Id.Value)
            .Map(dto => dto.Name, ent => ent.Name.Value)
            .Map(dto => dto.HostName, ent => ent.HostName.Value)
            .Map(dto => dto.PrimaryIpAddress, ent => ent.PrimaryIpAddress.Value)
            .Map(dto => dto.UpTime, ent => ent.UpTime.ToString());
    }
}
