using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterDelay : MonoBehaviour
{
    public float delayInSeconds = 5.0f; // Set the default delay time in the Inspector

    private void Start()
    {
        // Call the DeactivateObject method after the specified delay
        Invoke("DeactivateObject", delayInSeconds);
    }

    private void DeactivateObject()
    {
        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
