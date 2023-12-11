using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;


public class ObjectBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] private GameObject objectToCollect;
    [SerializeField] private GameObject soundPrefab; // Sound prefab for collection sound effect
    [SerializeField] private ObjectType type; // Define the type of the object

    private bool isCollected = false;
    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;

    public event System.Action OnCollect;

    private void Awake()
    {
        Debug.Log($"[ObjectBehavior] Awake - {gameObject.name} at position {transform.position}");
    }

    private void OnEnable()
    {
        Debug.Log($"[ObjectBehavior] OnEnable - {gameObject.name} at position {transform.position}");
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log($"[ObjectBehavior] OnPointerDown - {gameObject.name} at position {transform.position}");

        if (!isCollected && eventData.Pointer != null && eventData.Pointer.Result.CurrentPointerTarget == gameObject)
        {
            Debug.Log($"Airtap count before - {airTapCount}");
            airTapCount++;
            Debug.Log($"Airtap count after - {airTapCount}");
            if (airTapCount == 1)
            {
                StartCoroutine(ResetAirTapCount());
            }
            else if (airTapCount == 2)
            {
                CollectObject();
            }
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log($"on pointer up detected");
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log($"on pointer clicked detected");
    }

    private void CollectObject()
    {
        Debug.Log("Collecting " + objectToCollect.name);
        isCollected = true;
        objectToCollect.SetActive(false); // Deactivate the object

        OnCollect?.Invoke(); // Invoke the OnCollect event

        GameManager2.Instance.CollectObject(type); // Notify GameManager2 about the collection
        UIManager2.Instance.UpdateButtonSprite(type, true); // true for collected

        // Play collect sound and other collection logic
        if (soundPrefab != null)
        {
            GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
            AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(soundInstance, audioSource.clip.length);
        }
    }

    public void ResetCollection()
    {
        isCollected = false;
        airTapCount = 0;
        Debug.Log($"[ObjectBehavior] ResetCollection - {gameObject.name} at position {transform.position}");
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
        isCollected = false;
    }

    // Other interface methods
    // public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
    // public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
