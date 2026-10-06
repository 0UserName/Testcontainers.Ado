using NUnit.Framework;
using NUnit.Framework.Constraints;

using System.Data.Common;

namespace Testcontainers.Ado.NUnit
{
    public static class DbParameterConstraints
    {
        /// <summary>
        /// Returns a constraint that tests
        /// an output parameter by name and
        /// value.
        /// </summary>
        public static IResolveConstraint GetDbParameterEqualTo<T>(string name, T value)
        {
            return Has.Some.Matches<DbParameter>(p => p.ParameterName == name && Equals(p.Value, value));
        }
    }
}