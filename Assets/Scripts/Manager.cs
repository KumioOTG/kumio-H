using CodeMonkey.Utils;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


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

[System.Serializable]
public class InfoCardData
{
    public GameObject infoCardObject;
    public AudioClip relatedAudio;
}

public class InfoCard
{
    public GameObject infoCardObject;
    public AudioClip relatedAudio;
}

public class Manager : MonoBehaviour
{
    public List<CoinBehaviour> coins;
    public List<AudioClip> listenedNarrations;
    public List<CollectibleObjectBehaviour> collectedObjects;
    public List<InfoCard> infoCards;

    [SerializeField]
    private List<InfoCardData> collectedInfoCardsSerialized = new List<InfoCardData>();
    private List<InfoCard> collectedInfoCards;

    private AudioClip narrationToAddToListened;

    private void Awake()
    {
        collectedInfoCards = new List<InfoCard>();
    }

    private void Start()
    {
        listenedNarrations = new List<AudioClip>();
        collectedObjects = new List<CollectibleObjectBehaviour>();
    }

    private void Update()
    {
        foreach (GameObject indexFingerTip in GameObject.FindGameObjectsWithTag("IndexTip"))
        {
            if (indexFingerTip.GetComponent<SphereCollider>() == null)
            {
                indexFingerTip.tag = "PlayerHand";
                Rigidbody rigidbody = indexFingerTip.AddComponent<Rigidbody>();
                rigidbody.useGravity = false;
                SphereCollider collider = indexFingerTip.AddComponent<SphereCollider>();
                collider.radius = 0.1f;
                collider.isTrigger = true;
            }
        }
    }

    public void CheckAndAddToListenedNarrations(AudioClip narration)
    {
        if (!listenedNarrations.Contains(narration))
        {
            narrationToAddToListened = narration;
            Invoke("AddToListenedNarrations", narration.length);
        }
    }

    private void AddToListenedNarrations()
    {
        listenedNarrations.Add(narrationToAddToListened);
        narrationToAddToListened = null;
    }

    public void ReleaseCoin(CoinType coin)
    {
        if (coins[(int)coin] != null)
        {
            coins[(int)coin].Activate();
            coins[(int)coin] = null;
        }
    }

    public InfoCard GetInfoCardByAudio(AudioClip audio)
    {
        return infoCards.FirstOrDefault(infoCard => infoCard.relatedAudio == audio);
    }

    public void ReleaseInfoCard(InfoCard infoCard)
    {
        if (infoCard != null && !infoCard.infoCardObject.activeSelf)
        {
            GameObject instantiatedInfoCard = Instantiate(infoCard.infoCardObject, infoCard.infoCardObject.transform.position, infoCard.infoCardObject.transform.rotation);
            instantiatedInfoCard.SetActive(true);
            collectedInfoCards.Add(infoCard);
        }
    }

    public void CheckAndAddToCollectedObjects(CollectibleObjectBehaviour obj)
    {
        if (!collectedObjects.Contains(obj))
        {
            collectedObjects.Add(obj);
        }
    }

    public void ReleaseObject(CollectibleObjectBehaviour obj)
    {
        if (!obj.gameObject.activeSelf)
        {
            obj.Activate();
        }
    }
}
