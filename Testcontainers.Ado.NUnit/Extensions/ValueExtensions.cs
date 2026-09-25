using System.Data;

namespace Testcontainers.Ado.NUnit.Extensions
{
    public static class ValueExtensions
    {
        /// <summary>
        /// Creates an input parameter from the specified values.
        /// </summary>
        /// 
        /// <param name="type">
        /// A <b>database-specific</b> type, if specified; otherwise, it is inferred from the value.
        /// </param>
        public static DbTestParameter AsIn(this object value, string name, int? type = default)
        {
            return new DbTestParameter(name, value, type, ParameterDirection.Input);
        }

        /// <inheritdoc cref="AsIn(object, string, int?)" />
        public static DbTestParameter AsIn(this object value, int? type = default)
        {
            return value.AsIn(DbParameterNames.I_P, type);
        }

        /// <summary>
        /// Creates an output parameter from the specified values.
        /// </summary>
        /// 
        /// <param name="type">
        /// A <b>database-specific</b> type, if specified; otherwise, it is inferred from the value.
        /// </param>
        public static DbTestParameter AsOut(this object value, string name, int? type = default)
        {
            return new DbTestParameter(name, value, type, ParameterDirection.Output);
        }

        /// <inheritdoc cref="AsOut(object, string, int?)" />
        public static DbTestParameter AsOut(this object value, int? type = default)
        {
            return value.AsOut(DbParameterNames.O_P, type);
        }
    }
}