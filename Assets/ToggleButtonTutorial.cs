using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections.Generic;


public class ToggleButtonTutorial : MonoBehaviour
{
    public Sprite iconOpened;
    public Sprite iconCollected;
    public CoinBehaviourTutorial associatedCoin;
    public SpriteRenderer targetIcon;  // Make sure this is a SpriteRenderer as used previously

    private Button myButton;
    private bool isClickedBefore = false;

    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }

        if (targetIcon != null)
        {
            targetIcon.sprite = iconCollected;  // Default to iconCollected
        }

        // Subscribe to the OnCoinCollected event
        CoinBehaviourTutorial.OnCoinCollected += HandleCoinCollected;
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        CoinBehaviourTutorial.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(CoinBehaviourTutorial coin)
    {
        if (coin == associatedCoin)
        {
            targetIcon.sprite = iconCollected;  // Change to iconCollected when the coin is collected
        }
    }

    void OnButtonClick()
    {
        if (associatedCoin != null && !isClickedBefore)
        {
            targetIcon.sprite = iconOpened;
            associatedCoin.Activate();
            isClickedBefore = true;
        }
    }
}
