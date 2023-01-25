using UnityEngine;

public interface IPlayerInput
{
    public Vector3 GetMovement();
    public bool IsSprinting();
}
