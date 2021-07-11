using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 90f;
    
    [Header("Joystick Settings")]
    public Joystick joystick;
    public float deadZone = 0.2f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3();

        // Movement in the X axis
        if (joystick.Horizontal > deadZone)
        {
            movement.x = moveSpeed;
        }
        else if (joystick.Horizontal < -deadZone)
        {
            movement.x = moveSpeed * -1;
        }
        
        // Movement in the Y axis
        if (joystick.Vertical > deadZone)
        {
            movement.z = moveSpeed;
        }
        else if (joystick.Vertical < -deadZone)
        {
            movement.z = moveSpeed * -1;
        }
        
        
        gameObject.transform.Translate(movement * Time.deltaTime, Space.World);
        
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 
                rotationSpeed * Time.deltaTime); 
        }
    }
}
