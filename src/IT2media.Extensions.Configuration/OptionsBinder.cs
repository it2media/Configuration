using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IT2media.Extensions.Configuration
{
    public class OptionsBinder : IOptionsBinder
    {
        private const string IdentifierForOptionModels = "Options";

        private readonly IConfiguration _configuration;

        public OptionsBinder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InitOptions(Action<Type, object> registerInstance)
        {
            if (registerInstance == null) throw new ArgumentNullException(nameof(registerInstance), new Exception("Please provide a Action which should register the IOptions<T> instance to your service registry!"));

            var optionModels = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace != null && t.IsClass && t.Namespace.EndsWith(IdentifierForOptionModels) && t.Name.EndsWith(IdentifierForOptionModels) && !t.Name.Equals(IdentifierForOptionModels));

            foreach (var optionModel in optionModels)
            {
                var methodInfo = GetType().GetMethod(nameof(RegisterOption));
                var generic = methodInfo?.MakeGenericMethod(optionModel);
                generic?.Invoke(this, new object[] {registerInstance});
            }
        }

        public void RegisterOption<T>(Action<Type, object> registerInstance) where T : class, new()
        {
            var instance = CreateConfiguredInstance<T>();
            registerInstance(typeof(IOptions<T>), instance);
        }

        private IOptions<T> CreateConfiguredInstance<T>() where T: class, new()
        {
            var optionsModel = (T) Activator.CreateInstance(typeof(T));
            var key = $"{IdentifierForOptionModels}:{typeof(T).Name}";
            var configurationSection = _configuration.GetSection(key);
            configurationSection.Bind(optionsModel);
            return Options.Create(optionsModel);
        }
    }
}
