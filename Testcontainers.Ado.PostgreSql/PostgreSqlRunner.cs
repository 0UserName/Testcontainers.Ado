using DotNet.Testcontainers.Containers;

using Npgsql;

using System;
using System.Data.Common;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;
using Testcontainers.Ado.Contracts.Abstracts;

using Testcontainers.PostgreSql;

namespace Testcontainers.Ado.PostgreSql
{
    public sealed class PostgreSqlRunner(string image, string schemaDir, Func<DbDataSource, IDbCommand> commandFactory, Action<NpgsqlDataSourceBuilder> configure) : AbstractDbRunner(commandFactory ?? DefaultCommandFactory<PostgreSqlDbParameterFactory>)
    {
        /// <remarks>
        /// See: <see href="https://hub.docker.com/_/postgres#initialization-scripts">Initialization scripts</see>.
        /// </remarks>
        private readonly IDatabaseContainer _container = new PostgreSqlBuilder(image).WithResourceMapping(schemaDir, "/docker-entrypoint-initdb.d/").Build();

        /// <inheritdoc/>
        protected override DbDataSource CreateDataSource()
        {
            NpgsqlDataSourceBuilder builder = new
            NpgsqlDataSourceBuilder
            (_container.GetConnectionString());

            configure?.Invoke(builder);

            return builder.Build();
        }

        /// <inheritdoc/>
        protected override Task StartContainerAsync()
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