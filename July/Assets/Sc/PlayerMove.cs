using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _sprintSpeed = 6f;

    [Header("Look")]
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _mouseSensitivity = 0.05f;

    private CharacterController _characterController;
    private PlayerInputActions _inputActions;

    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private bool _isSprinting;
    private float _pitch;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _inputActions = new PlayerInputActions();

        _inputActions.Player.Move.performed +=
            context => _moveInput = context.ReadValue<Vector2>();

        _inputActions.Player.Move.canceled +=
            _ => _moveInput = Vector2.zero;

        _inputActions.Player.Look.performed +=
            context => _lookInput = context.ReadValue<Vector2>();

        _inputActions.Player.Look.canceled +=
            _ => _lookInput = Vector2.zero;

        _inputActions.Player.Sprint.performed +=
            _ => _isSprinting = true;

        _inputActions.Player.Sprint.canceled +=
            _ => _isSprinting = false;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        float speed = _isSprinting
            ? _sprintSpeed
            : _walkSpeed;

        Vector3 movement =
            transform.forward * _moveInput.y +
            transform.right * _moveInput.x;

        _characterController.Move(
            movement * speed * Time.deltaTime);
    }

    private void Look()
    {
        float mouseX =
            _lookInput.x * _mouseSensitivity;

        float mouseY =
            _lookInput.y * _mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, -80f, 80f);

        _cameraRoot.localRotation =
            Quaternion.Euler(_pitch, 0f, 0f);
    }
}