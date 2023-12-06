using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    [SerializeField] private CoinType coinType;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite collectedIcon;
    [SerializeField] private Sprite notCollectedIcon;

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

    //public void OnButtonClick()
    //{
        //gameManager.TryReactivateCoin(coinType);
    //}
}
