using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Diagnostics;
using System.Collections;

[DebuggerDisplay("Collectable Object: {gameObject.name}")]
public class CoinBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField]
    private GameObject objectToCollect;

    [SerializeField]
    private GameObject soundPrefab; // Set this prefab in the inspector

    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 1;
    private int originalLayer;


    private bool isCollected = false;
    private static int numCollected = 0;
    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;

    private void Start()
    {
        numCollected = 0;
        


        // Register for parent notification
        ParentObjectController parentController = GetComponentInParent<ParentObjectController>();
        if (parentController != null)
        {
            OnCollect += parentController.CollectableObjectCollected;
        }
    }

    public event System.Action OnCollect;

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!isCollected)
        {
            if (eventData.Pointer == null || eventData.Pointer.Result.CurrentPointerTarget != objectToCollect)
            {
                return;
            }

            airTapCount++;

            if (airTapCount == 1)
            {
                StartCoroutine(ResetAirTapCount());
            }
            else if (airTapCount == 2)
            {


                // Add collect action here
                UnityEngine.Debug.Log("Collecting " + objectToCollect.name);
                isCollected = true;
                numCollected++;
                UnityEngine.Debug.Log(numCollected + " objects collected");

                // Notify parent about collection
                OnCollect?.Invoke();

                // Play collect sound
                if (soundPrefab != null)
                {
                    GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
                    AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                    audioSource.Play();
                    Destroy(soundInstance, audioSource.clip.length);
                }

                // Destroy the game object
                Destroy(objectToCollect);
                UnityEngine.Debug.Log("Destroyed " + objectToCollect.name);
            }
        }
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
    }

    // Implement other interface methods as needed
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void Activate()
    {
        transform.position = player.position + player.forward * spawnDistance;
        transform.rotation = player.rotation;
        isCollected = false; // reset the collected state
        gameObject.layer = originalLayer;
        GetComponent<Renderer>().enabled = true;
    }

    public static int GetNumCollected()
    {
        return numCollected;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

}
