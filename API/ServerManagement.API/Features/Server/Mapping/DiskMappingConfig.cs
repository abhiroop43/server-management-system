namespace ServerManagement.API.Features.Server.Mapping;

public class DiskMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Domain.Entities.Disk, DiskDto>()
            .Map(dto => dto.Id, ent => ent.Id.Value)
            .Map(dto => dto.Name, ent => ent.Name.Value);
    }
}
