![Release workflow](https://github.com/0UserName/testcontainers.ado/actions/workflows/release.yml/badge.svg)



# Motivation

<div align="justify">

The library simplifies the creation of test projects for database-driver-specific functionality, such as driver plugins and custom wrappers around types used to pass data to databases.

</div>



# Usage

<div align="justify">

A custom fixture should be created and provided through the `TestFixtureSource` attribute applied to the test class:

</div>



```csharp
internal static class PostgreSqlFixture
{
    public static IEnumerable Runners
    {
        get
        {
            yield return new TestFixtureData(new PostgreSqlRunner("postgres:14-alpine", "Init", null, (NpgsqlDataSourceBuilder builder) => builder.UseTvp()));
        }
    }
}
```



<div align="justify">

The test class should inherit from `AbstractDbTests<TDbRunner>` and use the runner type provided by the fixture as its generic type parameter:

</div>



```csharp
[TestFixtureSource(typeof(PostgreSqlFixture), nameof(PostgreSqlFixture.Runners))]
public sealed class InsertTests<TDbRunner>(TDbRunner runner) : AbstractDbTests<TDbRunner>(runner) where TDbRunner : IDbRunner
{
    [TestCase("dbo.procedure1")]
    [TestCase("dbo.procedure2")]
    [TestCase("dbo.procedure3")]
    public async Task TestDataTableAsync(string procedure)
    {
        await Runner.ThatOutputRowsAsync(procedure, DbParameterNames.O_P, table.Rows.Count, table.AsIn(default), default(long).AsOut(default)); // Initialize the table variable beforehand.
    }
}
```



<div align="justify">

Test methods can then be implemented using standard `NUnit` assertion methods, along with a small set of `DbParameterAsserts` methods provided by the library.

</div>



> [!NOTE]
> See additional examples in the following projects: [DbExtensions.Tvp](https://github.com/0UserName/DbExtensions.Tvp/tree/master/DbExtensions.Tvp.Tests.Integrations) and [Npgsql.Tvp](https://github.com/0UserName/Npgsql.Tvp/tree/master/Npgsql.Tvp.Tests).