using UnityEngine;

public class JudgeDebugTrigger : MonoBehaviour
{
    [SerializeField] private JudgeSystem judgeSystem;
    [SerializeField] private WeaponData testWeaponA;
    [SerializeField] private WeaponData testWeaponB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) judgeSystem.Judge(testWeaponA);
        if (Input.GetKeyDown(KeyCode.Alpha2)) judgeSystem.Judge(testWeaponB);
    }
}
