using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Scriptable Objects/CustomerData")]
public class CustomerData : ScriptableObject
{
    public string CustomerName;
    public List<WeaponData> Weapons;
    public List<string> DialogueLines;
    public bool IsSpecial;
}
