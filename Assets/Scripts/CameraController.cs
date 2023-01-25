using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    private float _shakeDuration = 0.1f;
    private float _shakeMagnitude = 0.15f;

    public void ShakeCamera()
    {
        StartCoroutine(shakeCameraCoroutine());
    }

    private IEnumerator shakeCameraCoroutine()
    {
        float stopwatch = 0.0f;
        Vector3 originalPosition = transform.position;

        while (stopwatch <= _shakeDuration)
        {
            Vector3 position = new Vector3(Random.Range(-_shakeMagnitude, _shakeMagnitude), Random.Range(-_shakeMagnitude, _shakeMagnitude), transform.position.z);
            transform.position = position;

            stopwatch += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
