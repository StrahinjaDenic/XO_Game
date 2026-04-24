using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlacementEffect : MonoBehaviour
{
    public static PlacementEffect Instance { get; private set; }

    [SerializeField] Sprite flareSprite;
    [SerializeField] Canvas rootCanvas;

    void Awake() => Instance = this;

    public void SpawnFlare(RectTransform cellRect)
    {
        StartCoroutine(FlareRoutine(cellRect.position));
    }

    IEnumerator FlareRoutine(Vector3 worldPos)
    {
        GameObject go = new GameObject("Flare");
        go.transform.SetParent(rootCanvas.transform, false);

        RectTransform rt = go.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(110f, 110f);
        rt.position = worldPos;

        Image img = go.AddComponent<Image>();
        img.sprite = flareSprite;
        img.raycastTarget = false;

        float duration = 0.35f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rt.localScale = Vector3.one * Mathf.Lerp(0.1f, 1.6f, t);
            img.color = new Color(1f, 0.95f, 0.7f, Mathf.Lerp(0.9f, 0f, t));
            yield return null;
        }

        Destroy(go);
    }
}