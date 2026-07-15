using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private Transform shakeTarget;

    private Coroutine _routine;
    private Vector3 _originalLocalPos;

    private void Awake()
    {
        _originalLocalPos = shakeTarget.localPosition;
    }
    public void Shake(float duration = 0.8f, float magnitude = 0.9f)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float damper = 1f - (elapsed / duration);
            float offsetX = Random.Range(-1f, 1f) * magnitude * damper;
            float offsetY = Random.Range(-1f, 1f) * magnitude * damper;

            shakeTarget.localPosition = _originalLocalPos + new Vector3(offsetX, offsetY, 0f);
            yield return null;
        }

        shakeTarget.localPosition = _originalLocalPos;
    }
}
