using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager2 : MonoBehaviour
{
    [SerializeField] private List<SnapObjectByTags> controlPoints;
    [SerializeField] private GameObject finalImage;
    [SerializeField] private GameObject objectToActivateOnCompletion1; // Add your first game object here
    [SerializeField] private GameObject objectToActivateOnCompletion2; // Add your second game object here
    [SerializeField] private bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool completionCheck = true;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            bool snapped = controlPoints[i].Snapped;
            completionCheck &= snapped;
            if (snapped)
            {
                controlPoints[i].GetObjectToSnap().gameObject.GetComponent<Collider>().enabled = false;
            }
        }

        if (completionCheck && !isCompleted)
        {
            finalImage.SetActive(true);
            //coin.Activate();
            isCompleted = true;

            // Activate the specified game objects on completion
            if (objectToActivateOnCompletion1 != null)
            {
                objectToActivateOnCompletion1.SetActive(true);
            }

            if (objectToActivateOnCompletion2 != null)
            {
                objectToActivateOnCompletion2.SetActive(true);
            }
        }
    }
}
