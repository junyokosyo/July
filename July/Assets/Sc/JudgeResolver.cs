using UnityEngine;

public class JudgeResolver : MonoBehaviour
{
    [SerializeField] private Counter counter;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private FailPostEffect failPostEffect;

    private void Start()
    {
        InGameEventManager.Instance.OnJudgmentResult += HandleJudgmentResult;
    }

    private void OnDestroy()
    {
        if (InGameEventManager.Instance != null)
            InGameEventManager.Instance.OnJudgmentResult -= HandleJudgmentResult;
    }

    private void HandleJudgmentResult(bool isCorrect)
    {
        if (isCorrect)
        {
            WeaponPickup weapon = counter.GetPlacedWeapon();
            if (weapon != null) Destroy(weapon.gameObject);
        }
        else
        {
            cameraShake.Shake(0.3f, 0.1f);
            failPostEffect.Flash();
        }
    }
}
