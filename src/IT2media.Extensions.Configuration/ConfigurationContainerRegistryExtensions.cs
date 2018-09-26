using System;
using Microsoft.Extensions.Configuration;

namespace IT2media.Extensions.Configuration
{
    public static class ConfigurationContainerRegistryExtensions
    {
        public static IConfiguration InitOptions(this IConfiguration configuration, Action<Type, object> registerInstance)
        {
            IOptionsBinder optionsBinder = new OptionsBinder(configuration);

            if (registerInstance == null) throw new ArgumentNullException(nameof(registerInstance), new Exception("Please provide a Action which should register the IOptions<T> instance to your service registry!"));

            var optionModels = optionsBinder.GetOptionModels();

            foreach (var optionModel in optionModels)
            {
                var methodInfo = typeof(OptionsBinder).GetMethod("RegisterOption");
                var generic = methodInfo?.MakeGenericMethod(optionModel);
                generic?.Invoke(optionsBinder, new object[] {registerInstance});
            }

            return configuration;
        }
    }
}
