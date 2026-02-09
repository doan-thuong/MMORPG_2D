using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
{
    [SerializeField] private List<K> keys = new List<K>();
    [SerializeField] private List<V> values = new List<V>();

    // Lưu trữ dữ liệu từ Dictionary vào List để Unity có thể lưu file/hiển thị
    public void OnBeforeSerialize()
    {
        // Chỉ cập nhật List khi Dictionary có dữ liệu (tránh ghi đè khi đang load)
        // Hoặc khi số lượng thay đổi do code tác động
        keys.Clear();
        values.Clear();

        foreach (var pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // Đổ dữ liệu từ List vào Dictionary để dùng trong code
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Trọng yếu: Số lượng Key và Value không khớp!");
            return;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            // QUAN TRỌNG: Nếu trùng Key trên Inspector, Unity sẽ không crash 
            // mà chỉ bỏ qua hoặc ghi đè, giúp bạn có cơ hội sửa lại Key đó.
            if (keys[i] != null)
            {
                this[keys[i]] = values[i];
            }
        }
    }
}