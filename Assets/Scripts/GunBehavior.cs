using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    GameObject mag;
    Animator magAnim;
    public AudioClip reloadSound;
    public int clipSize;
    public bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        clipSize = 12;
        mag = GameObject.Find("Mag");
        magAnim = mag.GetComponent<Animator>();
        
        magAnim.SetBool("reloading", false);
        magAnim.SetInteger("mag_size", clipSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r") || clipSize == 0){
            StartCoroutine(reload());
        }
    }

    public void DecreaseAmmo(){
        if(clipSize > 0){
            this.clipSize--;
        }
        magAnim.SetInteger("mag_size", clipSize);
        Debug.Log("Ammo: " + this.clipSize);
        
    }

    private IEnumerator reload(){
        if(isReloading){
            yield break;
        }
        isReloading = true;
        //disable shoot script
        GetComponent<Shoot>().enabled = false;
        //play reload sound
        AudioSource.PlayClipAtPoint(reloadSound, transform.position);
        //take 5 seconds to reload
        Debug.Log("Reloading");
        magAnim.SetBool("reloading", true);
        yield return new WaitForSeconds(3f);
        this.clipSize = 12;
        magAnim.SetInteger("mag_size", clipSize);
        Debug.Log("Reloaded");
        //yield return new WaitForSeconds(1f);
        //enable shoot script
        GetComponent<Shoot>().enabled = true;
        magAnim.SetBool("reloading", false);
        isReloading = false;
    }
}
