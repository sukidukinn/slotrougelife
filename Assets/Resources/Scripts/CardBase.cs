using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class CardBase
{
    public string id;
    public string name;
    public string description;
    public string type;
    public int duplicatePoint; //重複代替ポイント

    public List<string> selectableOptions = new(); // ユーザーが選べるオプション（例: 安定型, SP型など）
    public string selectedOption = "";             // 実際に選択されたオプション

    // CardButtonUIのUI要素名と一致させた画像パス
    public string cardImage = "Images/monster1";
    public string cardFlameImage = "Images/card_flame1";
    public string backgroundImage = "Images/background1";
    public string detailImage = "Images/monster1";

    public Sprite icon; // 実行時キャッシュ用途（任意）
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
        seClip = Resources.Load<AudioClip>(sePath);
    }

    public static Sprite LoadSpriteByPath(string pathWithOptionalSpriteName)
    {
        if (string.IsNullOrEmpty(pathWithOptionalSpriteName)) return null;

        // 例: "Images/slotSymbol:cherry"
        var parts = pathWithOptionalSpriteName.Split(':');

        if (parts.Length == 2)
        {
            var sheetPath = parts[0];
            var spriteName = parts[1];

            var sprites = Resources.LoadAll<Sprite>(sheetPath);
            return sprites.FirstOrDefault(s => s.name == spriteName);
        }
        else
        {
            // 単体スプライト（画像全体）
            return Resources.Load<Sprite>(pathWithOptionalSpriteName);
        }
    }
}
