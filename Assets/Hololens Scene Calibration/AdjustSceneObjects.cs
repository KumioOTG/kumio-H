using UnityEngine;

public class AdjustSceneObjects : MonoBehaviour
{
    public Transform[] objectsToAdjust; // Assign these in the Unity Editor

    void Start()
    {
        AdjustObjectsToCamera();
    }

    private void AdjustObjectsToCamera()
    {
        Vector3 cameraPositionDelta = Camera.main.transform.position;
        float cameraYRotation = Camera.main.transform.eulerAngles.y;

        foreach (Transform obj in objectsToAdjust)
        {
            obj.position += cameraPositionDelta;
            Vector3 currentRotation = obj.eulerAngles;
            obj.rotation = Quaternion.Euler(currentRotation.x, cameraYRotation, currentRotation.z);
        }
    }
}
