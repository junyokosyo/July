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
}