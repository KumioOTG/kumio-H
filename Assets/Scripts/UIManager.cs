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
    }

    [SerializeField]
    private List<CoinButton> coinButtons;

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
            // Get the new sprite based on the collected state
            Sprite newSprite = collected ? GameManager.Instance.GetCollectedSprite(type) : GameManager.Instance.GetDefaultSprite(type);

            // Find the SpriteRenderer component in the children of the interactable
            SpriteRenderer spriteRenderer = coinButton.interactable.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Update the sprite of the SpriteRenderer
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer not found in CoinButton interactable for type: " + type);
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
        GameManager.Instance.RespawnCoin(type);
    }
}
