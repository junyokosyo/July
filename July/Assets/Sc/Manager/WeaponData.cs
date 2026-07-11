using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string WeaponID;
    public string WeaponName;
    public GameObject WeaponPrefab;
}
