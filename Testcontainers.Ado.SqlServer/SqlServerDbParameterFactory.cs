using Microsoft.Data.SqlClient;

using System.Data;

using Testcontainers.Ado.Contracts.Abstracts;

namespace Testcontainers.Ado.SqlServer
{
    public class SqlServerDbParameterFactory : AbstractDbParameterFactory<SqlParameter>
    {
        /// <inheritdoc/>
        protected override SqlParameter Create(DbTestParameter parameter)
        {
            SqlParameter p = new
            SqlParameter
            (parameter.Name, parameter.Value)
            {
                Direction = parameter.Direction
            };

            if (parameter.Type != default)
            {
                p.SqlDbType = (SqlDbType)parameter.Type;
            }

            return p;
        }
    }
}