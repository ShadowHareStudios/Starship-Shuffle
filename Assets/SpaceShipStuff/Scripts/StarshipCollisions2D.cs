using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarshipCollisions2D : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] GameObject Spaceship;
    [SerializeField] NearMissTrigger NearMissCheck;
    [SerializeField] float pointPenalty;
   
   

    private void Start()
    {
        if(Spaceship == null) { Debug.Log("You forgot to add a Ship reference"); }
        if (NearMissCheck == null) { Debug.Log("You forgot to add a NearMiss reference"); }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other == Spaceship) { Debug.Log("Hello Critical Hit, Reset Score"); }
        scoreController.ScoreReset();
    }

    public void OnParticleTrigger()
    {
        scoreController.ScoreBonus();
    }

}
