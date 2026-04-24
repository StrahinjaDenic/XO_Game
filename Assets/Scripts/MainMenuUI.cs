using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Popups")]
    [SerializeField] GameObject themePopup;
    [SerializeField] GameObject statsPopup;
    [SerializeField] GameObject settingsPopup;
    [SerializeField] GameObject exitPopup;

    [Header("Stats Texts")]
    [SerializeField] TextMeshProUGUI totalGamesText;
    [SerializeField] TextMeshProUGUI player1WinsText;
    [SerializeField] TextMeshProUGUI player2WinsText;
    [SerializeField] TextMeshProUGUI drawsText;
    [SerializeField] TextMeshProUGUI avgDurationText;

    [Header("Settings")]
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle sfxToggle;

    [Header("Theme Buttons")]
    [SerializeField] UnityEngine.UI.Button btnClassic;
    [SerializeField] UnityEngine.UI.Button btnNeon;
    [SerializeField] UnityEngine.UI.Button btnNature;

    void Start()
    {
        themePopup.SetActive(false);
        statsPopup.SetActive(false);
        settingsPopup.SetActive(false);
        exitPopup.SetActive(false);

        musicToggle.isOn = SettingsManager.Instance.MusicEnabled;
        sfxToggle.isOn = SettingsManager.Instance.SfxEnabled;

         OnThemeSelect(ThemeManager.Instance.CurrentTheme);
    }

    public void OnPlay()
    {
        AudioManager.Instance.PlayClick();
        themePopup.SetActive(true);
        AudioManager.Instance.PlayPopup();
    }

    public void OnStart()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene(1);
    }

    public void OnStats()
    {
        AudioManager.Instance.PlayClick();
        totalGamesText.text = $"Total Games: {StatsManager.Instance.TotalGames}";
        player1WinsText.text = $"Player 1 Wins: {StatsManager.Instance.Player1Wins}";
        player2WinsText.text = $"Player 2 Wins: {StatsManager.Instance.Player2Wins}";
        drawsText.text = $"Draws: {StatsManager.Instance.Draws}";
        float avg = StatsManager.Instance.GetAverageDuration();
        avgDurationText.text = string.Format("Avg Duration: {0:00}:{1:00}", (int)avg / 60, (int)avg % 60);
        statsPopup.SetActive(true);
        AudioManager.Instance.PlayPopup();
    }

    public void OnSettings()
    {
        AudioManager.Instance.PlayClick();
        settingsPopup.SetActive(true);
        AudioManager.Instance.PlayPopup();
    }

    public void OnExit()
    {
        AudioManager.Instance.PlayClick();
        exitPopup.SetActive(true);
        AudioManager.Instance.PlayPopup();
    }

    public void OnExitConfirm()
    {
        AudioManager.Instance.PlayClick();
        Application.Quit();
    }

    public void CloseAll()
    {
        AudioManager.Instance.PlayClick();
        themePopup.SetActive(false);
        statsPopup.SetActive(false);
        settingsPopup.SetActive(false);
        exitPopup.SetActive(false);
    }

    public void OnMusicToggle(bool value) => SettingsManager.Instance.SetMusic(value);
    public void OnSfxToggle(bool value) => SettingsManager.Instance.SetSfx(value);

    public void OnThemeSelect(int index)
    {
        AudioManager.Instance.PlayClick();
        ThemeManager.Instance.SetTheme(index);

        btnClassic.image.color = Color.white;
        btnNeon.image.color = Color.white;
        btnNature.image.color = Color.white;

        UnityEngine.UI.Button[] btns = { btnClassic, btnNeon, btnNature };
        btns[index].image.color = new Color(0.6f, 0.9f, 0.6f);
    }
}