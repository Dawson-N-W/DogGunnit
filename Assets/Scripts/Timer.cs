using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] 
    public Text timerText;

    private float startTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //keep running count of time
        startTime += Time.deltaTime;
        //convert to minutes and seconds
        float minutes = Mathf.FloorToInt(startTime / 60);
        float seconds = Mathf.FloorToInt(startTime % 60);
        //display time
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
