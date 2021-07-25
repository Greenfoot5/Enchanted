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

    private CharacterController controller;
    private Transform model;

    // Joystick localizations.
    private Vector3 forward, right;

    private void Awake()
    {
        // Player controller for movement physics
        controller = gameObject.GetComponent<CharacterController>();
        // Player model for rotation
        model = transform.GetChild(1);

        // Camera axis for joystick input
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    public Vector3 Forward => forward;
    public Vector3 Right => right;

    void Update()
    {
        // Default direction
        Vector3 direction = Vector3.zero;

        // If direction above the deadZone, calculate it using camera axis
        if (joystick.Direction.magnitude >= deadZone)
            direction = right * joystick.Horizontal + forward * joystick.Vertical;

        // Normalize and move
        direction.Normalize();
        controller.SimpleMove(moveSpeed * direction);

        // Don't rotate if no movement
        if (direction == Vector3.zero)
            return;

        // Lerped rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        model.rotation = Quaternion.Lerp(model.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}