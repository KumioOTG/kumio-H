using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceMonitor : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // The object that should be activated when audio stops.
    private AudioSource audioSource; // The AudioSource attached to this GameObject
    private bool hasActivated = false; // Flag to track if activation has occurred

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if objectToActivate is not null, audio is not playing, and the specified object is not active
        if (!hasActivated && objectToActivate != null && !audioSource.isPlaying && !objectToActivate.activeInHierarchy)
        {
            // Activate the object
            objectToActivate.SetActive(true);
            hasActivated = true; // Set the flag to indicate activation has occurred
        }
    }
}
