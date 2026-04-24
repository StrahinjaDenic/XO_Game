using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    [Header("Theme Sprites")]
    public Sprite[] xSprites; // 3 sprite-a za X (XO_0, XO_2, XO_4)
    public Sprite[] oSprites; // 3 sprite-a za O (XO_1, XO_3, XO_5)

    public int CurrentTheme { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentTheme = PlayerPrefs.GetInt("Theme", 0);
    }

    public void SetTheme(int index)
    {
        CurrentTheme = index;
        PlayerPrefs.SetInt("Theme", index);
    }

    public Sprite GetX() => xSprites[CurrentTheme];
    public Sprite GetO() => oSprites[CurrentTheme];
}