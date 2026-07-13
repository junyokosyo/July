using UnityEngine;

public class DebugTrigger : MonoBehaviour
{
    [SerializeField] private CustomerSpawner spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawner.SpawnRandomCustomer();
        }
    }

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerAppear += HandleCustomerAppear;
    }

    private void HandleCustomerAppear(CustomerRuntimeData data)
    {
        Debug.Log($"客が来た: {data.Template.CustomerName}, 欲しい銃: {data.ResolvedItem.WeaponName}");
    }
}
