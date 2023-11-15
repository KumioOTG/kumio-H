using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecalibrateScene : MonoBehaviour
{
    public Transform[] objectsToRecalibrate; // Assign these in the Unity Inspector

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void Recalibrate()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        // Capture the current camera position and rotation
        Vector3 cameraPosition = cameraTransform.position;
        Quaternion cameraRotation = cameraTransform.rotation;

        // Adjust each object's position and rotation relative to the camera's current state
        foreach (var obj in objectsToRecalibrate)
        {
            if (obj != null)
            {
                // Calculate new position and rotation relative to the camera
                obj.position -= cameraPosition;
                obj.rotation = Quaternion.Inverse(cameraRotation) * obj.rotation;
            }
        }

        // Optionally reset the camera's position and rotation
        cameraTransform.position = Vector3.zero;
        cameraTransform.rotation = Quaternion.identity;
    }
}
