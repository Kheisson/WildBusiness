using System;
using System.Collections.Generic;

namespace Infra
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            var type = typeof(T);
            Services.TryAdd(type, service);
        }

        public static T GetService<T>()
        {
            var type = typeof(T);
            return Services.TryGetValue(type, out var service) ? (T)service : default(T);
        }
    }
}