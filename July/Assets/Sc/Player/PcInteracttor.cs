using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PcInteracttor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask pcLayer;
    [SerializeField] private PCCameraController pcCamera;
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TMP_Text promptText;

    private PlayerInputActions _inputActions;
    private bool _canInteract;

    private void Awake()
    {
        _inputActions = PlayerInputProvider.Instance.Actions;
    }

    private void OnEnable()
    {
        _inputActions.Player.Interact.performed += OnInteract;
        _inputActions.PC.Exit.performed += OnExit;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= OnInteract;
        _inputActions.PC.Exit.performed -= OnExit;
    }

    private void Update()
    {
        // PCモード中は案内を出さない
        if (pcCamera.IsFocused)
        {
            SetPrompt(false);
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, interactRange, pcLayer);
        SetPrompt(hit);
    }

    private void SetPrompt(bool show)
    {
        if (_canInteract == show) return;
        _canInteract = show;

        promptUI.SetActive(show);
        if (show) promptText.text = "[E] PCを使う";
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!_canInteract) return;
        pcCamera.FocusPC();
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        pcCamera.UnfocusPC();
    }
}
