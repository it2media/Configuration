using System;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace IT2media.Extensions.Configuration.xUnitTests
{
    public class ConfigurationContainerRegistryExtensionsTest
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionIfActionNoActionIsProvided()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var configuration = A.Fake<IConfiguration>();
                Action<Type, object> registerInstance = null;
                // ReSharper disable once ExpressionIsAlwaysNull
                configuration.InitOptions(registerInstance);
            });
        }

        [Fact]
        public void ShouldNotThrowExceptionIfEmptyActionIsProvided()
        {
            var configuration = A.Fake<IConfiguration>();
            configuration.InitOptions(EmptyAction);

            Assert.True(configuration != null);
        }

        private static void EmptyAction(Type type, object obj)
        {

        }
    }
}
