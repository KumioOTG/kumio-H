using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;


public class UIManager2 : MonoBehaviour
{
    public static UIManager2 Instance { get; private set; }

    [System.Serializable]
    public class ObjectButton
    {
        public ObjectType type;
        public Interactable interactable; // Using MRTK's Interactable
        public Sprite defaultSprite;
        public Sprite collectedSprite;
        public ObjectBehaviour ObjectBehaviour; // Reference to the ObjectBehavior script
    }

    [SerializeField] private List<ObjectButton> objectButtons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // No need for DontDestroyOnLoad in UIManager
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Handle scene-specific initialization here if needed.

        // Check for collected objects and update button sprites
        foreach (ObjectButton objectButton in objectButtons)
        {
            bool isCollected = GameManager2.Instance.IsObjectCollected(objectButton.type);
            UpdateButtonSprite(objectButton.type, isCollected);
        }
    }

    public void SetObjectButtonReferences(List<ObjectButton> buttons)
    {
        objectButtons = buttons;
    }

    public void UpdateButtonSprite(ObjectType type, bool collected)
    {
        ObjectButton objectButton = objectButtons.Find(ob => ob.type == type);
        if (objectButton != null && objectButton.interactable != null)
        {
            // Update the UI button sprite here based on 'collected' state
            SpriteRenderer spriteRenderer = objectButton.interactable.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Sprite newSprite = collected ? objectButton.collectedSprite : objectButton.defaultSprite;
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found for type: " + type);
            }
        }
        else
        {
            Debug.LogError("ObjectButton or Interactable is null for type: " + type);
        }
    }

    public void OnObjectButtonClicked(int objectTypeInt)
    {
        ObjectType type = (ObjectType)objectTypeInt;
        ObjectButton objectButton = objectButtons.Find(ob => ob.type == type);
        if (objectButton != null && objectButton.ObjectBehaviour != null)
        {
            Debug.Log("Reactivating object and resetting sprite for type: " + type);
            objectButton.ObjectBehaviour.gameObject.SetActive(true); // Reactivate the object
            objectButton.ObjectBehaviour.ResetCollection(); // Reset the collection status

            // Inform the GameManager about the collected object
            GameManager2.Instance.ResetCollectedObject(type);
            UpdateButtonSprite(type, false); // Reset the button sprite to default
        }
        else
        {
            Debug.LogError("ObjectButton or ObjectBehavior is null for type: " + type);
        }
    }
}