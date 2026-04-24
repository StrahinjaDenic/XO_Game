using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public bool MusicEnabled { get; private set; }
    public bool SfxEnabled { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        SfxEnabled = PlayerPrefs.GetInt("SfxEnabled", 1) == 1;
    }

    public void SetMusic(bool value)
    {
        MusicEnabled = value;
        PlayerPrefs.SetInt("MusicEnabled", value ? 1 : 0);
        AudioManager.Instance.ApplySettings();
    }

    public void SetSfx(bool value)
    {
        SfxEnabled = value;
        PlayerPrefs.SetInt("SfxEnabled", value ? 1 : 0);
        AudioManager.Instance.ApplySettings();
    }
}