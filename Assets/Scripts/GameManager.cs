using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.Serializable]
    public class CoinData
    {
        public CoinType type;
        public CoinBehaviour coinPrefab;
        public Sprite defaultSprite;
        public Sprite collectedSprite;
        public Transform spawnPoint;
    }

    [SerializeField]
    private List<CoinData> coinDatas;
    private Dictionary<CoinType, CoinBehaviour> spawnedCoins = new Dictionary<CoinType, CoinBehaviour>();
    private Dictionary<CoinType, bool> coinCollectedStatus = new Dictionary<CoinType, bool>();

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

    public void CollectCoin(CoinType type)
    {
        coinCollectedStatus[type] = true;
        // Update UI Button for the collected coin
        UIManager.Instance.UpdateButtonSprite(type, true);
    }

    public void RespawnCoin(CoinType type)
    {
        if (!coinCollectedStatus[type]) return;

        if (spawnedCoins.ContainsKey(type) && spawnedCoins[type] != null)
        {
            Destroy(spawnedCoins[type].gameObject);
        }

        CoinData data = coinDatas.Find(coinData => coinData.type == type);
        if (data != null)
        {
            CoinBehaviour spawnedCoin = Instantiate(data.coinPrefab, data.spawnPoint.position, data.spawnPoint.rotation);
            spawnedCoins[type] = spawnedCoin;
        }

        // Reset UI Button to default state
        UIManager.Instance.UpdateButtonSprite(type, false);
    }

    public Sprite GetDefaultSprite(CoinType type)
    {
        return coinDatas.Find(coinData => coinData.type == type)?.defaultSprite;
    }

    public Sprite GetCollectedSprite(CoinType type)
    {
        return coinDatas.Find(coinData => coinData.type == type)?.collectedSprite;
    }
}
