using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceMonitorDelay : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // The object that should be activated when audio stops.
    [SerializeField] private float delayInSeconds = 5.0f; // Delay in seconds that you can set in the Inspector
    private AudioSource audioSource; // The AudioSource attached to this GameObject
    private bool hasActivated = false; // Flag to track if activation has occurred
    private float timer = 0.0f; // Timer to track the delay

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if objectToActivate is not null, audio is not playing, and the specified object is not active
        if (!hasActivated && objectToActivate != null && !audioSource.isPlaying)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if the timer has reached the specified delay
            if (timer >= delayInSeconds)
            {
                // Activate the object
                objectToActivate.SetActive(true);
                hasActivated = true; // Set the flag to indicate activation has occurred
            }
        }
    }
}
