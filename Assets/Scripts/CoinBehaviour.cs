using CodeMonkey.Utils;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{


    public CoinType type;

    [SerializeField] private Manager gameManager;

    [SerializeField] private GameObject soundPrefab;
    private bool isCollected = false;
    private static int numCollected = 0;
    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;

    // Event to notify when the coin is collected
    public delegate void CoinCollectedHandler(GameObject collectedCoin);
    public event CoinCollectedHandler OnCollected;

    private void Start()
    {
        numCollected = 0;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!isCollected)
        {
            airTapCount++;

            if (airTapCount == 1)
            {
                StartCoroutine(ResetAirTapCount());
            }
            else if (airTapCount == 2)
            {
                UnityEngine.Debug.Log("Collecting " + gameObject.name);
                isCollected = true;
                numCollected++;
                UnityEngine.Debug.Log(numCollected + " coins collected");

                // Play collect sound
                if (soundPrefab != null)
                {
                    GameObject soundInstance = Instantiate(soundPrefab, transform.position, Quaternion.identity);
                    AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                    audioSource.Play();
                    Destroy(soundInstance, audioSource.clip.length);
                }

                // Notify about the collection
                OnCollected?.Invoke(gameObject);

                // Destroy the coin object
                Destroy(gameObject);
                UnityEngine.Debug.Log("Destroyed " + gameObject.name);
            }
        }
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

    public static int GetNumCollected()
    {
        return numCollected;
    }
}

