using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// カスタマーをランダムにスポーン　データの注入をするクラス
/// </summary>
public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<CustomerData> allCustomerrs;

    public void SpawnRandomCustomer()
    {
        if (allCustomerrs == null || allCustomerrs.Count == 0)
        {
            Debug.LogError("CustomerSpawner: allCustomerrs is not assigned.");
            return;
        }
        CustomerData selectedCustomer = RandomPicker.PickRandom(allCustomerrs);
        CustomerRuntimeData runtimeData = new CustomerRuntimeData(selectedCustomer);

        InGameEventManager.Instance.EmitCustomerAppear(runtimeData);
    }
}

