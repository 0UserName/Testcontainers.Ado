using DotNet.Testcontainers.Containers;

using Microsoft.Data.SqlClient;

using System;
using System.Data.Common;

using System.IO;

using System.Linq;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;
using Testcontainers.Ado.Contracts.Abstracts;

using Testcontainers.MsSql;

namespace Testcontainers.Ado.SqlServer
{
    public sealed class SqlServerRunner(string image, string schemaDir, Func<DbDataSource, IDbCommand> commandFactory) : AbstractDbRunner(commandFactory ?? DefaultCommandFactory<SqlServerDbParameterFactory>)
    {
        private readonly IDatabaseContainer _container = new MsSqlBuilder(image).Build();

        /// <inheritdoc/>
        protected override DbDataSource CreateDataSource()
        {
            return SqlClientFactory.Instance.CreateDataSource(_container.GetConnectionString());
        }

        /// <inheritdoc/>
        protected override Task StartContainerAsync()
        {
            return _container.StartAsync();
        }

        /// <inheritdoc/>
        protected override async Task SetupSchemaAsync()
        {
            foreach (string batch in Directory.GetFiles(schemaDir).SelectMany(s => File.ReadAllText(s).Split("GO", StringSplitOptions.RemoveEmptyEntries)))
            {
                await Source.CreateCommand(batch).ExecuteNonQueryAsync();
            }
        }

        /// <inheritdoc/>
        public override ValueTask DisposeAsync()
        {
            return _container.DisposeAsync();
        }
    }
}