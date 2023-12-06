using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private UIManager.CoinButton[] coinButtons;

    private void Awake()
    {
        UIManager uiManager = FindObjectOfType<UIManager>(); // Find the UIManager specific to this scene
        if (uiManager != null)
        {
            uiManager.SetCoinButtonReferences(new List<UIManager.CoinButton>(coinButtons));
        }
        else
        {
            Debug.LogWarning("UIManager not found in the scene.");
        }
    }
}
