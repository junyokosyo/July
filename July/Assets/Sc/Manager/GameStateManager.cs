using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private CustomerSpawner spawner;

    public GameState CurrentState { get; private set; } = GameState.Idle;

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerReadyAtCounter += _ => ChangeState(GameState.Dialogue);
        InGameEventManager.Instance.OnDialogueFinished += () => ChangeState(GameState.Serving);
        InGameEventManager.Instance.OnJudgmentResult += _ => ChangeState(GameState.Result);
        InGameEventManager.Instance.OnCustomerExit += () => ChangeState(GameState.Idle);
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"状態変更: {newState}");

        switch (newState)
        {
            case GameState.Idle:
                break;
        }
    }

    /// <summary>次の客を呼ぶ。Idle状態の時だけ有効。</summary>
    public void RequestNextCustomer()
    {
        if (CurrentState != GameState.Idle)
        {
            Debug.Log("今は客を呼べません(接客中)");
            return;
        }

        ChangeState(GameState.CustomerComing);
        spawner.SpawnRandomCustomer();
    }
}
