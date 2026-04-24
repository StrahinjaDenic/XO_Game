using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    public int TotalGames { get; private set; }
    public int Player1Wins { get; private set; }
    public int Player2Wins { get; private set; }
    public int Draws { get; private set; }
    public float TotalDuration { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        TotalGames = PlayerPrefs.GetInt("TotalGames", 0);
        Player1Wins = PlayerPrefs.GetInt("Player1Wins", 0);
        Player2Wins = PlayerPrefs.GetInt("Player2Wins", 0);
        Draws = PlayerPrefs.GetInt("Draws", 0);
        TotalDuration = PlayerPrefs.GetFloat("TotalDuration", 0f);
    }

    public void RecordResult(int winner, float duration)
    {
        TotalGames++;
        TotalDuration += duration;

        if (winner == 1) Player1Wins++;
        else if (winner == 2) Player2Wins++;
        else Draws++;

        PlayerPrefs.SetInt("TotalGames", TotalGames);
        PlayerPrefs.SetInt("Player1Wins", Player1Wins);
        PlayerPrefs.SetInt("Player2Wins", Player2Wins);
        PlayerPrefs.SetInt("Draws", Draws);
        PlayerPrefs.SetFloat("TotalDuration", TotalDuration);
        PlayerPrefs.Save();
    }

    public float GetAverageDuration()
    {
        return TotalGames == 0 ? 0f : TotalDuration / TotalGames;
    }
}