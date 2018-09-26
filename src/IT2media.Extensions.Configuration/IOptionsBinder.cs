using System;
using System.Collections.Generic;

namespace IT2media.Extensions.Configuration
{
    public interface IOptionsBinder
    {
        IEnumerable<Type> GetOptionModels();
        void RegisterOption<T>(Action<Type, object> registerInstance) where T : class, new();
        Action<T> BindAction<T>() where T : class, new();
    }
}