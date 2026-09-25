using NUnit.Framework;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado.NUnit
{
    public static class DbTestParameterAsserts
    {
        /// <summary>
        /// Applies a constraint that tests whether the result of executing the stored
        /// procedure contains an output parameter with the specified name whose value
        /// equals the expected number of inserted rows.
        /// </summary>
        public static Task ThatInsertAsync(this IDbTestRunner runner, string procedure, string parameterName, long expectedRows, params DbTestParameter[] parameters)
        {
            return Assert.ThatAsync(() => runner.DbCommand.ExecuteProcedureAsync(procedure, parameters), DbTestParameterConstraints.GetDbParameterEqualTo(parameterName, expectedRows));
        }
    }
}