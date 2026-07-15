using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform dropPoint; // 武器が降ってくる位置(上空)

    /// <summary>指定の武器を生成して落下させる。</summary>
    public void SpawnWeapon(WeaponData weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogError("WeaponSpawner: weaponDataがnullです");
            return;
        }

        if (weaponData.WeaponPrefab == null)
        {
            Debug.LogError($"WeaponSpawner: {weaponData.WeaponName} のPrefabが未設定です");
            return;
        }

        Instantiate(weaponData.WeaponPrefab, dropPoint.position, dropPoint.rotation);
        Debug.Log($"武器を生成: {weaponData.WeaponName}");
    }
}
