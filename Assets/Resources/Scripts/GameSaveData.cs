using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float seVolume = 1f;
    public float voiceVolume = 1f;
    public string playerName = "Hero";
    public string language = "jp";
    public int stageCleared = 1;
    public List<string> ownedCards = new();
}