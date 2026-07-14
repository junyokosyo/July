using UnityEngine;

/// <summary>
/// このクラスは、ゲーム内の入力モードを管理します。プレイヤーが会話中かどうかに応じて、適切な入力アクションマップを有効化または無効化します。
/// </summary>
public class InputModeController : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        InGameEventManager.Instance.OnDialogueStartRequested += EnterDialogueMode;
        InGameEventManager.Instance.OnDialogueFinished += ExitDialogueMode;
    }

    private void OnDisable()
    {
        if (InGameEventManager.Instance != null)
        {
            InGameEventManager.Instance.OnDialogueStartRequested -= EnterDialogueMode;
            InGameEventManager.Instance.OnDialogueFinished -= ExitDialogueMode;
        }
    }

    private void Start()
    {
        _inputActions.Player.Enable();
        _inputActions.Dialogue.Disable();
    }

    private void EnterDialogueMode(CustomerRuntimeData data)
    {
        _inputActions.Player.Disable();
        _inputActions.Dialogue.Enable();
    }

    private void ExitDialogueMode()
    {
        _inputActions.Dialogue.Disable();
        _inputActions.Player.Enable();
    }

}
