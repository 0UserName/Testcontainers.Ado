using System;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts
{
    public interface IDbTestRunner : IAsyncDisposable
    {
        DbDataSource Source
        {
            get;
        }

        /// <summary>
        /// Starts the dependent database and applies the SQL scripts to the database once it has started.
        /// </summary>
        /// 
        /// <param name="image">
        /// The full Docker image name, including the image repository and tag: <c>mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04</c>.
        /// </param>
        Task StartAsync(string image, string schemaDir);

        /// <summary>
        /// Executes a stored procedure with @i_p as an input argument
        /// with an inferred type and @o_p as a BIGINT output argument.
        /// </summary>
        ///
        /// <param name="procedure">
        /// Name of the stored procedure.
        /// </param>
        /// 
        /// <returns>
        /// The number of
        /// rows inserted.
        /// </returns>
        Task<long> InsertValueAsync(string procedure, object value);
    }
}