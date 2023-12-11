using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;

public class ObjectActivator3 : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private ObjectBehaviour[] objectsToCollect;

    private HashSet<ObjectBehaviour> collectedObjects = new HashSet<ObjectBehaviour>();

    private void OnEnable()
    {
        foreach (var obj in objectsToCollect)
        {
            if (obj != null)
            {
                obj.OnCollect += () => HandleObjectCollected(obj);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var obj in objectsToCollect)
        {
            if (obj != null)
            {
                obj.OnCollect -= () => HandleObjectCollected(obj);
            }
        }
    }

    private void HandleObjectCollected(ObjectBehaviour collectedObject)
    {
        if (collectedObject != null)
        {
            collectedObjects.Add(collectedObject);

            // Check if both specific objects are collected
            if (collectedObjects.Count == 2)
            {
                ActivateObject();
            }
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
