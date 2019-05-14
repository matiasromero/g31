using System;
using System.Collections.Generic;

namespace HomeSwitchHome.Infrastructure.NHibernate
{
    public class SessionContext
    {
        private Dictionary<object, object> map = new Dictionary<object, object>();

        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }

        public bool Contains(object key)
        {
            return map.ContainsKey(key);
        }

        public T GetValueOrDefault<T>()
        {
            return Contains<T>() ? Get<T>() : default(T);
        }

        public T GetValueOrDefault<T>(object key)
        {
            return Contains(key) ? Get<T>(key) : default(T);
        }

        public T Get<T>()
        {
            return Get<T>(typeof(T));
        }

        public T Get<T>(object key)
        {
            lock (map)
            {
                if (!map.TryGetValue(key, out var ctx))
                {
                    throw new Exception("key not found in context");
                }

                return (T) ctx;
            }
        }

        public void Put<T>(T value)
        {
            Put(typeof(T), value);
        }

        public void Put<T>(object key, T value)
        {
            lock (map)
            {
                map[key] = value;
            }
        }
    }
}