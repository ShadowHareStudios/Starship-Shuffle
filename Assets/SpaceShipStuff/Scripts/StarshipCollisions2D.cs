using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarshipCollisions2D : MonoBehaviour
{
    [SerializeField] ScoreController scoreController;
    [SerializeField] GameObject Spaceship;
    [SerializeField] NearMissTrigger NearMissCheck;
    [SerializeField] GameObject Asteroid;
    [SerializeField] float nearMissBonus;


    
    public void OnParticleCollision(GameObject other)
    {
        if (other == Spaceship) { Debug.Log("Hello Critical Hit"); }
        scoreController.ResetScore();
    }

    public void OnParticleTrigger()
    {
        { Debug.Log("Hello NearMiss"); }
        scoreController.ScoreBoost(nearMissBonus);
    }

}
