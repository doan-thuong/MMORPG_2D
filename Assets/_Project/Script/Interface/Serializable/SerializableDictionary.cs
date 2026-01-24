using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<KeyValuePairWrapper> serializedPairs = new();

    // Khi Unity serialize (trước khi save)
    public void OnBeforeSerialize()
    {
        serializedPairs.Clear();
        foreach (var kvp in this)
        {
            serializedPairs.Add(new KeyValuePairWrapper { key = kvp.Key, value = kvp.Value });
        }
    }

    // Khi Unity deserialize (khi load hoặc mở Inspector)
    public void OnAfterDeserialize()
    {
        Clear();
        foreach (var pair in serializedPairs)
        {
            if (pair.key != null && !ContainsKey(pair.key))
            {
                Add(pair.key, pair.value);
            }
        }
    }

    // Wrapper cho từng cặp key-value (để Inspector hiển thị)
    [Serializable]
    private class KeyValuePairWrapper
    {
        public TKey key;
        public TValue value;
    }
}