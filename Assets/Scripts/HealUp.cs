using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUp : MonoBehaviour
{
    GameObject player;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            //set object invisible
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //turn off its collision
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            player.gameObject.GetComponent<Health>().health += 10;
            health = player.gameObject.GetComponent<Health>().health;
            if(player.gameObject.GetComponent<Health>().health > player.gameObject.GetComponent<Health>().MAX_HEALTH){
                player.gameObject.GetComponent<Health>().health = player.gameObject.GetComponent<Health>().MAX_HEALTH;
            }

        if(health >= 40){
            GameObject.Find("Bone 5").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(health >= 30){
            GameObject.Find("Bone 4").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(health >= 20){
            GameObject.Find("Bone 3").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(health >= 10){
            GameObject.Find("Bone 2").GetComponent<SpriteRenderer>().color = Color.white;
        }
        

        Destroy(gameObject);
            
        }
    }
}
