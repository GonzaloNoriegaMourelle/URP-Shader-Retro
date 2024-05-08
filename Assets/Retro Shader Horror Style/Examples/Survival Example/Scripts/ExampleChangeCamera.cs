using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChangeCamera : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    private int currentCameraIndex = 0;


    public static ExampleChangeCamera instance;
    void Start()
    {
        // Activate the first camera in the list and deactivate the rest
        SwitchCamera(0);
    }

    private void Awake()
    {
        instance = this;
    }

    public void SwitchCamera(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= cameras.Count)
        {
            Debug.LogWarning("Invalid camera index");
            return;
        }

        // Deactivate all cameras except the one at cameraIndex
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == cameraIndex);
        }

        currentCameraIndex = cameraIndex;
    }
}
