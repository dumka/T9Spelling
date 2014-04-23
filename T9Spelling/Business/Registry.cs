using System;
using System.Collections.Generic;
using T9Spelling.Properties;

namespace T9Spelling.Business
{
    public class Registry
    {
        private Registry()
        {
        }

        /// <summary>
        /// Sets object to registry.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">Object.</param>
        public static void Set<T>(T obj)
        {
            Instance.SetObject(obj);
        }

        /// <summary>
        /// Gets object from registry.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <returns>Object.</returns>
        public static T Get<T>()
        {
            return Instance.GetObject<T>();
        }

        private void SetObject<T>(T obj)
        {
            Type targetType = typeof(T);

            lock (thisLock)
            {
                if (!objectsMap.ContainsKey(targetType))
                    objectsMap.Add(targetType, obj);
            }
        }

        private T GetObject<T>()
        {
            Type targetType = typeof(T);
            if (!objectsMap.ContainsKey(targetType))
                throw new InvalidOperationException(
                    String.Format(Resources.RegistryDoesntContainObject, targetType));
            return (T) objectsMap[targetType];
        }

        private static Registry Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (thisLock)
                    {
                        if (instance == null)
                            instance = new Registry();
                    }
                }
                return instance;
            }
        }

        private static Registry instance;
        private readonly IDictionary<Type, object> objectsMap = new Dictionary<Type, object>();
        private static object thisLock = new object();
    }
}
