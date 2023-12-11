using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;

public class ObjectActivator2 : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private ObjectBehaviour objectBehaviour;

    private void OnEnable()
    {
        if (objectBehaviour != null)
        {
            objectBehaviour.OnCollect += ActivateObject;
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
