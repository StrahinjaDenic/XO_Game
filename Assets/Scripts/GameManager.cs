using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int CurrentPlayer { get; private set; } = 1;
    public int[] Board { get; private set; } = new int[9];
    public int Player1Moves { get; private set; }
    public int Player2Moves { get; private set; }
    public float MatchDuration { get; private set; }
    public bool GameOver { get; private set; }
    public int[] LastWinCombo { get; private set; }

    static readonly int[][] winCombos = new int[][]
    {
        new int[] {0,1,2}, new int[] {3,4,5}, new int[] {6,7,8},
        new int[] {0,3,6}, new int[] {1,4,7}, new int[] {2,5,8},
        new int[] {0,4,8}, new int[] {2,4,6}
    };

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!GameOver)
            MatchDuration += Time.deltaTime;
    }

    public void StartNewGame()
    {
        Board = new int[9];
        CurrentPlayer = 1;
        Player1Moves = 0;
        Player2Moves = 0;
        MatchDuration = 0f;
        GameOver = false;
    }

    public int PlaceMark(int cellIndex)
    {
        if (Board[cellIndex] != 0 || GameOver) return -1;

        Board[cellIndex] = CurrentPlayer;
        if (CurrentPlayer == 1) Player1Moves++;
        else Player2Moves++;

        AudioManager.Instance.PlayPlacement();

        int winner = CheckWinner();
        if (winner != -1)
        {
            GameOver = true;
            StatsManager.Instance.RecordResult(winner, MatchDuration);
            AudioManager.Instance.PlayWin();
            return winner;
        }

        CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
        return -1;
    }

    int CheckWinner()
    {
        foreach (var combo in winCombos)
        {
            if (Board[combo[0]] != 0 &&
                Board[combo[0]] == Board[combo[1]] &&
                Board[combo[1]] == Board[combo[2]])
            {
                LastWinCombo = combo;
                return Board[combo[0]];
            }
        }

        foreach (int cell in Board)
            if (cell == 0) return -1;

        return 0;
    }

    public void ReturnToMenu() => SceneManager.LoadScene(0);
}