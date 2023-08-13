using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//影片中用來存關鍵道具的，這裡用來存科技樹碎片

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.keys.Clear();

        if(keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, bot the amout of keys (" + keys.Count + ") does not match the number of values (" + values.Count + ") which indicates that something went wrong");
        }

        for(int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

}
