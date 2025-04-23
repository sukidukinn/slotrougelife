using UnityEngine;
using System.Collections.Generic; // ← List<>
using System; // ← Guid
using System.Linq; // ← ToList()

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private SharedGameState sharedState = new();
    public SharedGameState SharedGameState { get; private set; } = new();
    public SoundManager SoundManager { get; private set; } = new();
    public SoundPanel soundPanelPrefab; 
    private SoundPanel soundPanelInstance;
    private ISaveDataStorage<GameSaveData> saveStorage;
    public List<CardBase> masterCardList = new();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        SoundManager.Initialize(); // AudioSource 初期化

        //外部データ読込
        saveStorage = new JsonFileSaveDataStorage<GameSaveData>();
        saveStorage.Load("game_data", data =>
        {
            if (data != null)
            {
                GameManager.Instance.SoundManager?.SetVolumes(
                    data.masterVolume, data.bgmVolume, data.seVolume, data.voiceVolume
                );
                //Debug.Log("LoadVolume: "+GameManager.Instance.SoundManager.ToString());
            }
        });

        BGM1ButtonEvent();
    }
    public void SaveGame(GameSaveData currentData)
    {
        saveStorage.Save("game_data", currentData, () =>
        {
            Debug.Log("保存完了！");
        });
    }
    public void SaveSoundVolume(float master, float bgm, float se, float voice)
    {
        saveStorage.Load("game_data", existing =>
        {
            if (existing == null)
            {
                existing = new GameSaveData(); // デフォルト生成
            }

            // 音量だけ上書き
            existing.masterVolume = master;
            existing.bgmVolume = bgm;
            existing.seVolume = se;
            existing.voiceVolume = voice;

            saveStorage.Save("game_data", existing, () =>
            {
                Debug.Log("音量設定のみ保存されました。");
            });

        }, defaultValue: new GameSaveData());
    }
        public void BGM1ButtonEvent()
        {
            Instance.SoundManager.PlayBGM("Sound/bgm001");
        }
        public void SE1ButtonEvent()
        {
            Instance.SoundManager.PlaySE("Sound/se003");
        }
        public void VOICE1ButtonEvent()
        {
            Instance.SoundManager.PlayVoice("Sound/card0001");
        }
        public void ToggleSoundPanel()
        {
            if (soundPanelInstance == null)
            {
                var canvas = FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    soundPanelInstance = Instantiate(soundPanelPrefab, canvas.transform);
                    soundPanelInstance.UpdateDisplay();
                }
            } else {
                bool current = soundPanelInstance.gameObject.activeSelf;
                soundPanelInstance.gameObject.SetActive(!current);
            }
        }
}