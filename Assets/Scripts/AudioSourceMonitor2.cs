using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceMonitor2 : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate; // The objects that should be activated when audio stops.
    private AudioSource audioSource; // The AudioSource attached to this GameObject
    private bool hasActivated = false; // Flag to track if activation has occurred

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if objectsToActivate is not null, audio is not playing, and the specified objects are not active
        if (!hasActivated && objectsToActivate != null && objectsToActivate.Length > 0 && !audioSource.isPlaying)
        {
            // Activate the objects
            foreach (var obj in objectsToActivate)
            {
                if (obj != null && !obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                }
            }

            hasActivated = true; // Set the flag to indicate activation has occurred
        }
    }
}
