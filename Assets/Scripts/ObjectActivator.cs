using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private CoinBehaviour coinBehaviour;

    private void OnEnable()
    {
        if (coinBehaviour != null)
        {
            coinBehaviour.OnCollect += ActivateObject;
        }
    }

    private void OnDisable()
    {
        if (coinBehaviour != null)
        {
            coinBehaviour.OnCollect -= ActivateObject;
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
