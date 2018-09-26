using System;

namespace IT2media.Extensions.Configuration
{
    public interface IOptionsBinder
    {
        void InitOptions(Action<Type, object> registerInstance);
    }
}