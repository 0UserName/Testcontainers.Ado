using System.Data;

namespace Testcontainers.Ado
{
    /// <param name="Type">
    /// Gets or sets a <b>database-specific</b> type,
    /// if specified; otherwise, it is inferred from
    /// the value.
    /// </param>
    public record class DbTestParameter(string Name, object Value, int? Type, ParameterDirection Direction)
    { }
}