using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI; // For MRTK Interactable
using Microsoft.MixedReality.Toolkit.Input; // For MRTK input handling
using System.Linq;
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

    [SerializeField]
    private List<Interactable> coinButtons; // MRTK Interactable buttons

    [SerializeField]
    private Sprite[] defaultSprites;
    [SerializeField]
    private Sprite[] collectedSprites;

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

    public void CoinCollected(CoinBehaviour coin)
    {
        int coinIndex = (int)coin.type;
        if (coins[coinIndex] != null)
        {
            coins[coinIndex] = null;
            UpdateButtonIcon(coinIndex, false); // Update to collected state
        }
    }

    public void ReleaseCoin(CoinType coin)
    {
        int coinIndex = (int)coin;
        if (coins[coinIndex] != null)
        {
            coins[coinIndex].Activate();
            coins[coinIndex] = null;
            UpdateButtonIcon(coinIndex, true); // Update to default state
        }
    }

    private void UpdateButtonIcon(int coinIndex, bool isDefault)
    {
        if (coinIndex < 0 || coinIndex >= coinButtons.Count) return;

        // Change the button's sprite
        Interactable button = coinButtons[coinIndex];
        Image buttonImage = button.transform.Find("UIButtonSpriteIcon").GetComponent<Image>(); // Replace "YourChildObjectName" with the actual name of the child object
        if (buttonImage != null)
        {
            buttonImage.sprite = isDefault ? defaultSprites[coinIndex] : collectedSprites[coinIndex];
        }
        else
        {
            // Handle cases where the Image component is not found
            Debug.LogError("Image component not found on the button's child");
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
