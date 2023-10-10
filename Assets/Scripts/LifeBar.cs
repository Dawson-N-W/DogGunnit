using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int health = player.GetComponent<Health>().health;
        if(health <= 40){
            //turn red
            GameObject.Find("Bone 5").GetComponent<SpriteRenderer>().color = Color.red;
        }
        if(health <= 30){
            GameObject.Find("Bone 4").GetComponent<SpriteRenderer>().color = Color.red;
        }
        if(health <= 20){
            GameObject.Find("Bone 3").GetComponent<SpriteRenderer>().color = Color.red;
        }
        if(health <= 10){
            GameObject.Find("Bone 2").GetComponent<SpriteRenderer>().color = Color.red;
        }
        if(health == 0){
            GameObject.Find("Bone 1").GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
