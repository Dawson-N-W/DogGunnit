using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePowerUp : MonoBehaviour
{
    GameObject player;
    public GameObject bullet;
    [SerializeField]
    public Text damageText;
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
            bullet.gameObject.GetComponent<BulletBehav>().damage *= 2;
            StartCoroutine(damageIncrease(gameObject));
            
        }
    }

    public IEnumerator damageIncrease(GameObject DamagePowerUp){
        Debug.Log("waiting");
        yield return new WaitForSeconds(10f);
        Debug.Log("wait finished");
        bullet.gameObject.GetComponent<BulletBehav>().damage /= 2;
        Destroy(DamagePowerUp);
    }
}
