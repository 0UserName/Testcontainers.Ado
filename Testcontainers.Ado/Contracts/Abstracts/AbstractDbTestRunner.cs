using System;
using System.Data;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts.Abstracts
{
    public abstract class AbstractDbTestRunner<TContainer, TDbParameter> : IDbTestRunner where TContainer : IAsyncDisposable where TDbParameter : DbParameter
    {
        protected TContainer Container
        {
            get;
            set;
        }

        public DbDataSource Source
        {
            get;
            protected set;
        }

        /// <summary>
        /// Executes the SQL scripts in the
        /// specified directory against the
        /// database.
        /// </summary>
        /// 
        /// <remarks>
        /// Override only when the container does not support automatic initialization.
        /// </remarks>
        protected virtual Task SetupSchemaAsync(string schemaDir)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates a new instance of a DbParameter object.
        /// </summary>
        /// 
        /// <remarks>
        /// Override when adding driver-specific features.
        /// </remarks>
        protected virtual AbstractDbTestRunner<TContainer, TDbParameter> CreateDbParameter(string name, object value, ParameterDirection direction, out TDbParameter dbParameter)
        {
            dbParameter = (TDbParameter)Activator.CreateInstance(typeof(TDbParameter), name, value);

            dbParameter.Direction = direction;

            return this;
        }

        /// <inheritdoc/>
        public abstract Task StartAsync(string image, string schemaDir);

        /// <inheritdoc/>
        public async Task<long> InsertValueAdoAsync(string procedure, object value)
        {
            using (DbCommand command = Source.CreateCommand(procedure))
            {
                CreateDbParameter("@i_p", value, ParameterDirection.Input, out TDbParameter i).CreateDbParameter("@o_p", default(long), ParameterDirection.Output, out TDbParameter o);

                command.Parameters.Add(i);
                command.Parameters.Add(o);

                command.CommandType = CommandType.StoredProcedure;

                await command.ExecuteNonQueryAsync();

                return (long)o.Value;
            }
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            return Container.DisposeAsync();
        }
    }
}