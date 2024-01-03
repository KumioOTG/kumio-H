using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ToggleObjectOnButtonPress : MonoBehaviour
{
    public GameObject objectToToggle;

    private bool isObjectActive = false;

    private void Start()
    {
        objectToToggle.SetActive(isObjectActive);
    }

    public void ToggleObject()
    {
        isObjectActive = !isObjectActive;
        objectToToggle.SetActive(isObjectActive);
    }
}
