using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundPanel : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;
    public Slider voiceSlider;

    public TMP_Text masterValueText;
    public TMP_Text bgmValueText;
    public TMP_Text seValueText;
    public TMP_Text voiceValueText;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(v => OnVolumeChanged());
        bgmSlider.onValueChanged.AddListener(v => OnVolumeChanged());
        seSlider.onValueChanged.AddListener(v => OnVolumeChanged());
        voiceSlider.onValueChanged.AddListener(v => OnVolumeChanged());
    }
    void OnVolumeChanged()
    {
        float m = masterSlider.value;
        float b = bgmSlider.value;
        float s = seSlider.value;
        float v = voiceSlider.value;

        masterValueText.text = m.ToString("0.00");
        bgmValueText.text = b.ToString("0.00");
        seValueText.text = s.ToString("0.00");
        voiceValueText.text = v.ToString("0.00");

        GameManager.Instance.SoundManager.SetVolumes(m, b, s, v);
    }

    public void UpdateDisplay()
    {
        var sm = GameManager.Instance.SoundManager;

        masterSlider.value = sm.masterVolume;
        bgmSlider.value = sm.bgmVolume;
        seSlider.value = sm.seVolume;
        voiceSlider.value = sm.voiceVolume;

        masterValueText.text = sm.masterVolume.ToString("0.00");
        bgmValueText.text = sm.bgmVolume.ToString("0.00");
        seValueText.text = sm.seVolume.ToString("0.00");
        voiceValueText.text = sm.voiceVolume.ToString("0.00");
    }

    public void ClosePanel()
    {
        GameManager.Instance.SoundManager.saveVolume();
        this.gameObject.SetActive(false);
    }
}
