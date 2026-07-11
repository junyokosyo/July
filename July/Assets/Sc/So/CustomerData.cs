using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class CustomerData : ScriptableObject
{
    public string CustomerName;
    public WeaponData[] Weapons;
}
