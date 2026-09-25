using NUnit.Framework;

using System.Threading.Tasks;

using Testcontainers.Ado.Contracts;

namespace Testcontainers.Ado.NUnit.Contracts.Abstracts
{
    public abstract class AbstractDbTests<TDbTestRunner>(string image, string schemaDir) where TDbTestRunner : IDbTestRunner, new()
    {
        private readonly TDbTestRunner _runner = new TDbTestRunner();

        protected TDbTestRunner Runner
        {
            get => _runner;
        }

        [OneTimeSetUp]
        protected virtual async Task StartAsync()
        {
            await Runner.StartAsync(image, schemaDir);
        }

        [OneTimeTearDown]
        protected virtual async Task StopAsync()
        {
            await Runner.DisposeAsync();
        }
    }
}