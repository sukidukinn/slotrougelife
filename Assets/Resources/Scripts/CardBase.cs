using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardBase
{
    public string id;
    public string name;
    public string description;
    public string type;
    public string iconPath;
    public Sprite icon;
    public string sePath;
    public AudioClip seClip;

    public int rarity;
    public int cost;
    public string instanceId;
    public int createdTurn;

    public Dictionary<string, Action<object>> callbacks = new();
    public Dictionary<string, string> parameters = new();

    public CharaBase linkedChara;

    public CardBase Clone()
    {
        var copy = (CardBase)this.MemberwiseClone();
        copy.instanceId = Guid.NewGuid().ToString();
        copy.callbacks = new Dictionary<string, Action<object>>(this.callbacks);
        copy.parameters = new Dictionary<string, string>(this.parameters);
        return copy;
    }

    public void LoadAssets()
    {
        icon = Resources.Load<Sprite>(iconPath);
        seClip = Resources.Load<AudioClip>(sePath);
    }
}
