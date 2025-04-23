using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.IO;
using System;

public class JsonFileSaveDataStorage<T> : ISaveDataStorage<T>
{
    private string GetPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, $"{key}.json");
    }

    public void Save(string key, T data, Action onSaved = null)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(key), json);
        Debug.Log($"[JsonStorage] {key} を保存しました");

        onSaved?.Invoke(); // コールバック呼び出し
    }

    public void Load(string key, Action<T> onLoaded, T defaultValue = default)
    {
        string path = GetPath(key);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            onLoaded?.Invoke(data);
        }
        else
        {
            Debug.Log($"[JsonStorage] {key} が存在しません。デフォルト値を返します");
            onLoaded?.Invoke(defaultValue);
        }
    }
}
