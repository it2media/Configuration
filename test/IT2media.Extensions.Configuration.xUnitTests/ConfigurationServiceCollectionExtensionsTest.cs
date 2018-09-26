using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IT2media.Extensions.Configuration.xUnitTests
{
    public class ConfigurationServiceCollectionExtensionsTest
    {
        [Fact]
        public void ShouldNotThrowException()
        {
            var configuration = A.Fake<IConfiguration>();
            var serviceCollection = A.Fake<IServiceCollection>();

            configuration.InitOptions(serviceCollection);

            Assert.True(configuration != null);
        }
    }
}
