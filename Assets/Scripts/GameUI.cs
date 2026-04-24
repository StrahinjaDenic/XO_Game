using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI player1MovesText;
    [SerializeField] TextMeshProUGUI player2MovesText;

    [Header("Game Over Popup")]
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] TextMeshProUGUI gameOverResultText;
    [SerializeField] TextMeshProUGUI gameOverDurationText;

    [Header("Settings Popup")]
    [SerializeField] GameObject settingsPopup;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle sfxToggle;

    void Awake() => Instance = this;

    void Start()
    {
        gameOverPopup.SetActive(false);
        settingsPopup.SetActive(false);
        musicToggle.isOn = SettingsManager.Instance.MusicEnabled;
        sfxToggle.isOn = SettingsManager.Instance.SfxEnabled;
        GameManager.Instance.StartNewGame();
    }

    void Update()
    {
        if (GameManager.Instance.GameOver) return;
        float t = GameManager.Instance.MatchDuration;
        timerText.text = string.Format("{0:00}:{1:00}", (int)t / 60, (int)t % 60);
        player1MovesText.text = $"P1: {GameManager.Instance.Player1Moves}";
        player2MovesText.text = $"P2: {GameManager.Instance.Player2Moves}";
    }

    public void ShowGameOver(int winner)
    {
        StartCoroutine(ShowGameOverDelayed(winner));
    }

    IEnumerator ShowGameOverDelayed(int winner)
    {
        yield return new WaitForSeconds(1.8f);
        StrikeEffect.Instance?.HideStrike();
        gameOverPopup.SetActive(true);
        AudioManager.Instance.PlayPopup();
        float d = GameManager.Instance.MatchDuration;
        gameOverResultText.text = winner == 0 ? "Draw!" : $"Player {winner} Wins!";
        gameOverDurationText.text = string.Format("Duration: {0:00}:{1:00}", (int)d / 60, (int)d % 60);
    }

    public void OnRetry()
    {
        AudioManager.Instance.PlayClick();
        gameOverPopup.SetActive(false);
        StrikeEffect.Instance?.HideStrike();
        GameManager.Instance.StartNewGame();
        foreach (var cell in FindObjectsByType<BoardCell>(FindObjectsSortMode.None))
            cell.ResetCell();
    }

    public void OnExit()
    {
        AudioManager.Instance.PlayClick();
        GameManager.Instance.ReturnToMenu();
    }

    public void OnSettingsOpen()
    {
        AudioManager.Instance.PlayPopup();
        settingsPopup.SetActive(true);
    }

    public void OnSettingsClose()
    {
        AudioManager.Instance.PlayClick();
        settingsPopup.SetActive(false);
    }

    public void OnMusicToggle(bool value) => SettingsManager.Instance.SetMusic(value);
    public void OnSfxToggle(bool value) => SettingsManager.Instance.SetSfx(value);
}