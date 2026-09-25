![Release workflow](https://github.com/0UserName/testcontainers.ado/actions/workflows/release.yml/badge.svg)



# Motivation

<div align="justify">

The library simplifies the creation of test projects for database driver-specific functionality, such as driver plugins and custom wrappers around driver-specific types used to send data to remote databases.

</div>



# Usage

<div align="justify">

Create a test class that inherits from `AbstractDbTests`. Use `TestFixture` to specify the required test runner type and provide the `image` and `schemaDir` arguments needed to manage the container lifecycle:

</div>



```csharp
[TestFixture(typeof(SqlServerTestRunner), "mcr.microsoft.com/mssql/server:2022-CU27-ubuntu-20.04", "Init")]
public sealed class InsertTests<TDbTestRunner>(string image, string schemaDir) : AbstractDbTests(image, schemaDir) where TDbTestRunner : IDbTestRunner, new()
{
    [Test]
    public async Task TestDataTableAsync()
    {
        DataTable value;

        // Initialize the test data.

        await Runner.ThatInsertAsync("dbo.table_valued_insert", value, value.Rows.Count);
    }
}
```



<div align="justify">

Test methods can then be implemented using standard `NUnit` assertion methods or a small set of `DbAsserts` methods provided by the library.

</div>



> [!NOTE]
> See additional examples in the following projects: [DbExtensions.Tvp](https://github.com/0UserName/DbExtensions.Tvp/tree/master/DbExtensions.Tvp.Tests.Integrations) and [Npgsql.Tvp](https://github.com/0UserName/Npgsql.Tvp/tree/master/Npgsql.Tvp.Tests).