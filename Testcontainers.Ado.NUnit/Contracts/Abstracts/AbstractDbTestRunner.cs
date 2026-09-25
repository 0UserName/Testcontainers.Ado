using NUnit.Framework;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado.NUnit.Contracts.Abstracts
{
    public abstract class AbstractDbTestRunner<TDbTestRunner>(TDbTestRunner runner) where TDbTestRunner : IDbTestRunner
    {
        protected TDbTestRunner Runner
        {
            get => runner;
        }

        [OneTimeSetUp]
        protected virtual async Task StartAsync()
        {
            await Runner.StartAsync();
        }

        [OneTimeTearDown]
        protected virtual async Task StopAsync()
        {
            await Runner.DisposeAsync();
        }
    }
}