using Unity.Mathematics;
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
        if (math.abs(joystick.Horizontal) > deadZone)
        {
            movement.x = joystick.Horizontal;
        }
        
        // Movement in the Y axis
        if (math.abs(joystick.Vertical) > deadZone)
        {
            movement.z = joystick.Vertical;
        }
        
        movement.Normalize();
        gameObject.transform.Translate(movement * (Time.deltaTime * moveSpeed), Space.World);
        
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 
                rotationSpeed * Time.deltaTime); 
        }
    }
}
