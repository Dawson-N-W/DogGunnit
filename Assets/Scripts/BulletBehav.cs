using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    public GameObject player;
    public GameObject smallEnemyPrefab;
    public GameObject bigEnemyPrefab;
    public int damage = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
            float speed = collision.gameObject.GetComponent<EnemyBehavior>().speed;
            //stop enemy movement
            collision.gameObject.GetComponent<EnemyBehavior>().stopMovement();
            collision.gameObject.GetComponent<EnemyBehavior>().StartCoroutine(collision.gameObject.GetComponent<EnemyBehavior>().pauseMovement());
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Dog House"))
        {
            Destroy(gameObject);
        }
    }

    public void resetDamage(){
        damage = 25;
    } 

}
