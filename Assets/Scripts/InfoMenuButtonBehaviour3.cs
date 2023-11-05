using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        firstAudioSource.loop = false;
        secondAudioSource.loop = false;
    }

    private void ToggleInfoCard()
    {
        // Instantiate or destroy the info card depending on the current state
        if (currentState == ButtonState.Opened)
        {
            CloseInfoCard();
        }
        else if (currentState == ButtonState.Collected)
        {
            OpenInfoCard();
        }

        // Play the first audio source on first press, second audio source on second press
        if (isFirstPress)
        {
            firstAudioSource.Play();
        }
        else
        {
            secondAudioSource.Play();
        }

        // Toggle the isFirstPress flag
        isFirstPress = !isFirstPress;
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
            Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            instantiatedInfoCard = Instantiate(infoCardPrefab, spawnPosition, rotation);
        }
    }

    private enum ButtonState
    {
        Collected,
        Opened
    }
}
