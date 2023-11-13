using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class ResetTrackedPose : MonoBehaviour
{
    private TrackedPoseDriver trackedPoseDriver;

    void Start()
    {
        // Try to get the Tracked Pose Driver component from the same GameObject this script is attached to
        trackedPoseDriver = GetComponent<TrackedPoseDriver>();

        if (trackedPoseDriver == null)
        {
            Debug.LogError("TrackedPoseDriver component not found on the GameObject.");
            return;
        }

        // Optional: Automatically reset the pose at the start of the scene
        // ResetDriverPose();
    }

    public void ResetDriverPose()
    {
        // Reset the local position and rotation of the GameObject to which the Tracked Pose Driver is attached
        trackedPoseDriver.transform.localPosition = Vector3.zero;
        trackedPoseDriver.transform.localRotation = Quaternion.identity;

        // Add any additional reset logic here if needed
    }
}
