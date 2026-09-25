using System;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts.Abstracts
{
    public abstract class AbstractDbRunner(Func<DbDataSource, IDbCommand> commandFactory) : IDbRunner
    {
        public DbDataSource Source
        {
            get => field ??= CreateDataSource();
        }

        public IDbCommand DbCommand
        {
            get => field ??= commandFactory(Source);
        }

        protected static IDbCommand DefaultCommandFactory<TDbParameterFactory>(DbDataSource source) where TDbParameterFactory : IDbParameterFactory, new()
        {
            return new DbTestCommand(source, new TDbParameterFactory());
        }

        /// <summary>
        /// Creates a data source that can be used to obtain
        /// open connections, and against which commands can
        /// be executed directly.
        /// </summary>
        protected abstract DbDataSource CreateDataSource();

        /// <summary>
        /// Starts the container.
        /// </summary>
        protected abstract Task StartContainerAsync();

        /// <summary>
        /// Initializes the database schema
        /// after the container has started.
        /// </summary>
        /// 
        /// <remarks>
        /// Override this method when the container
        /// does not support initialization scripts.
        /// </remarks>
        protected virtual Task SetupSchemaAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StartAsync()
        {
            return StartContainerAsync().ContinueWith(continuationFunction => SetupSchemaAsync(), TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();
        }

        /// <inheritdoc/>
        public abstract ValueTask DisposeAsync();
    }
}