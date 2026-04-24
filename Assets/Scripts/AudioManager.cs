using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip bgmClip;
    public AudioClip clickClip;
    public AudioClip placementClip;
    public AudioClip winClip;
    public AudioClip popupClip;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ApplySettings();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void ApplySettings()
    {
        if (SettingsManager.Instance.MusicEnabled)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    public void PlayClick() { if (SettingsManager.Instance.SfxEnabled) sfxSource.PlayOneShot(clickClip); }
    public void PlayPlacement() { if (SettingsManager.Instance.SfxEnabled) sfxSource.PlayOneShot(placementClip); }
    public void PlayWin() { if (SettingsManager.Instance.SfxEnabled) sfxSource.PlayOneShot(winClip); }
    public void PlayPopup() { if (SettingsManager.Instance.SfxEnabled) sfxSource.PlayOneShot(popupClip); }
}