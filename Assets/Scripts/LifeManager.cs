using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{ 
[SerializeField] int maxLives;
int currentLives;


    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
