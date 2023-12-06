using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public enum FloatingMenuType
{
    Coin,
    Info,
    Object,
    Route
}


public class Manager : MonoBehaviour
{
    public List<CoinBehaviour> coins; // Assuming each CoinBehaviour correctly represents a coin
    public List<CollectibleObjectBehaviour> collectedObjects;

    private void Start()
    {
        collectedObjects = new List<CollectibleObjectBehaviour>();
    }

    public void CheckAndAddToCollectedObjects(CollectibleObjectBehaviour obj)
    {
        if (!collectedObjects.Contains(obj))
        {
            collectedObjects.Add(obj);
        }
    }

    public bool IsCoinCollected(CoinType coinType)
    {
        int index = (int)coinType;
        return index >= 0 && index < coins.Count && coins[index] != null;
    }

    //public void TryReactivateCoin(CoinType coinType)
    //{
        //int index = (int)coinType;
        //if (index >= 0 && index < coins.Count && coins[index] != null)
        //{
            //coins[index].Reactivate(); // Assuming Reactivate is the method to make the coin active again
        //}
    //}
}