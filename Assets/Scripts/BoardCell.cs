using UnityEngine;
using UnityEngine.UI;

public class BoardCell : MonoBehaviour
{
    [SerializeField] int cellIndex;

    Image markImage;
    static RectTransform[] allCells;

    void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>();
        markImage = images.Length > 1 ? images[1] : images[0];
        markImage.color = Color.clear;
    }

    void Start()
    {
        if (allCells == null) allCells = new RectTransform[9];
        allCells[cellIndex] = GetComponent<RectTransform>();
    }

    public void OnClick()
    {
        int result = GameManager.Instance.PlaceMark(cellIndex);
        if (result == -1 && GameManager.Instance.Board[cellIndex] == 0) return;

        markImage.sprite = GameManager.Instance.Board[cellIndex] == 1
            ? ThemeManager.Instance.GetX()
            : ThemeManager.Instance.GetO();
        markImage.color = Color.white;
        PlacementEffect.Instance?.SpawnFlare(GetComponent<RectTransform>());

        if (result > 0)
        {
            StrikeEffect.Instance?.ShowStrike(GameManager.Instance.LastWinCombo, allCells);
            GameUI.Instance.ShowGameOver(result);
        }
        else if (result == 0)
        {
            GameUI.Instance.ShowGameOver(result);
        }
    }

    public void ResetCell()
    {
        markImage.color = Color.clear;
    }
}