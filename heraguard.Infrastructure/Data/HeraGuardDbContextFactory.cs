using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace heraguard.Infrastructure.Data;

public class HeraGuardDbContextFactory : IDesignTimeDbContextFactory<HeraGuardDbContext>
{
    public HeraGuardDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HeraGuardDbContext>();
            
        // Coloca temporalmente tu cadena de conexión para migraciones
        optionsBuilder.UseNpgsql("Host=aws-1-us-east-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.vjgaifeloyvwqxaymzcm;Password=KTjgf5R3?;Timeout=300;CommandTimeout=300");
            
        return new HeraGuardDbContext(optionsBuilder.Options);
    }
}