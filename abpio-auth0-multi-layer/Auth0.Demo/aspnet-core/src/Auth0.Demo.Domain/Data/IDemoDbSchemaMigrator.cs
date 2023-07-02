using System.Threading.Tasks;

namespace Auth0.Demo.Data;

public interface IDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
