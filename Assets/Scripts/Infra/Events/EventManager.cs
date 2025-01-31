using System;
using System.Collections.Generic;

namespace Infra.Events
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, MulticastDelegate> EventTable = new Dictionary<Type, MulticastDelegate>();

        public static void AddListener<T>(Action<T> listener)
        {
            var eventType = typeof(T);
            
            if (EventTable.TryGetValue(eventType, out var delegates))
            {
                EventTable[eventType] = Delegate.Combine(delegates, listener) as MulticastDelegate;
            }
            else
            {
                EventTable[eventType] = listener;
            }
        }

        public static void RemoveListener<T>(Action<T> listener)
        {
            var eventType = typeof(T);

            if (!EventTable.TryGetValue(eventType, out var delegates)) return;

            if (delegates is not Action<T> currentDelegates) return;
            
            currentDelegates -= listener;
            
            if (currentDelegates == null)
            {
                EventTable.Remove(eventType);
            }
            else
            {
                EventTable[eventType] = currentDelegates;
            }
        }

        public static void TriggerEvent<T>(T eventArgs)
        {
            var eventType = typeof(T);

            if (!EventTable.TryGetValue(eventType, out var delegates)) return;
            
            var callback = delegates as Action<T>;
            callback?.Invoke(eventArgs);
        }
    }
}