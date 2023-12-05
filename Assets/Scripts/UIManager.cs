using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [System.Serializable]
    public class CoinButton
    {
        public CoinType type;
        public Interactable interactable; // Using MRTK's Interactable
        public Sprite defaultSprite;
        public Sprite collectedSprite;
        public CoinBehaviour coinBehaviour; // Reference to the CoinBehaviour script
    }

    [SerializeField] private List<CoinButton> coinButtons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateButtonSprite(CoinType type, bool collected)
    {
        CoinButton coinButton = coinButtons.Find(cb => cb.type == type);
        if (coinButton != null && coinButton.interactable != null)
        {
            // Find the SpriteRenderer component in the children of the interactable
            SpriteRenderer spriteRenderer = coinButton.interactable.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Sprite newSprite = collected ? coinButton.collectedSprite : coinButton.defaultSprite;
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found for type: " + type);
            }
        }
        else
        {
            Debug.LogError("CoinButton or Interactable is null for type: " + type);
        }
    }

    public void OnCoinButtonClicked(int coinTypeInt)
    {
        CoinType type = (CoinType)coinTypeInt;
        CoinButton coinButton = coinButtons.Find(cb => cb.type == type);
        if (coinButton != null && coinButton.coinBehaviour != null)
        {
            coinButton.coinBehaviour.gameObject.SetActive(true); // Reactivate the coin
        }
        else
        {
            Debug.LogError("CoinButton or CoinBehaviour is null for type: " + type);
        }
    }

}
