using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaMyGames;

public class TargetPoint : MonoBehaviour
{
    public void SetRandomPosition()
    {
        transform.position = Utilities.GetRandomWorldPositionFromScreen();
    }
}
