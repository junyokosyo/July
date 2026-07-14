using UnityEngine;

/// <summary>
/// このクラスは、顧客の移動と到着を管理します。顧客がカウンターに向かって移動し、到着した際にイベントを発火します。
/// </summary>
public class CustomerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float arriveThreshold = 0.05f; // 到着とみなす距離

    private CustomerRuntimeData runtimeData;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool hasArrived = false;
    private bool isLeaving = false;

    /// <summary>生成直後に呼ぶ。客データと目的地を受け取る。</summary>
    public void Initialize(CustomerRuntimeData data, Vector3 counterPosition)
    {
        runtimeData = data;
        targetPosition = counterPosition;
        isMoving = true;
        isLeaving = false;
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) <= arriveThreshold)
        {
            Arrive();
        }
    }

    private void Arrive()
    {
        isMoving = false;

        if (isLeaving)
        {
            InGameEventManager.Instance.EmitCustomerExit();
            Destroy(gameObject);
        }
        else
        {
            InGameEventManager.Instance.EmitCustomerReadyAtCounter(runtimeData);
        }
    }

    /// <summary>退場を開始する。出口へ向かって移動し、到着したら破棄される。</summary>
    public void Leave(Vector3 exitPosition)
    {
        targetPosition = exitPosition;
        isMoving = true;
        isLeaving = true;
    }

    public CustomerRuntimeData RuntimeData => runtimeData;
    public bool HasArrived => hasArrived;
}
