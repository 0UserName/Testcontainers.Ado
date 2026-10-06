using System.Collections.Generic;

using System.Data;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts
{
    public interface IDbCommand
    {
        /// <summary>
        /// Returns a DbCommand that is
        /// ready for execution against
        /// the DbDataSource.
        /// </summary>
        DbCommand CreateCommand(string commandText, CommandType type, params DbTestParameter[] parameters);

        /// <summary>
        /// Executes the specified stored procedure.
        /// </summary>
        /// 
        /// <param name="procedure">
        /// The text command to run
        /// against the data source.
        /// </param>
        /// 
        /// <returns>
        /// The output parameters.
        /// </returns>
        Task<IEnumerable<DbParameter>> ExecuteProcedureAsync(string procedure, DbTestParameter[] parameters);
    }
}