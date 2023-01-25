using UnityEngine;

public class PlayerInputKeyboard : MonoBehaviour, IPlayerInput
{
    public Vector3 GetMovement()
    {
        Vector3 movement;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.z = 0.0f;

        return movement.normalized;
    }

    public bool IsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }

        return false;
    }
}
