using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabber : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float grabRange = 3f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private GameObject grabPromptUI;
    [SerializeField] private TMP_Text grabPromptText;

    private PlayerInputActions _inputActions;
    private WeaponPickup _heldWeapon;
    private WeaponPickup _hoveredWeapon;

    private void Awake()
    {
        _inputActions = PlayerInputProvider.Instance.Actions;
    }

    private void OnEnable()
    {
        _inputActions.Player.Grab.performed += OnGrab;
    }

    private void OnDisable()
    {
        _inputActions.Player.Grab.performed -= OnGrab;
    }

    private void Update()
    {
        if (_heldWeapon != null)
        {
            SetHovered(null);
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.SphereCast(ray, 0.2f, out RaycastHit hit, grabRange, grabbableLayer))
        {
            WeaponPickup weapon = hit.collider.GetComponent<WeaponPickup>();
            SetHovered(weapon);
        }
        else
        {
            SetHovered(null);
        }
    }

    private void SetHovered(WeaponPickup weapon)
    {
        bool shouldShow = (weapon != null);
        if (_hoveredWeapon == weapon && grabPromptUI.activeSelf == shouldShow) return;
        _hoveredWeapon = weapon;

        if (_hoveredWeapon != null)
        {
            grabPromptUI.SetActive(true);
            grabPromptText.text = $"[左クリック] {_hoveredWeapon.WeaponData.WeaponName} を持つ";
        }
        else
        {
            grabPromptUI.SetActive(false);
        }
    }

    private void OnGrab(InputAction.CallbackContext context)
    {
        if (_heldWeapon == null)
            TryGrab();
        else
            Release();
    }

    private void TryGrab()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.SphereCast(ray, 0.2f, out RaycastHit hit, grabRange, grabbableLayer))
        {
            WeaponPickup weapon = hit.collider.GetComponent<WeaponPickup>();
            if (weapon != null) Grab(weapon);
        }
    }

    private void Grab(WeaponPickup weapon)
    {
        _heldWeapon = weapon;
        _heldWeapon.Rb.isKinematic = true;
        _heldWeapon.transform.SetParent(holdPoint);
        _heldWeapon.transform.localPosition = Vector3.zero;
        _heldWeapon.transform.localRotation = Quaternion.identity;

        SetHovered(null);
    }

    private void Release()
    {
        _heldWeapon.transform.SetParent(null);
        _heldWeapon.Rb.isKinematic = false;
        _heldWeapon = null;
    }
}
