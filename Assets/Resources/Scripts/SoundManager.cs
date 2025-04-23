using UnityEngine;
using System.Collections.Generic;

public class SoundManager
{
    private AudioSource bgmSource;
    private AudioSource seSource;
    private AudioSource voiceSource;

    private Dictionary<string, AudioClip> loadedClips = new();

    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float seVolume = 1f;
    public float voiceVolume = 1f;

    public void Initialize()
    {
        GameObject root = new GameObject("SoundManager_AudioSources");
        Object.DontDestroyOnLoad(root);

        bgmSource = root.AddComponent<AudioSource>();
        seSource = root.AddComponent<AudioSource>();
        voiceSource = root.AddComponent<AudioSource>();

        bgmSource.loop = true;
        seSource.loop = false;
        voiceSource.loop = false;
    }

    public void PlayBGM(string path)
    {
        var clip = LoadClip(path);
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.volume = bgmVolume * masterVolume;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(string path)
    {
        var clip = LoadClip(path);
        if (clip != null)
        {
            seSource.PlayOneShot(clip, seVolume * masterVolume);
        }
    }

    public void PlayVoice(string path)
    {
        var clip = LoadClip(path);
        if (clip != null)
        {
            voiceSource.PlayOneShot(clip, voiceVolume * masterVolume);
        }
    }

    private AudioClip LoadClip(string path)
    {
        if (loadedClips.TryGetValue(path, out var clip)) return clip;

        clip = Resources.Load<AudioClip>(path);
        if (clip != null) loadedClips[path] = clip;
        else Debug.LogWarning($"サウンドファイルが見つかりません: {path}");

        return clip;
    }

    public void SetVolumes(float master, float bgm, float se, float voice)
    {
        // 音量を設定する
        masterVolume = master;
        bgmVolume = bgm;
        seVolume = se;
        voiceVolume = voice;
           
        bgmSource.volume = bgmVolume * masterVolume;
    }

    public void saveVolume(){
        GameManager.Instance.SaveSoundVolume(masterVolume, bgmVolume, seVolume, voiceVolume); 
    }
    public override string ToString()
    {
        return $"Master: {masterVolume:0.00}, BGM: {bgmVolume:0.00}, SE: {seVolume:0.00}, Voice: {voiceVolume:0.00}";
    }
}
