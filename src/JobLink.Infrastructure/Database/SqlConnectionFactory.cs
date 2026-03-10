using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace JobLink.Infrastructure.Database;

public class SqlConnectionFactory(IConfiguration configuration)
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}