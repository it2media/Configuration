using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IT2media.Extensions.Configuration
{
    public class OptionsBinder : IOptionsBinder
    {
        public static string IdentifierForOptionModels { get; set; } = "Options";

        private readonly IConfiguration _configuration;

        public OptionsBinder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Type> GetOptionModels()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace != null && t.IsClass && t.Namespace.EndsWith(IdentifierForOptionModels) && t.Name.EndsWith(IdentifierForOptionModels) && !t.Name.Equals(IdentifierForOptionModels));
        }

        public void RegisterOption<T>(Action<Type, object> registerInstance) where T : class, new()
        {
            var instance = CreateConfiguredInstance<T>();
            registerInstance(typeof(IOptions<T>), instance);
        }

        public Action<T> BindAction<T>() where T : class, new()
        {
            return GetSectionAndBind;
        }

        #region private

        private IOptions<T> CreateConfiguredInstance<T>() where T: class, new()
        {
            var optionsModel = (T)Activator.CreateInstance(typeof(T));
            GetSectionAndBind(optionsModel);
            return Options.Create(optionsModel);
        }

        private void GetSectionAndBind<T>(T optionsModel) where T : class, new()
        {
            var key = $"{IdentifierForOptionModels}:{typeof(T).Name}";
            var configurationSection = _configuration.GetSection(key);
            configurationSection.Bind(optionsModel);
        }
        #endregion
    }
}
