using UnityEngine;

/// <summary>
/// カウンターに置かれた武器と、客が欲しい武器を照合して判定するクラス。
/// </summary>
public class JudgeSystem : MonoBehaviour
{
    private CustomerRuntimeData currentCustomer;

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerReadyAtCounter += HandleCustomerReady;
    }

    private void OnDestroy()
    {
        if (InGameEventManager.Instance != null)
            InGameEventManager.Instance.OnCustomerReadyAtCounter -= HandleCustomerReady;
    }

    private void HandleCustomerReady(CustomerRuntimeData data)
    {
        currentCustomer = data; // 今の客を覚えておく
    }

    /// <summary>置かれた武器を判定する。正解ならtrueを返す。</summary>
    public void Judge(WeaponData placedItem)
    {
        if (currentCustomer == null)
        {
            Debug.LogWarning("判定対象の客がいません");
            return;
        }

        bool isCorrect = (placedItem == currentCustomer.ResolvedItem);
        InGameEventManager.Instance.EmitJudgmentResult(isCorrect);

        Debug.Log(isCorrect
            ? $"正解! 客が欲しかったのは {currentCustomer.ResolvedItem.WeaponName}"
            : $"不正解! 欲しかったのは {currentCustomer.ResolvedItem.WeaponName} / 置いたのは {placedItem.WeaponName}");

    }
}

