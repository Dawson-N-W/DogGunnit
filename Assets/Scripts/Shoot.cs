using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public GameObject enemy;
    public AudioClip shootSound;
    public float bulletSpeed = 10f;
    public float fireRate = 0.01f;
    public bool canFire = true;
    public float isShooting = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Fire());
        //Fire();
    }

    

    public IEnumerator Fire(){
        if(Input.GetKeyDown("space") && canFire && GetComponent<GunBehavior>().isReloading == false)
        {
        isShooting = 1f;
        yield return new WaitForSeconds(0.10f);

        // Play the shoot sound
        AudioSource.PlayClipAtPoint(shootSound, transform.position);


        canFire = false;
        GameObject newBullet = Instantiate(bullet, player.transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
        
        // Set the bullet's direction vector based on the player's movement direction
        Vector2 direction = player.GetComponent<Movement>().Direction;

        // Rotate the bullet to face the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Add force to the bullet in the direction of the player's movement
        newBullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        
        //shoot one bullet at a time
        this.GetComponent<GunBehavior>().DecreaseAmmo();
        //isShooting = 0f;
        StartCoroutine(FireCooldown());
    }  
    }

    private IEnumerator FireCooldown() {
        //yield return new WaitForSeconds(0.1f);
        isShooting = 0f;
        yield return new WaitForSeconds(fireRate);
        
        canFire = true;
    }
}
