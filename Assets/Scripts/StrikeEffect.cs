using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StrikeEffect : MonoBehaviour
{
    public static StrikeEffect Instance { get; private set; }

    [SerializeField] RectTransform strikeLine;

    void Awake() => Instance = this;

    void Start() => strikeLine.gameObject.SetActive(false);

    public void ShowStrike(int[] combo, RectTransform[] cells)
    {
        StartCoroutine(AnimateStrike(combo, cells));
    }

    public void HideStrike()
    {
        StopAllCoroutines();
        strikeLine.gameObject.SetActive(false);
    }

    IEnumerator AnimateStrike(int[] combo, RectTransform[] cells)
    {
        strikeLine.gameObject.SetActive(true);

        Vector3 startPos = cells[combo[0]].position;
        Vector3 endPos = cells[combo[2]].position;

        Vector3 center = (startPos + endPos) / 2f;
        Vector2 direction = endPos - startPos;
        float length = direction.magnitude + 180f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        strikeLine.position = center;
        strikeLine.sizeDelta = new Vector2(0, 12);
        strikeLine.localRotation = Quaternion.Euler(0, 0, angle);

        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            strikeLine.sizeDelta = new Vector2(Mathf.Lerp(0, length, t), 12);
            yield return null;
        }

        strikeLine.sizeDelta = new Vector2(length, 12);
        WinCelebrationEffect.Instance?.SpawnCelebration(cells, combo);
    }
}