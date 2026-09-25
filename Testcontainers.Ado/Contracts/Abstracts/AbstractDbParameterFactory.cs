using System.Data;
using System.Data.Common;

using System.Linq;

namespace Testcontainers.Ado.Contracts.Abstracts
{
    public abstract class AbstractDbParameterFactory<TDbParameter> : IDbParameterFactory where TDbParameter : DbParameter
    {
        /// <summary>
        /// Creates a new instance of <typeparamref name="TDbParameter"/>.
        /// </summary>
        protected abstract TDbParameter Create(DbTestParameter parameter);

        /// <inheritdoc/>
        public DbParameter[] Create(params DbTestParameter[] parameters)
        {
            return parameters.Select(Create).ToArray();
        }
    }
}