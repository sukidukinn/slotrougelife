using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISaveDataStorage<T>
{
    void Save(string key, T data, Action onSaved = null);
    void Load(string key, Action<T> onLoaded, T defaultValue = default);
}