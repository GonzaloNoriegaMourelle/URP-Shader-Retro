using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChangeCameraCollision : MonoBehaviour
{
    public int cameraChange;




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ExampleChangeCamera.instance.SwitchCamera(cameraChange);
        }
    }
}
