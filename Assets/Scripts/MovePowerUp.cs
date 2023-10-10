using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePowerUp : MonoBehaviour
{
    GameObject player;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moveSpeed = player.gameObject.GetComponent<Movement>().defaultSpeed;
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
            player.gameObject.GetComponent<Movement>().defaultSpeed += 20f;
            StartCoroutine(speedIncrease(gameObject));
            
        }
    }

    public IEnumerator speedIncrease(GameObject MovePowerUp){
        Debug.Log("waiting");
        yield return new WaitForSeconds(3f);
        Debug.Log("wait finished");
        player.gameObject.GetComponent<Movement>().defaultSpeed -= 20f;
        player.gameObject.GetComponent<Movement>().speed = player.gameObject.GetComponent<Movement>().defaultSpeed;
        Destroy(MovePowerUp);
    }
}
