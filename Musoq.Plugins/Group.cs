﻿using System;
using System.Collections.Generic;

namespace FQL.Plugins
{
    public class Group
    {
        public Group(Group parent, string[] fieldNames, object[] values)
        {
            Parent = parent;

            for (var i = 0; i < fieldNames.Length; i++) Values.Add(fieldNames[i], values[i]);
        }

        public Group Parent { get; }
        public int Count { get; private set; }

        private IDictionary<string, object> Values { get; } = new Dictionary<string, object>();
        private IDictionary<string, Func<object, object>> Converters { get; } = new Dictionary<string, Func<object, object>>();

        public void Hit()
        {
            Count += 1;
        }

        public T GetValue<T>(string name)
        {
            if (!Values.ContainsKey(name))
                throw new KeyNotFoundException(name);

            if (Converters.ContainsKey(name))
                return (T) Converters[name](Values[name]);

            return (T) Values[name];
        }

        public T GetRawValue<T>(string name)
        {
            if (!Values.ContainsKey(name))
                throw new KeyNotFoundException(name);

            return (T)Values[name];
        }

        public T GetOrCreateValue<T>(string name, T defValue = default(T))
        {
            if (!Values.ContainsKey(name))
                Values.Add(name, defValue);

            return (T)Values[name];
        }

        public void SetValue<T>(string name, T value)
        {
            Values[name] = value;
        }

        public TR GetOrCreateValueWithConverter<T, TR>(string name, T value, Func<object, object> converter)
        {
            if (!Values.ContainsKey(name))
                Values.Add(name, value);

            if(!Converters.ContainsKey(name))
                Converters.Add(name, converter);

            return (TR) Converters[name](Values[name]);
        }
    }
}