using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour3 : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Assign this in the inspector
    [SerializeField] private GameObject infoCardPrefab;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite normalIcon;
    [SerializeField] private Sprite collectedIcon;
    [SerializeField] private Sprite openedIcon;
    private GameObject instantiatedInfoCard;
    private ButtonState currentState = ButtonState.Collected;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnClick.AddListener(ToggleInfoCard);
        audioSource.loop = false;
    }

    private void ToggleInfoCard()
    {
        switch (currentState)
        {
            case ButtonState.Opened:
                // If in Opened state, destroy the info card, change to Collected state
                if (instantiatedInfoCard != null)
                {
                    Destroy(instantiatedInfoCard);
                    instantiatedInfoCard = null;
                }
                icon.sprite = collectedIcon;
                currentState = ButtonState.Collected;
                break;

            case ButtonState.Collected:
                // If in Collected state, instantiate the info card, change to Opened state
                InstantiateInfoCard(); // Instantiate the info card
                if (!audioSource.isPlaying)
                {
                    audioSource.Play(); // Play audio
                }
                icon.sprite = openedIcon; // Change to opened icon
                currentState = ButtonState.Opened;
                break;
        }
    }

    private void InstantiateInfoCard()
    {
        if (instantiatedInfoCard == null)
        {
            float distanceFromUser = 2.0f;
            Vector3 userPosition = Camera.main.transform.position;
            Vector3 userForward = Camera.main.transform.forward;
            Vector3 spawnPosition = userPosition + userForward * distanceFromUser;
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            instantiatedInfoCard = Instantiate(infoCardPrefab, spawnPosition, rotation);
        }
    }

    private enum ButtonState
    {
        
        Collected,
        Opened
    }
}
