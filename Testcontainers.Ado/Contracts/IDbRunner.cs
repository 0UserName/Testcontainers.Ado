using System;
using System.Data.Common;

using System.Threading.Tasks;

namespace Testcontainers.Ado.Contracts
{
    public interface IDbRunner : IAsyncDisposable
    {
        DbDataSource Source
        {
            get;
        }

        IDbCommand DbCommand
        {
            get;
        }

        /// <summary>
        /// Starts the database container and applies the SQL scripts once the container has started.
        /// </summary>
        Task StartAsync();
    }
}