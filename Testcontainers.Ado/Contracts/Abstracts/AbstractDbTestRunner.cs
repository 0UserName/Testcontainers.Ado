using System;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts.Abstracts
{
    public abstract class AbstractDbTestRunner(Func<DbDataSource, IDbTestCommand> commandFactory) : IDbTestRunner
    {
        public DbDataSource Source
        {
            get => field ??= CreateDataSource();
        }

        public IDbTestCommand DbCommand
        {
            get => field ??= commandFactory(Source);
        }

        /// <summary>
        /// Returns the default IDbTestCommand implementation.
        /// </summary>
        protected static IDbTestCommand DefaultCommandFactory<TDbParameter, TDbType>(DbDataSource source) where TDbParameter : DbParameter where TDbType : Enum
        {
            return new DbTestCommand(source, new DbTestParameterFactory<TDbParameter, TDbType>());
        }

        /// <summary>
        /// Gets the database
        /// connection string.
        /// </summary>
        protected abstract string GetConnectionString();

        /// <summary>
        /// Creates a data source that can be used to obtain
        /// open connections, and against which commands can
        /// be executed directly.
        /// </summary>
        protected abstract DbDataSource CreateDataSource();

        /// <summary>
        /// Initializes the database schema
        /// after the container has started.
        /// </summary>
        /// 
        /// <remarks>
        /// Override this method only when the container does not support automatic schema initialization.
        /// </remarks>
        protected virtual Task SetupSchemaAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public abstract Task StartAsync();

        /// <inheritdoc/>
        public abstract ValueTask DisposeAsync();
    }
}