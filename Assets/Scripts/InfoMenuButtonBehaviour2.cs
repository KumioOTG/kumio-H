using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour2 : MonoBehaviour
{
    [SerializeField] private AudioSource firstAudioSource; // Assign this in the inspector
    [SerializeField] private AudioSource secondAudioSource; // Assign this in the inspector
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
        firstAudioSource.loop = false;
        secondAudioSource.loop = false;
    }

    private void Update()
    {
        // Here, 'firstAudioSource' should be used instead of 'audioSource'
        if (!firstAudioSource.isPlaying && instantiatedInfoCard == null && currentState == ButtonState.Normal)
        {
            icon.sprite = collectedIcon;
            currentState = ButtonState.Collected;
        }
    }

    private void ToggleInfoCard()
    {
        switch (currentState)
        {
            case ButtonState.Normal:
                if (!firstAudioSource.isPlaying)
                {
                    firstAudioSource.Play();
                }
                break;

            case ButtonState.Collected:
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
                if (instantiatedInfoCard != null)
                {
                    Destroy(instantiatedInfoCard);
                    instantiatedInfoCard = null;
                    icon.sprite = collectedIcon;
                    currentState = ButtonState.Collected;
                }

                if (!secondAudioSource.isPlaying)
                {
                    secondAudioSource.Play();
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
