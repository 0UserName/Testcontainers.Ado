using Npgsql;
using NpgsqlTypes;

using Testcontainers.Ado.Contracts.Abstracts;

namespace Testcontainers.Ado.PostgreSql
{
    public class PostgreSqlDbParameterFactory : AbstractDbParameterFactory<NpgsqlParameter>
    {
        /// <inheritdoc/>
        protected override NpgsqlParameter Create(DbTestParameter parameter)
        {
            NpgsqlParameter p = new
            NpgsqlParameter
            (parameter.Name, parameter.Value)
            {
                Direction = parameter.Direction
            };

            if (parameter.Type != default)
            {
                p.NpgsqlDbType = (NpgsqlDbType)parameter.Type;
            }

            return p;
        }
    }
}