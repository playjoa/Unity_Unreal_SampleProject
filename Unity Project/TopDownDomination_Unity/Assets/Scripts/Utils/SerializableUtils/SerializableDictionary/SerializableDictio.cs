using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.SerializableUtils
{
    [Serializable]
    public class SerializableDictio<TKeyValue, TValueType>
    {
        [SerializeField] private SerializableDictioValue<TKeyValue, TValueType>[] dictionaryValues;

        public bool Initialized { get; private set; }

        private Dictionary<TKeyValue, TValueType> _generatedDictionary;
        public Dictionary<TKeyValue, TValueType> GeneratedDictionary
        {
            get
            {
                if (!Initialized)
                    Initiate();

                return _generatedDictionary;
            }
        }

        public void Initiate()
        {
            _generatedDictionary = new Dictionary<TKeyValue, TValueType>();
            
            foreach (var value in dictionaryValues)
            {
                if (value == null) continue;

                if (_generatedDictionary.ContainsKey(value.Key))
                {
                    Debug.LogWarning($"Duplicated Key Values for key {value.Key} when generating Serialized Dictionary");
                    continue;
                }

                _generatedDictionary.Add(value.Key, value.Value);
            }

            Initialized = true;
        }

        public bool TryGetValue(TKeyValue keyValue, out TValueType foundValue)
        {
            if (!Initialized)
                Initiate();

            return GeneratedDictionary.TryGetValue(keyValue, out foundValue);
        }

        public bool ContainsKey(TKeyValue keyValue)
        {
            if (!Initialized)
                Initiate();

            return GeneratedDictionary.ContainsKey(keyValue);
        }
    }
}