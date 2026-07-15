using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private CustomerSpawner spawner;
    [SerializeField] private DialogueCameraFocus cameraFocus;

    public GameState CurrentState { get; private set; } = GameState.Idle;

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerReadyAtCounter += OnReadyAtCounter;
        InGameEventManager.Instance.OnDialogueFinished += OnDialogueFinished;
        InGameEventManager.Instance.OnJudgmentResult += _ => ChangeState(GameState.Result);
        InGameEventManager.Instance.OnCustomerExit += () => ChangeState(GameState.Idle);
    }

    private void OnReadyAtCounter(CustomerRuntimeData data)
    {
        ChangeState(GameState.Dialogue);
        InputModeController.Instance.SetMode(InputMode.Dialogue);
        cameraFocus.StartFocus();
    }

    private void OnDialogueFinished()
    {
        ChangeState(GameState.Serving);
        InputModeController.Instance.SetMode(InputMode.Gameplay);
        cameraFocus.StopFocus();
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
