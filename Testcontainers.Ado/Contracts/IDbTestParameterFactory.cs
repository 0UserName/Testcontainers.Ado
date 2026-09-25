using System.Data.Common;

namespace Testcontainers.Ado.Contracts
{
    public interface IDbTestParameterFactory
    {
        /// <summary>
        /// Creates DbParameter instances
        /// from the specified parameters.
        /// </summary>
        DbParameter[] Create(DbTestParameter[] parameters);
    }
}