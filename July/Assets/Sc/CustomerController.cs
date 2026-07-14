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

    /// <summary>生成直後に呼ぶ。客データと目的地を受け取る。</summary>
    public void Initialize(CustomerRuntimeData data, Vector3 counterPosition)
    {
        runtimeData = data;
        targetPosition = counterPosition;
        isMoving = true;
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
        hasArrived = true;
        InGameEventManager.Instance.EmitCustomerReadyAtCounter(runtimeData);
    }

    public CustomerRuntimeData RuntimeData => runtimeData;
    public bool HasArrived => hasArrived;
}
