using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 会話の進行を管理するクラス。カスタマーが出現した際に、DialogueLinesを順番に表示する。
/// </summary>
public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private TypewriterText typewriter;

    private PlayerInputActions _inputActions;
    private string[] lines;
    private int currentIndex;
    private CustomerRuntimeData currentCustomer;

    private void Awake()
    {
        _inputActions = PlayerInputProvider.Instance.Actions;
    }

    private void OnEnable()
    {
        _inputActions.Dialogue.Advance.performed += OnAdvance;
    }

    private void OnDisable()
    {
        _inputActions.Dialogue.Advance.performed -= OnAdvance;
    }

    private void Start()
    {
        InGameEventManager.Instance.OnCustomerReadyAtCounter += HandleCustomerAppear;
        InGameEventManager.Instance.OnCustomerExit += HandleCustomerExit;
    }

    private void OnDestroy()
    {
        if (InGameEventManager.Instance != null)
            InGameEventManager.Instance.OnCustomerReadyAtCounter -= HandleCustomerAppear;
        InGameEventManager.Instance.OnCustomerExit -= HandleCustomerExit;
    }

    private void OnAdvance(InputAction.CallbackContext context)
    {
        Advance();
    }

    private void HandleCustomerAppear(CustomerRuntimeData data)
    {
        currentCustomer = data;
        lines = data.Template.DialogueLines.ToArray();
        currentIndex = 0;
        ShowCurrentLine();
    }

    public void Advance()
    {
        if (lines == null) return;

        if (typewriter.IsTyping)
        {
            typewriter.Skip();
            return;
        }
        currentIndex++;
        if (currentIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        string line = lines[currentIndex];
        line = ReplacePlaceholders(line);
        typewriter.ShowLine(line);
        InGameEventManager.Instance.EmitDialogueLineShown(line);
    }

    private string ReplacePlaceholders(string line)
    {
        if (currentCustomer != null && currentCustomer.ResolvedItem != null)
        {
            line = line.Replace("{weapon}", currentCustomer.ResolvedItem.WeaponName);
        }
        return line;
    }

    private void EndDialogue()
    {
        InGameEventManager.Instance.EmitDialogueFinished();
        lines = null;
        Debug.Log("会話終了");
    }
    private void HandleCustomerExit()
    {
        typewriter.Clear();
        currentCustomer = null;
    }
}
