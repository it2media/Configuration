using System;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace IT2media.Extensions.Configuration.xUnitTests
{
    public class OptionsBinderTest
    {
        [Fact]
        public void RegisterOptionTest()
        {
            var configuration = A.Fake<IConfiguration>();
            IOptionsBinder optionsBinder = new OptionsBinder(configuration);

            Type testType = null;
            object testObject = null;

            optionsBinder.RegisterOption<TestOption>((type, o) => { testType = type;
                testObject = o;
            });

            Assert.True(testType == typeof(IOptions<TestOption>));

            Assert.NotNull((IOptions<TestOption>) testObject);
            Assert.NotNull(((IOptions<TestOption>) testObject).Value);
        }

        [Fact]
        public void BindActionTest()
        {
            var configuration = A.Fake<IConfiguration>();
            IOptionsBinder optionsBinder = new OptionsBinder(configuration);

            var action = optionsBinder.BindAction<TestOption>();

            Assert.NotNull(action);
        }
    }

    public class TestOption { }
}
