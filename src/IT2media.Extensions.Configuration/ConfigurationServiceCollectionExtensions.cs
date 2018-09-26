using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IT2media.Extensions.Configuration
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection InitOptionModels(this IConfiguration configuration, IServiceCollection serviceCollection)
        {
            IOptionsBinder optionsBinder = new OptionsBinder(configuration);

            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            var optionModels = optionsBinder.GetOptionModels();

            foreach (var optionModel in optionModels)
            {
                var actionMethodInfo = typeof(OptionsBinder).GetMethod(nameof(OptionsBinder.BindAction));
                var genericActionMethodInfo = actionMethodInfo?.MakeGenericMethod(optionModel);
                var actionToPass = genericActionMethodInfo?.Invoke(optionsBinder, null);

                var methodInfo = typeof(OptionsServiceCollectionExtensions).GetMethods().First(mi => mi.Name == "Configure");
                var generic = methodInfo?.MakeGenericMethod(optionModel);
                generic?.Invoke(serviceCollection, new[] { serviceCollection, actionToPass });
            }

            return serviceCollection;
        }
    }
}
