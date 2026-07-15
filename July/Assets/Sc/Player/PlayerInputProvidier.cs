using UnityEngine;

/// <summary>
/// このクラスは、プレイヤーの入力を管理するためのシングルトンプロバイダーです。PlayerInputActionsを保持し、ゲーム全体でアクセス可能にします。
/// </summary>
public class PlayerInputProvider : MonoBehaviour
{
    public static PlayerInputProvider Instance { get; private set; }
    public PlayerInputActions Actions { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        Actions?.Enable();
    }

    private void OnDisable()
    {
        Actions?.Disable();
    }
}
