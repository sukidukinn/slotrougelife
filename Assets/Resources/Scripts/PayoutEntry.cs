using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class PayoutEntry
{
    public string yakuName;       // 役名（例："Bell", "Cherry", "Miss"）
    public float probability;     // 出現確率（合計100%にしなくてもOK）
    public int payoutAmount;      // 枚数（=倍率計算に使う）
}

