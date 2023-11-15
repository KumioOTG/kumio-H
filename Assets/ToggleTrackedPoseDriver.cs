using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class ToggleTrackedPoseDriver : MonoBehaviour
{
    // Reference to the main camera
    private TrackedPoseDriver trackedPoseDriver;

    void Start()
    {
        // Find the Main Camera and its TrackedPoseDriver component
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null)
        {
            trackedPoseDriver = mainCamera.GetComponent<TrackedPoseDriver>();
            if (trackedPoseDriver == null)
            {
                Debug.LogError("TrackedPoseDriver component not found on Main Camera.");
            }
        }
        else
        {
            Debug.LogError("Main Camera not found in the scene.");
        }
    }

    public void TogglePoseDriver()
    {
        // Start the coroutine to toggle the TrackedPoseDriver
        StartCoroutine(TogglePoseDriverCoroutine());
    }

    private IEnumerator TogglePoseDriverCoroutine()
    {
        if (trackedPoseDriver != null)
        {
            trackedPoseDriver.enabled = false; // Deactivate

            // Wait for a certain duration before reactivating
            yield return new WaitForSeconds(1); // Adjust the delay duration here

            trackedPoseDriver.enabled = true;  // Reactivate
        }
    }
}
