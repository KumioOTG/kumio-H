using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using System.Diagnostics;
using System.Collections;

[DebuggerDisplay("Collectable Object: {gameObject.name}")]
public class CoinBehaviour : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField]
    private GameObject objectToCollect;
    [SerializeField] private GameObject soundPrefab; // Sound prefab for collection sound effect
    [SerializeField] private CoinType type; // Define the type of the coin

    private bool isCollected = false;
    private int airTapCount = 0;
    private float tapTimeLimit = 0.5f;

    public event System.Action OnCollect;

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (!isCollected && eventData.Pointer != null && eventData.Pointer.Result.CurrentPointerTarget == gameObject)
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
                UnityEngine.Debug.Log("Collecting " + objectToCollect.name);
                isCollected = true;
                UnityEngine.Debug.Log(isCollected);
                objectToCollect.SetActive(false); // Deactivate the coin

                GameManager.Instance.CollectCoin(type);// Notify GameManager about the collection
                UIManager.Instance.UpdateButtonSprite(type, true); // true for collected

                // Play collect sound and other collection logic
                if (soundPrefab != null)
                {
                    GameObject soundInstance = Instantiate(soundPrefab, objectToCollect.transform.position, Quaternion.identity);
                    AudioSource audioSource = soundInstance.GetComponent<AudioSource>();
                    audioSource.Play();
                    Destroy(soundInstance, audioSource.clip.length);
                }
            }
        }
    }

    private IEnumerator ResetAirTapCount()
    {
        yield return new WaitForSeconds(tapTimeLimit);
        airTapCount = 0;
    }

   

    // Other interface methods
    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}