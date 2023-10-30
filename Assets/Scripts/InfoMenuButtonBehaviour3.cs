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
    private ButtonState currentState = ButtonState.Normal;
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
            case ButtonState.Normal:
                // Transition from Normal to Collected
                icon.sprite = collectedIcon;
                currentState = ButtonState.Collected;
                break;

            case ButtonState.Collected:
                // Instantiate the info card
                float distanceFromUser = 2.0f;
                Vector3 userPosition = Camera.main.transform.position;
                Vector3 userForward = Camera.main.transform.forward;
                Vector3 spawnPosition = userPosition + userForward * distanceFromUser;
                Quaternion rotation = Quaternion.Euler(0, 180, 0);
                instantiatedInfoCard = Instantiate(infoCardPrefab, spawnPosition, rotation);
                icon.sprite = openedIcon;
                currentState = ButtonState.Opened;
                break;

            case ButtonState.Opened:
                // Play the audio only when the info card is already instantiated
                if (instantiatedInfoCard != null)
                {
                    Destroy(instantiatedInfoCard);
                    instantiatedInfoCard = null;
                    icon.sprite = collectedIcon;
                    currentState = ButtonState.Collected;
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                break;
        }
    }

    private enum ButtonState
    {
        Normal,
        Collected,
        Opened
    }
}
