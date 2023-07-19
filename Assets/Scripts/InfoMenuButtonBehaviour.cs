using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
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

    private void Update()
    {
        if (!audioSource.isPlaying && instantiatedInfoCard == null)
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
                Destroy(instantiatedInfoCard);
                instantiatedInfoCard = null;
                icon.sprite = collectedIcon;
                currentState = ButtonState.Collected;
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
