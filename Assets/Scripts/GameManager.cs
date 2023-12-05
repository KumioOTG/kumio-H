using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum CoinType
{
    AVREA,
    SAECLA,
    GERIT,
    QUI,
    PORTAM,
    CONSTRVIT,
    AURO
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<CoinType, bool> collectedCoins = new Dictionary<CoinType, bool>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeCollectedCoins();
    }

    private void InitializeCollectedCoins()
    {
        foreach (CoinType type in System.Enum.GetValues(typeof(CoinType)))
        {
            collectedCoins[type] = false; // Initially, no coins are collected
        }
    }

    public void CollectCoin(CoinType type)
    {
        if (collectedCoins.ContainsKey(type))
        {
            collectedCoins[type] = true;
            // Additional logic for when a coin is collected
        }
    }

    public bool IsCoinCollected(CoinType type)
    {
        return collectedCoins.ContainsKey(type) && collectedCoins[type];
    }

    public void ResetCollectedCoin(CoinType type)
    {
        if (collectedCoins.ContainsKey(type))
        {
            collectedCoins[type] = false;
            // Additional logic for resetting a single coin
        }
    }

    public void ResetCollectedCoins()
    {
        // Resetting the collection state
        foreach (CoinType type in System.Enum.GetValues(typeof(CoinType)))
        {
            collectedCoins[type] = false;
        }
    }


}
