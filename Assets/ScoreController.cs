using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] int scoreMultiplier;
    [SerializeField] int nearMissBonus;
    float currentScore;
    [SerializeField] Text scoreText;
    [SerializeField] Canvas playerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null)
        {
            scoreText = playerCanvas.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentScore += scoreMultiplier * Time.deltaTime;
        
        scoreText.text = "Current Score : " + currentScore.ToString();
    }

    
    public void ResetScore()
    {
        currentScore= 0f;
    }
    public void ScoreBoost()
    {

        { Debug.Log("Hello NearMiss + " + nearMissBonus); }
        currentScore +=   nearMissBonus; 
    }
}
