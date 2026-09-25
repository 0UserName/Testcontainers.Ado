using DotNet.Testcontainers.Containers;

using Npgsql;
using NpgsqlTypes;

using System;
using System.Data.Common;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;
using Testcontainers.Ado.Contracts.Abstracts;

using Testcontainers.PostgreSql;

namespace Testcontainers.Ado.PostgreSql
{
    public sealed class PostgreSqlRunner(string image, string schemaDir, Func<DbDataSource, IDbTestCommand> commandFactory, Func<NpgsqlDataSourceBuilder, NpgsqlDataSourceBuilder> typeMapper) : AbstractDbTestRunner(commandFactory ?? DefaultCommandFactory<NpgsqlParameter, NpgsqlDbType>)
    {
        /// <remarks>
        /// See: <see href="https://hub.docker.com/_/postgres#initialization-scripts">Initialization scripts</see>.
        /// </remarks>
        private readonly IDatabaseContainer _container = new PostgreSqlBuilder(image).WithResourceMapping(schemaDir, "/docker-entrypoint-initdb.d/").Build();

        /// <summary>
        /// Creates a new data source builder
        /// using the given connection string.
        /// </summary>
        private NpgsqlDataSourceBuilder CreateDataSourceBuilder()
        {
            NpgsqlDataSourceBuilder builder = new
            NpgsqlDataSourceBuilder
            (GetConnectionString());

            return typeMapper == default ? builder : typeMapper(builder);
        }

        /// <inheritdoc/>
        protected override string GetConnectionString()
        {
            return _container.GetConnectionString();
        }

        /// <inheritdoc/>
        protected override DbDataSource CreateDataSource()
        {
            return CreateDataSourceBuilder().Build();
        }

        /// <inheritdoc/>
        public override Task StartAsync()
        {
            return _container.StartAsync();
        }

        /// <inheritdoc/>
        public override ValueTask DisposeAsync()
        {
            return _container.DisposeAsync();
        }
    }
}