
using UnityEngine;

public class RotateSceneContent : MonoBehaviour
{
    public GameObject sceneContent; // Assign this in Unity Editor. This should be the parent object of all your scene content.

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public void RotateContentToCamera()
    {
        if (cameraTransform == null || sceneContent == null)
        {
            Debug.LogError("Missing components: Main camera or Scene Content.");
            return;
        }

        // Get the Y-axis rotation of the camera
        float cameraYRotation = cameraTransform.eulerAngles.y;

        // Rotate the scene content to align with the camera's Y-axis rotation
        // This rotates around the world Y-axis; adjust as needed for your specific use case
        sceneContent.transform.eulerAngles = new Vector3(0, cameraYRotation, 0);
    }
}