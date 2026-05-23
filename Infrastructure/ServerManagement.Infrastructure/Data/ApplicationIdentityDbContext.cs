using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ServerManagement.Infrastructure.Data;

public class ApplicationIdentityDbContext : IdentityDbContext
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options) { }
}
