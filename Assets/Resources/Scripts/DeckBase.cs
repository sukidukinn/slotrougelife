using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections.Generic;

public abstract class DeckBase
{
    protected List<CardBase> drawPile = new();
    protected CardBase bossCard;

    protected int seed;
    protected System.Random rng;

    public void Initialize(int seed)
    {
        this.seed = seed;
        rng = new System.Random(seed);
        Shuffle();
    }

    public void AddCard(CardBase card)
    {
        drawPile.Add(card.Clone());
    }

    public void SetBossCard(CardBase card)
    {
        bossCard = card.Clone();
    }

    public void Shuffle()
    {
        int n = drawPile.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int j = rng.Next(i, n);
            var temp = drawPile[i];
            drawPile[i] = drawPile[j];
            drawPile[j] = temp;
        }
    }

    public List<CardBase> Draw(int count)
    {
        List<CardBase> drawn = new();

        for (int i = 0; i < count && drawPile.Count > 0; i++)
        {
            drawn.Add(drawPile[0]);
            drawPile.RemoveAt(0);
        }

        // デッキが足りない場合、ボスカードを追加（1枚だけ）
        if (drawPile.Count == 0 && bossCard != null)
        {
            drawn.Add(bossCard.Clone());
        }

        return drawn;
    }

    public int Remaining => drawPile.Count;
}
