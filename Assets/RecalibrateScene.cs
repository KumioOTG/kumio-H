using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRecalibrator : MonoBehaviour
{
    public GameObject sceneContainer; // Assign this in the Unity Inspector

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void Recalibrate()
    {
        if (cameraTransform == null || sceneContainer == null)
        {
            Debug.LogError("Missing components: Main camera or Scene Container.");
            return;
        }

        // Calculate the difference in position and rotation
        Vector3 positionOffset = cameraTransform.position - sceneContainer.transform.position;
        Quaternion rotationOffset = Quaternion.Inverse(sceneContainer.transform.rotation) * cameraTransform.rotation;

        // Update the position and rotation of each child relative to the new offsets
        foreach (Transform child in sceneContainer.transform)
        {
            child.position -= positionOffset;
            child.rotation = rotationOffset * child.rotation;
        }

        // Reset the camera's position and rotation to the scene container
        sceneContainer.transform.position = cameraTransform.position;
        sceneContainer.transform.rotation = cameraTransform.rotation;
    }
}
