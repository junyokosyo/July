using UnityEngine;

public class DebugTrigger : MonoBehaviour
{
    [SerializeField] private GameStateManager stateManager; // spawnerから変更

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateManager.RequestNextCustomer(); // 状態管理経由で呼ぶ
        }
    }
}