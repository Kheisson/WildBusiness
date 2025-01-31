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
            
            if (!Services.TryAdd(type, service))
            {
                throw new InvalidOperationException($"Service of type {type.FullName} is already registered.");
            }
        }

        public static T GetService<T>()
        {
            var type = typeof(T);
            
            if (Services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            LogServiceNotFound(type);
            return default;
        }

        private static void LogServiceNotFound(Type type)
        {
            LlamaLog.LogWarning($"Service of type {type.FullName} is not registered.");
        }
    }
}