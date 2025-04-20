using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PayoutTable", menuName = "Combat/PayoutTable", order = 1)]
public class PayoutTableData : ScriptableObject
{
    public List<PayoutEntry> entries = new();

    // 敵キャラ用：乱数と攻撃力からダメージ算出
    public PayoutEntry GetRandomYaku(System.Random rng)
    {
        float roll = (float)rng.NextDouble();
        float total = 0f;

        foreach (var entry in entries)
        {
            total += entry.probability;
            if (roll < total)
                return entry;
        }

        return entries.Count > 0 ? entries[entries.Count - 1] : null;
    }

    public int CalculateDamage(PayoutEntry entry, int attackStat)
    {
        return attackStat * entry.payoutAmount;
    }
}
