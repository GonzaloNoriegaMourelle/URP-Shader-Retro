using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCameraMove : MonoBehaviour
{


    public Vector3 startPosition;
    public Vector3 endPosition;
    public float moveTime = 1.0f; // Tiempo en segundos para llegar de un punto a otro
    public KeyCode startMovementKey = KeyCode.Space; // Tecla para iniciar el movimiento

    private float elapsedTime = 0.0f;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKeyDown(startMovementKey))
        {
            isMoving = true;
            elapsedTime = 0.0f;
        }

        if (isMoving && elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }
    }

}
