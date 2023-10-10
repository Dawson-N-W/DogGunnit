using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    public Text scoreText;

    public int currentScore = 0;
    public int previousScore = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(currentScore != previousScore){
            scoreText.text = "Score: " + currentScore;
            previousScore = currentScore;
        }
            
    }

    
}
