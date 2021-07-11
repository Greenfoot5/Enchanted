using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Joystick joystick;
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3();
        
        // Movement in the X axis
        if (joystick.Horizontal > 0)
        {
            movement.x = moveSpeed;
        }
        else if (joystick.Horizontal < 0)
        {
            movement.x = moveSpeed * -1;
        }
        
        // Movement in the Y axis
        if (joystick.Vertical > 0)
        {
            movement.z = moveSpeed;
        }
        else if (joystick.Vertical < 0)
        {
            movement.z = moveSpeed * -1;
        }

        gameObject.transform.Translate(movement * Time.deltaTime);
    }
}
