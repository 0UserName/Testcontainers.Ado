using Npgsql;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts.Abstracts;

using Testcontainers.PostgreSql;

namespace Testcontainers.Ado.PostgreSql
{
    public class PostgreSqlRunner : AbstractDbTestRunner<PostgreSqlContainer, NpgsqlParameter>
    {
        /// <remarks>
        /// See: <see href="https://hub.docker.com/_/postgres#initialization-scripts">Initialization scripts</see>.
        /// </remarks>
        private const string ENTRY_POINT_DIR = "/docker-entrypoint-initdb.d/";

        /// <summary>
        /// Creates a new data source builder
        /// using the given connection string.
        /// </summary>
        protected virtual NpgsqlDataSourceBuilder CreateDataSourceBuilder(string connectionString)
        {
            return new NpgsqlDataSourceBuilder(connectionString);
        }

        /// <inheritdoc/>
        public override async Task StartAsync(string image, string schemaDir)
        {
            Container = new PostgreSqlBuilder(image).WithResourceMapping(schemaDir, ENTRY_POINT_DIR).Build();

            await Container.StartAsync();

            Source = CreateDataSourceBuilder(Container.GetConnectionString()).Build();
        }
    }
}