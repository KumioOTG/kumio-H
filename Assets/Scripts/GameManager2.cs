using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public enum ObjectType
{
    BUYUTEC,
    MERMI,
    KAVUK,
    YUZUK

}

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }

    private Dictionary<ObjectType, bool> collectedObjects = new Dictionary<ObjectType, bool>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeCollectedObjects();
    }

    private void InitializeCollectedObjects()
    {
        foreach (ObjectType type in System.Enum.GetValues(typeof(ObjectType)))
        {
            collectedObjects[type] = false; // Initially, no objects are collected
        }
    }

    public void CollectObject(ObjectType type)
    {
        if (collectedObjects.ContainsKey(type))
        {
            collectedObjects[type] = true;
            // Additional logic for when an object is collected
        }
    }

    public bool IsObjectCollected(ObjectType type)
    {
        return collectedObjects.ContainsKey(type) && collectedObjects[type];
    }

    public void ResetCollectedObject(ObjectType type)
    {
        if (collectedObjects.ContainsKey(type))
        {
            collectedObjects[type] = false;
            // Additional logic for resetting a single object
        }
    }

    public void ResetCollectedObjects()
    {
        // Resetting the collection state
        foreach (ObjectType type in System.Enum.GetValues(typeof(ObjectType)))
        {
            collectedObjects[type] = false;
        }
    }
}