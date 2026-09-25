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

        IDbTestCommand DbCommand
        {
            get;
        }

        /// <summary>
        /// Starts the database container and applies the SQL scripts once the container has started.
        /// </summary>
        Task StartAsync();
    }
}