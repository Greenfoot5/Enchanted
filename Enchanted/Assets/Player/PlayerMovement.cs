using UnityEngine;

/// <summary>
/// The player controller. Uses joystick data to move the player in a certain direction.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 90f;

    [Header("Joystick Settings")]
    public Joystick joystick;
    public float deadZone = 0.2f;

    private CharacterController _controller;
    private Transform _model;

    // Joystick localizations.
    private Vector3 _forward, _right;

    private void Awake()
    {
        // Player controller for movement physics
        _controller = gameObject.GetComponent<CharacterController>();
        // Player model for rotation
        _model = transform.GetChild(1);

        // Camera axis for joystick input
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward.Normalize();
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
    }

    public Vector3 Forward => _forward;
    public Vector3 Right => _right;

    void Update()
    {
        // Default direction
        var direction = Vector3.zero;

        // If direction above the deadZone, calculate it using camera axis
        if (joystick.Direction.magnitude >= deadZone)
            direction = _right * joystick.Horizontal + _forward * joystick.Vertical;

        // Normalize and move
        direction.Normalize();
        _controller.SimpleMove(moveSpeed * direction);

        // Don't rotate if no movement
        if (direction == Vector3.zero)
            return;

        // Lerped rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        _model.rotation = Quaternion.Lerp(_model.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
