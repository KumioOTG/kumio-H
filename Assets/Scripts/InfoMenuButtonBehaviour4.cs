using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class InfoMenuButtonBehaviour4 : MonoBehaviour
{
    [SerializeField] private GameObject infoCardInScene;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite normalIcon;
    [SerializeField] private Sprite collectedIcon;
    [SerializeField] private Sprite openedIcon;
    private ButtonState currentState = ButtonState.Collected;
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnClick.AddListener(ToggleInfoCard);

        // Ensure the info card in the scene is initially deactivated
        if (infoCardInScene != null)
        {
            infoCardInScene.SetActive(false);
        }
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
    }

    private enum ButtonState
    {
        Collected,
        Opened
    }
}
