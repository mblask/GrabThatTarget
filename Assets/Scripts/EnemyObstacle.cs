using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyObstacle : MonoBehaviour
{
    protected abstract void move();
    public abstract void Activate(bool value);
    public abstract bool IsActive();
    public abstract void IncreaseSpeedByPercentage(float percentage);
    public abstract void ResetSpeed();
}
