using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// カスタマーをランダムにスポーン　データの注入をするクラス
/// </summary>
public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<CustomerData> allCustomerrs;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform counterPoint;

    public void SpawnRandomCustomer()
    {
        if (allCustomerrs == null || allCustomerrs.Count == 0)
        {
            Debug.LogError("CustomerSpawner: allCustomerrs is not assigned or empty.");
            return;
        }

        CustomerData selectedCustomer = RandomPicker.PickRandom(allCustomerrs);
        CustomerRuntimeData runtimeData = new CustomerRuntimeData(selectedCustomer);

        // プレハブを入口に生成
        GameObject obj = Instantiate(
            selectedCustomer.CustomerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // 目的地(カウンター)へ向かわせる
        CustomerController controller = obj.GetComponent<CustomerController>();
        if (controller == null)
        {
            Debug.LogError("生成したプレハブに CustomerController がありません");
            return;
        }
        controller.Initialize(runtimeData, counterPoint.position);

        InGameEventManager.Instance.EmitCustomerAppear(runtimeData);
    }
}

