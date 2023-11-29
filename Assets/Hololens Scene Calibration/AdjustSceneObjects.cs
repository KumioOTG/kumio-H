using UnityEngine;
using System.Collections;

public class AdjustSceneObjects : MonoBehaviour
{
    public Transform[] objectsToAdjust; // Assign these in the Unity Editor

    void Start()
    {
        StartCoroutine(DelayedAdjustment());
    }

    IEnumerator DelayedAdjustment()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed

        // Now adjust the objects
        AdjustObjectsToCamera();
    }

    private void AdjustObjectsToCamera()
    {
        Vector3 cameraPositionDelta = Camera.main.transform.position;
        Quaternion cameraRotationDelta = Camera.main.transform.rotation;

        foreach (Transform obj in objectsToAdjust)
        {
            obj.position += cameraPositionDelta;
            obj.rotation = Quaternion.Euler(obj.eulerAngles.x, cameraRotationDelta.eulerAngles.y, obj.eulerAngles.z);
        }
    }
}
