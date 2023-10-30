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


public enum CoinTypeTutorial
{
    AVREA,
    SAECLA,
    GERIT,
    QUI,
    PORTAM,
    CONSTRVIT,
    AURO
}

public enum FloatingMenuTypeTutorial
{
    Coin,
    Info,
    Object,
    Route
}

[System.Serializable]
public class InfoCardDataTutorial
{
    public GameObject infoCardObject;
    public AudioClip relatedAudio;
}

public class InfoCardTutorial
{
    public GameObject infoCardObject;
    public AudioClip relatedAudio;
}

public class ManagerTutorial : MonoBehaviour
{
    public List<CoinBehaviourTutorial> coins;
    public List<AudioClip> listenedNarrations;
    public List<CollectibleObjectBehaviour> collectedObjects;
    public List<InfoCardTutorial> infoCards;

    [SerializeField]
    private List<InfoCardDataTutorial> collectedInfoCardsSerialized = new List<InfoCardDataTutorial>();
    private List<InfoCardTutorial> collectedInfoCards;

    private AudioClip narrationToAddToListened;

    private void Awake()
    {
        collectedInfoCards = new List<InfoCardTutorial>();
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

    public void ReleaseCoin(CoinTypeTutorial coin)
    {
        if (coins[(int)coin] != null)
        {
            coins[(int)coin].Activate();
            coins[(int)coin] = null;
        }
    }

    public InfoCardTutorial GetInfoCardByAudio(AudioClip audio)
    {
        return infoCards.FirstOrDefault(infoCard => infoCard.relatedAudio == audio);
    }

    public void ReleaseInfoCard(InfoCardTutorial infoCard)
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
