using CodeMonkey.Utils;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviourTutorial : MonoBehaviour, IMixedRealityPointerHandler
{


    public CoinTypeTutorial type;
    public delegate void CollectedHandler(CoinBehaviourTutorial coin);
    public static event CollectedHandler OnCoinCollected;

    [SerializeField] private ManagerTutorial gameManager;
    [SerializeField] private Transform player;
    [SerializeField] private NarratorManager narrator;
    [SerializeField] private AudioClip narration;
    [SerializeField] private List<AudioClip> followingNarrations;
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private GameObject soundPrefab2;
    [SerializeField] private float spawnDistance = 1f;

    private bool isCollected = false;
    private static int numCollected = 0;

    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;
    private int originalLayer;

    void Start()
    {
        gameManager = FindAnyObjectByType<ManagerTutorial>();
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        narrator = FindObjectOfType<NarratorManager>();
        originalLayer = gameObject.layer;
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

                isCollected = true;
                numCollected++;

                PlayCollectSound();
                PlayCollectSound2();
                Collect();
            }
        }
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // No action needed.
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Debug.Log("Coin activated: " + gameObject.name);

        // Position the coin in front of the player
        transform.position = player.position + player.forward * spawnDistance;

        // Adjust the rotation to make the coin face the player
        Vector3 directionToFace = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToFace, Vector3.up);

        isCollected = false;
        gameObject.layer = originalLayer;
        GetComponent<Renderer>().enabled = true;
    }

    private void Collect()
    {
        gameManager.coins[(int)type] = this;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        GetComponent<Renderer>().enabled = false;

        OnCoinCollected?.Invoke(this); // Trigger the event

        PlayCollectSound();
        PlayCollectSound2(); // Play additional sound
    }

    private void PlayCollectSound()
    {
        if (soundPrefab != null)
        {
            GameObject soundInstance = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
        }
    }

    private void PlayCollectSound2()
    {
        // Play additional sound
        if (soundPrefab2 != null)
        {
            GameObject soundInstance = Instantiate(soundPrefab2, transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
        }

        static int GetNumCollected()
        {
            return numCollected;
        }

        // Add any missing methods or event handlers you need for your game logic.
    }
}

