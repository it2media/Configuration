using System;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace IT2media.Extensions.Configuration.xUnitTests
{
    public class OptionsBinderTest
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionIfActionNoActionIsProvided()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var configuration = A.Fake<IConfiguration>();
                IOptionsBinder optionsBinder = new OptionsBinder(configuration);
                optionsBinder.InitOptions(null);
            });
        }

        [Fact]
        public void ShouldNotThrowArgumentNullExceptionIfEmptyActionIsProvided()
        {
            var configuration = A.Fake<IConfiguration>();
            IOptionsBinder optionsBinder = new OptionsBinder(configuration);
            optionsBinder.InitOptions(EmptyAction);

            Assert.True(optionsBinder != null);
        }

        private static void EmptyAction(Type type, object obj)
        {

        }
    }
}
