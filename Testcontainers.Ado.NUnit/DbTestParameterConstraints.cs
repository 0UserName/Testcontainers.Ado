using NUnit.Framework;
using NUnit.Framework.Constraints;

using System.Data.Common;

namespace Testcontainers.Ado.NUnit
{
    public static class DbTestParameterConstraints
    {
        /// <summary>
        /// Returns a constraint that tests whether the procedure has an output parameter
        /// with the specified name and verifies that its value equals the expected value.
        /// </summary>
        public static IResolveConstraint GetDbParameterEqualTo<T>(string parameterName, T expected)
        {
            return Has.Some.Matches<DbParameter>(p => p.ParameterName == parameterName && Equals(p.Value, expected));
        }
    }
}