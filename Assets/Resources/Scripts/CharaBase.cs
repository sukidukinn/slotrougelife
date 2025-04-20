using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharaBase
{
    public string id;
    public string name;
    public int maxHP = 100;
    public int currentHP = 100;
    public int attack = 10;
    public int defense = 5;

    // 配当表：成立役ごとのダメージや確率などを定義（仮構造）
    public PayoutTableData payoutTable;

    // 所有スタック（通常カード）
    public List<CardBase> stack = new();

    // 補助カード（UIに表示される）
    public List<CardBase> supportStack = new();

    public CharaBase Clone()
    {
        var copy = (CharaBase)this.MemberwiseClone();
        copy.stack = new List<CardBase>();
        foreach (var card in stack)
            copy.stack.Add(card.Clone());

        copy.supportStack = new List<CardBase>();
        foreach (var card in supportStack)
            copy.supportStack.Add(card.Clone());
    // payoutTable が ScriptableObject 型なら参照そのままでOK
    // 旧: copy.payoutTable = new List<StringPair>(payoutTable);
        return copy;
    }

    public void ApplyDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
    }

    public bool IsDead => currentHP <= 0;
}
