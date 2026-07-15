using UnityEngine;



public enum InputMode
{
    Gameplay,
    Dialogue,
    PC,
}

public class InputModeController : MonoBehaviour
{
    public static InputModeController Instance { get; private set; }

    private PlayerInputActions _actions;

    public InputMode CurrentMode { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _actions = PlayerInputProvider.Instance.Actions;
    }

    private void Start()
    {
        SetMode(InputMode.Gameplay);
    }

    public void SetMode(InputMode mode)
    {
        CurrentMode = mode;

        switch (mode)
        {
            case InputMode.Gameplay:
                _actions.Player.Enable();
                _actions.Dialogue.Disable();
                _actions.PC.Enable();
                SetCursor(locked: true);
                break;

            case InputMode.Dialogue:
                _actions.Player.Disable();
                _actions.Dialogue.Enable();
                _actions.PC.Disable();
                SetCursor(locked: true);
                break;

            case InputMode.PC:
                _actions.Player.Disable();
                _actions.Dialogue.Disable();
                _actions.PC.Disable();
                SetCursor(locked: false);
                break;
        }

        Debug.Log($"入力モード変更: {mode}");
    }

    private void SetCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

}
