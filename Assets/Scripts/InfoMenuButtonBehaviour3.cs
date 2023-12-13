using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour3 : MonoBehaviour
{
    [SerializeField] private AudioSource firstAudioSource; // Assign the first AudioSource in the inspector
    [SerializeField] private AudioSource secondAudioSource; // Assign the second AudioSource in the inspector
    [SerializeField] private GameObject infoCardInScene;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite collectedIcon;
    [SerializeField] private Sprite openedIcon;
    private GameObject instantiatedInfoCard;
    private ButtonState currentState = ButtonState.Collected;
    private Interactable interactable;
    private bool isFirstPress = true; // Flag to check if it's the first press

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnClick.AddListener(ToggleInfoCard);
        if (firstAudioSource != null) firstAudioSource.loop = false;
        if (secondAudioSource != null) secondAudioSource.loop = false;
    }

    private void ToggleInfoCard()
    {
        switch (currentState)
        {
            case ButtonState.Opened:
                // If in Opened state, deactivate the info card, change to Collected state
                if (infoCardInScene != null)
                {
                    infoCardInScene.SetActive(false);
                }
                icon.sprite = collectedIcon;
                currentState = ButtonState.Collected;
                break;

            case ButtonState.Collected:
                // If in Collected state, activate the info card, change to Opened state
                if (infoCardInScene != null)
                {
                    infoCardInScene.SetActive(true);
                }
                icon.sprite = openedIcon;
                currentState = ButtonState.Opened;
                break;
        }

        // Play the audio based on isFirstPress
        if (isFirstPress)
        {
            PlayAudio(firstAudioSource);
        }
        else
        {
            PlayAudio(secondAudioSource);
        }

        isFirstPress = !isFirstPress;
    }

    private void PlayAudio(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.isActiveAndEnabled)
        {
            audioSource.Play();
        }
    }

    private enum ButtonState
    {
        Collected,
        Opened
    }
}
