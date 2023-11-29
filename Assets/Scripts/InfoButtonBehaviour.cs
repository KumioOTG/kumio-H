using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Manager gameManager;
    
  

    void Start()
    {
        gameManager = FindAnyObjectByType<Manager>();
        
    }

    void Update()
    {
        // No action needed.
    }

   
    
}
