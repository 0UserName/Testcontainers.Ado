using NUnit.Framework;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado.NUnit.Contracts.Abstracts
{
    public abstract class AbstractDbTests<TDbRunner>(TDbRunner runner) where TDbRunner : IDbRunner
    {
        protected TDbRunner Runner
        {
            get => runner;
        }

        [OneTimeSetUp]
        protected virtual Task StartAsync()
        {
            return Runner.StartAsync();
        }

        [OneTimeTearDown]
        protected virtual ValueTask StopAsync()
        {
            return Runner.DisposeAsync();
        }
    }
}