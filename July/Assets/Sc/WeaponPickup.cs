using UnityEngine;

/// <summary>
/// このクラスは、武器のピックアップアイテムを表します。武器データを保持し、Rigidbodyコンポーネントを管理します。
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    public WeaponData WeaponData => weaponData;
    public Rigidbody Rb { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
}
