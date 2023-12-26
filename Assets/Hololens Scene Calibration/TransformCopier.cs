using UnityEngine;

public class TransformCopier : MonoBehaviour
{
    public Transform sourceTransform; // The camera's transform
    public Transform targetTransform; // The transform of the GameObject you want to copy to

    // Call this method when the button is clicked
    public void CopyTransform()
    {
        if (sourceTransform != null && targetTransform != null)
        {
            // Copy position
            targetTransform.position = sourceTransform.position;

            // Copy rotation with exclusion of X-axis rotation
            Quaternion sourceRotation = sourceTransform.rotation;
            Quaternion targetRotation = targetTransform.rotation;
            targetTransform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, sourceRotation.eulerAngles.y, sourceRotation.eulerAngles.z);
        }
    }
}
