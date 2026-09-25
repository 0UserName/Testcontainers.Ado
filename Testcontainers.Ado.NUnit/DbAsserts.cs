using NUnit.Framework;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado.NUnit
{
    public static class DbAsserts
    {
        /// <summary>
        /// Applies a constraint to ensure that
        /// the number of inserted rows matches
        /// the expected row count.
        /// </summary>
        public static Task ThatInsertAsync(this IDbTestRunner runner, string procedure, object value, int expectedRows)
        {
            return Assert.ThatAsync(() => runner.InsertValueAsync(procedure, value), Is.EqualTo(expectedRows));
        }
    }
}