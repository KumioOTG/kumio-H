using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour3 : MonoBehaviour
{
    [SerializeField] private AudioSource firstAudioSource; // Assign the first AudioSource in the inspector
    [SerializeField] private AudioSource secondAudioSource; // Assign the second AudioSource in the inspector
    [SerializeField] private GameObject infoCardPrefab;
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
        if (currentState == ButtonState.Opened)
        {
            CloseInfoCard();
        }
        else if (currentState == ButtonState.Collected)
        {
            OpenInfoCard();
        }

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

    private void OpenInfoCard()
    {
        InstantiateInfoCard();
        icon.sprite = openedIcon;
        currentState = ButtonState.Opened;
    }

    private void CloseInfoCard()
    {
        if (instantiatedInfoCard != null)
        {
            Destroy(instantiatedInfoCard);
            instantiatedInfoCard = null;
        }
        icon.sprite = collectedIcon;
        currentState = ButtonState.Collected;
    }

    private void InstantiateInfoCard()
    {
        if (instantiatedInfoCard == null)
        {
            float distanceFromUser = 2.0f;
            Vector3 userPosition = Camera.main.transform.position;
            Vector3 userForward = Camera.main.transform.forward;
            Vector3 spawnPosition = userPosition + userForward * distanceFromUser;

            // Adjust the rotation to face the camera
            Quaternion rotation = Quaternion.LookRotation(-userForward, Vector3.up); // This line is changed

            instantiatedInfoCard = Instantiate(infoCardPrefab, spawnPosition, rotation);
        }
    }

    private enum ButtonState
    {
        Collected,
        Opened
    }
}
