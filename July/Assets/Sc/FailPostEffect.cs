using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class FailPostEffect : MonoBehaviour
{

    [SerializeField] private Volume failVolume; // 失敗用Volume
    [SerializeField] private float fadeInTime = 0.08f;
    [SerializeField] private float holdTime = 0.15f;
    [SerializeField] private float fadeOutTime = 0.6f;
    [SerializeField] private float maxWeight = 1f;

    private Coroutine _routine;

    private void Awake()
    {
        failVolume.weight = 0f; // 念のため初期化
    }

    /// <summary>失敗演出を再生する。</summary>
    public void Flash()
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        yield return Fade(0f, maxWeight, fadeInTime);
        yield return new WaitForSeconds(holdTime);
        yield return Fade(maxWeight, 0f, fadeOutTime);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            failVolume.weight = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        failVolume.weight = to;
    }
}
