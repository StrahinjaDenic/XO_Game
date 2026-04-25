using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCelebrationEffect : MonoBehaviour
{
    public static WinCelebrationEffect Instance { get; private set; }

    [SerializeField] Sprite[] particleSprites;
    [SerializeField] Sprite[] flareFrames;
    [SerializeField] Canvas rootCanvas;
    [SerializeField] int particlesPerCell = 7;

    void Awake() => Instance = this;

    public void SpawnCelebration(RectTransform[] cells, int[] winCombo)
    {
        foreach (int idx in winCombo)
        {
            StartCoroutine(BurstFromCell(cells[idx]));
            StartCoroutine(FlareOnCell(cells[idx].position));
        }
    }

    IEnumerator BurstFromCell(RectTransform cell)
    {
        for (int i = 0; i < particlesPerCell; i++)
        {
            StartCoroutine(ParticleRoutine(cell.position));
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator FlareOnCell(Vector3 worldPos)
    {
        GameObject go = new GameObject("WinFlare");
        go.transform.SetParent(rootCanvas.transform, false);

        RectTransform rt = go.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(130f, 130f);
        rt.position = worldPos;

        Image img = go.AddComponent<Image>();
        img.raycastTarget = false;

        foreach (Sprite frame in flareFrames)
        {
            img.sprite = frame;
            img.color = Color.white;
            yield return new WaitForSeconds(0.06f);
        }

        Destroy(go);
    }

    IEnumerator ParticleRoutine(Vector3 origin)
    {
        GameObject go = new GameObject("Particle");
        go.transform.SetParent(rootCanvas.transform, false);

        RectTransform rt = go.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(28f, 28f);
        rt.position = origin;

        Image img = go.AddComponent<Image>();
        img.sprite = particleSprites[Random.Range(0, particleSprites.Length)];
        img.raycastTarget = false;

        Vector2 dir = Random.insideUnitCircle.normalized;
        float speed = Random.Range(120f, 280f);
        float duration = Random.Range(1.0f, 2.0f);
        float elapsed = 0f;
        Color col = new Color(Random.value, Random.value, 0.4f, 1f);
        img.color = col;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            rt.position += (Vector3)(dir * speed * Time.deltaTime);
            rt.localScale = Vector3.one * Mathf.Lerp(1f, 0f, t);
            img.color = new Color(col.r, col.g, col.b, 1f - t);
            yield return null;
        }

        Destroy(go);
    }
}