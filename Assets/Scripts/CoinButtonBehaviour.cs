using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite collectedIcon;
    [SerializeField] private Sprite notCollectedIcon;

    public CoinType coinType;
    public CoinBehaviour associatedCoin;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<Manager>();
        }

        UpdateButtonState();
    }

    private void Update()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        bool isCollected = gameManager.IsCoinCollected(coinType);

        icon.sprite = isCollected ? collectedIcon : notCollectedIcon;
        GetComponent<BoxCollider>().enabled = isCollected;
    }

    public void OnButtonPressed()
    {
        Manager.Instance.TryReactivateCoin(coinType);
    }

    public void ActivateCoin()
    {
        // Logic to activate the coin, for example:
        associatedCoin.gameObject.SetActive(true);
        // Plus, any additional logic needed to "reset" or "activate" the coin
    }

}
