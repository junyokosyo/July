using UnityEngine;

/// <summary>
/// このクラスは、顧客とのインタラクションを管理します。顧客が出現した際に会話を開始できる状態を管理し、プレイヤーがインタラクトしたときに会話開始イベントを発火します。
/// </summary>

public class CustomerInteraction : MonoBehaviour
{
    private bool canStartDialogue = false;
    private CustomerRuntimeData waitingCustomer;

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerAppear += HandleCustomerAppear;
    }

    private void OnDestroy()
    {
        if (InGameEventManager.Instance != null)
            InGameEventManager.Instance.OnCustomerAppear -= HandleCustomerAppear;
    }

    private void HandleCustomerAppear(CustomerRuntimeData data)
    {
        waitingCustomer = data;
        canStartDialogue = true;
    }
    public void OnInteract()
    {
        if (canStartDialogue)
        {
            canStartDialogue = false;
            InGameEventManager.Instance.EmitDialogueStartRequested(waitingCustomer);
        }
    }
}
