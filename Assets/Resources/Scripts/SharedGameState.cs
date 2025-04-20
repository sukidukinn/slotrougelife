using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedGameState
{
    private DungeonDeck currentDeck;
    private List<CardBase> lastRunRewards = new();
    private string lastMessage = "";

    public void SetDeck(DungeonDeck deck)
    {
        currentDeck = deck;
    }

    public DungeonDeck GetDeck()
    {
        return currentDeck;
    }

    public void SetReward(List<CardBase> rewards, string message = "")
    {
        lastRunRewards = new List<CardBase>(rewards);
        lastMessage = message;
    }

    public List<CardBase> GetRewards()
    {
        return lastRunRewards;
    }

    public string GetMessage()
    {
        return lastMessage;
    }

    public void Clear()
    {
        currentDeck = null;
        lastRunRewards.Clear();
        lastMessage = "";
    }
}