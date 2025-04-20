using UnityEngine;
using System.Collections.Generic; // ← List<>
using System; // ← Guid
using System.Linq; // ← ToList()

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<CardBase> masterCardList = new();
    public GameOptions options = new();
    public AudioManager audioManager;
    private SharedGameState sharedState = new();

    public SharedGameState SharedGameState => sharedState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        // システム初期化のみ
        options.Load();
    }
}