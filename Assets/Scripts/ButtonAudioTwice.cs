using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ButtonAudioTwice : MonoBehaviour
{
    [SerializeField] private AudioSource firstAudioSource; // Assign the first AudioSource in the inspector
    [SerializeField] private AudioSource secondAudioSource; // Assign the second AudioSource in the inspector
    
    private Interactable interactable;
    private bool isFirstPress = true; // Flag to check if it's the first press

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    interactable.OnClick.AddListener(ToggleSound); // Add this line to set the listener
    firstAudioSource.loop = false;
    secondAudioSource.loop = false;
    }

   private void ToggleSound()
{
    if (isFirstPress)
    {
        // Stop the second audio source in case it is playing
        secondAudioSource.Stop(); 
        firstAudioSource.Play();
    }
    else
    {
        // Stop the first audio source before playing the second
        firstAudioSource.Stop(); 
        secondAudioSource.Play();
    }

    // Toggle the isFirstPress flag
    isFirstPress = !isFirstPress;
}



    private enum ButtonState
    {
        Collected,
        Opened
    }
    
    
   
}
