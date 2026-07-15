using UnityEngine;
using UnityEngine.InputSystem;

public class JudgeButton : MonoBehaviour
{

    [SerializeField] private Counter counter;
    [SerializeField] private JudgeSystem judgeSystem;
    [SerializeField] private InputActionReference judgeAction;

    private void OnEnable()
    {
        judgeAction.action.performed += OnJudge;
        judgeAction.action.Enable();
    }

    private void OnDisable()
    {
        judgeAction.action.performed -= OnJudge;
        judgeAction.action.Disable();
    }

    private void OnJudge(InputAction.CallbackContext context)
    {
        WeaponData placedItem = counter.GetPlacedItem();

        if (placedItem == null)
        {
            Debug.Log("カウンターに武器が置かれていません");
            return;
        }

        judgeSystem.Judge(placedItem);
    }
}
