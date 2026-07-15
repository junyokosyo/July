using UnityEngine;
using System.Collections;

public class PCCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform pcViewPoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float moveDuration = 0.5f;

    private Transform _originalParent;
    private Vector3 _originalLocalPos;
    private Quaternion _originalLocalRot;
    private Coroutine _routine;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (IsFocused) UnfocusPC();
            else FocusPC();
        }
    }
    public bool IsFocused { get; private set; }

    /// <summary>PCモニターへカメラを寄せる。</summary>
    public void FocusPC()
    {
        if (IsFocused || _routine != null) return;

        _originalParent = playerCamera.parent;
        _originalLocalPos = playerCamera.localPosition;
        _originalLocalRot = playerCamera.localRotation;

        playerController.enabled = false;
        playerCamera.SetParent(null);
        InputModeController.Instance.SetMode(InputMode.PC);
        _routine = StartCoroutine(FocusRoutine());
    }

    /// <summary>元のプレイヤー視点へ戻す。</summary>
    public void UnfocusPC()
    {
        if (!IsFocused || _routine != null) return;

        _routine = StartCoroutine(UnfocusRoutine());
    }

    private IEnumerator FocusRoutine()
    {
        yield return MoveCamera(pcViewPoint.position, pcViewPoint.rotation);
        IsFocused = true;
        _routine = null;
    }

    private IEnumerator UnfocusRoutine()
    {
        Vector3 targetPos = _originalParent.TransformPoint(_originalLocalPos);
        Quaternion targetRot = _originalParent.rotation * _originalLocalRot;

        yield return MoveCamera(targetPos, targetRot);

        playerCamera.SetParent(_originalParent);
        playerCamera.localPosition = _originalLocalPos;
        playerCamera.localRotation = _originalLocalRot;

        playerController.enabled = true;
        InputModeController.Instance.SetMode(InputMode.Gameplay);
        IsFocused = false;
        _routine = null;
    }

    private IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
    {
        Vector3 startPos = playerCamera.position;
        Quaternion startRot = playerCamera.rotation;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / moveDuration);
            playerCamera.position = Vector3.Lerp(startPos, targetPos, t);
            playerCamera.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        playerCamera.position = targetPos;
        playerCamera.rotation = targetRot;
    }
}
