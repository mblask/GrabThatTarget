using UnityEngine;
using AlpacaMyGames;

public class PlayerInputMouse : MonoBehaviour, IPlayerInput
{
    public Vector3 GetMovement()
    {
        if (!Input.GetMouseButton(0))
            return default(Vector3);

        Vector2 movement = (Utilities.GetMouseWorldLocation2D() - (Vector2)transform.position).normalized;
        return new Vector3(movement.x, movement.y, 0.0f);
    }

    public bool IsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return true;

        return false;
    }
}
