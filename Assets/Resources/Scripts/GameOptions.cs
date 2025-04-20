[System.Serializable]
public class GameOptions
{
    public float bgmVolume = 1f;
    public float seVolume = 1f;
    public string language = "jp";

    public void Load() { /* TODO: PlayerPrefsなどから */ }
    public void Save() { /* TODO: PlayerPrefsなどへ */ }
}
