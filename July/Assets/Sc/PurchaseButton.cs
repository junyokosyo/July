using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private WeaponData weaponToPurchase; // このボタンで買える武器
    [SerializeField] private WeaponSpawner weaponSpawner;

    /// <summary>ボタンのOnClickから呼ぶ。</summary>
    public void OnPurchaseClicked()
    {
        weaponSpawner.SpawnWeapon(weaponToPurchase);
    }
}
