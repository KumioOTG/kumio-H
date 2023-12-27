using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;

public class ObjectActivator4 : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private CoinBehaviour coinBehaviour;
    [SerializeField] private float activationDelay = 1.0f; // Default delay is 1 second

    private Coroutine activationCoroutine;

    private void OnEnable()
    {
        if (coinBehaviour != null)
        {
            coinBehaviour.OnCollect += StartActivationDelay;
        }
    }

    //private void OnDisable()
    //{
        //if (coinBehaviour != null)
        //{
           // coinBehaviour.OnCollect -= StartActivationDelay;
        //}
    //}

    private void StartActivationDelay()
    {
        if (activationCoroutine != null)
        {
            // Stop any ongoing activation coroutine if a new coin is collected
            StopCoroutine(activationCoroutine);
        }

        activationCoroutine = StartCoroutine(ActivateObjectWithDelay());
    }

    private IEnumerator ActivateObjectWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
