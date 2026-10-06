using System.Collections.Generic;

using System.Data;
using System.Data.Common;

using System.Linq;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado
{
    public class DbTestCommand(DbDataSource source, IDbParameterFactory parameterFactory) : Contracts.IDbCommand
    {
        /// <inheritdoc/>
        public virtual DbCommand CreateCommand(string commandText, CommandType type, params DbTestParameter[] parameters)
        {
            DbCommand command = source.CreateCommand(commandText);

            command.CommandType = type;

            command.Parameters.AddRange(parameterFactory.Create(parameters));

            return command;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<DbParameter>> ExecuteProcedureAsync(string procedure, DbTestParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(procedure, CommandType.StoredProcedure, parameters))
            {
                await command.ExecuteNonQueryAsync();

                return command.Parameters.Cast<DbParameter>().Where(p => p.Direction == ParameterDirection.Output).ToArray();
            }
        }
    }
}