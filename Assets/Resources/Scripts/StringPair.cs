using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StringPair
{
    public string key;
    public string value;

    public StringPair(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}
