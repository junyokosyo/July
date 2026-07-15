using UnityEngine;

/// <summary>
/// このクラスは、カウンターの状態を管理します。カウンターに置かれた武器のデータを取得する機能を提供します。
/// </summary>
public class Counter : MonoBehaviour
{
    private WeaponPickup _placedWeapon;

    /// <summary>
    /// カウンターに置かれている武器のデータを取得します。
    /// </summary>
    /// <returns></returns>
    public WeaponData GetPlacedItem()
    {
        return _placedWeapon != null ? _placedWeapon.WeaponData : null;
    }

    public WeaponPickup GetPlacedWeapon()
    {
        return _placedWeapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponPickup weapon = other.GetComponent<WeaponPickup>();
        if (weapon != null)
        {
            _placedWeapon = weapon;
            Debug.Log($"カウンターに乗った: {weapon.WeaponData.WeaponName}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WeaponPickup weapon = other.GetComponent<WeaponPickup>();
        if (weapon != null && weapon == _placedWeapon)
        {
            _placedWeapon = null;
            Debug.Log("カウンターから離れた");
        }
    }
}
