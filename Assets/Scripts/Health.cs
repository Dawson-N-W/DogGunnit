using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health = 100;

    public int MAX_HEALTH = 100;

    GameObject player;
    GameObject enemy;
    GameObject spawner;
    public GameObject bullet;
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        spawner = GameObject.Find("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHealth(int maxHealth, int health){
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    


    public void Damage(int amount){

        if(amount < 0){
            throw new System.ArgumentException("Damage amount must be positive");
        }

        health -= amount;
        //make enemies red in a flash
        if(gameObject.tag == "Enemy"){
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //wait a split second and change back to normal
            Invoke("ResetColor", 0.3f);
            
        }

        if(health <= 0){
            if(player != null && player.GetComponent<Health>() != null && player.GetComponent<Health>().health <= 0){
                //restart game
                if (bullet != null) {
                 bullet.gameObject.GetComponent<BulletBehav>().resetDamage();
                }           
                SceneManager.LoadScene("Main Menu");
            }
            else{
            player.GetComponent<Score>().currentScore += 100;
            //get current enemy location
            Vector3 enemyLocation = gameObject.transform.position;
            spawner.GetComponent<EnemySpawner>().EnemyDestroyed(enemyLocation);
            
            Destroy(gameObject);
            }
        }
    }

    void ResetColor(){
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Heal(int amount){
        if(amount < 0){
            throw new System.ArgumentException("Health amount must be positive");
        }

        health += amount;
        if(health > MAX_HEALTH){
            health = MAX_HEALTH;
        }
    }
}
