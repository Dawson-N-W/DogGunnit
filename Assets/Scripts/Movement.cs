using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    public float defaultSpeed = 8f;
    public float speed = 8f;
    private Rigidbody2D rb;
    public Vector2 Direction;
    public float magnitude;
    private Camera mainCamera;
    private float spriteHalfWidth, spriteHalfHeight;
    public GameObject gun;
    public float shooting;
    public bool isRunning;
    public bool isIdling;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
        gun = GameObject.Find("Gun");
        // Get a reference to the main camera
        mainCamera = Camera.main;

        // Get half the width and height of the sprite
        spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        spriteHalfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        Animate();
        ClampToScreen();
    }

    public void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        

        if (Horizontal == 0 && Vertical == 0)
        {
        
            rb.velocity = new Vector2(0, 0);
            magnitude = 0;
            return;
        }
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                Debug.Log("Shift");
                this.speed = 0f;
            }
            else{
                this.speed = defaultSpeed;
            }

        Direction = new Vector2(Horizontal, Vertical);
        magnitude = Direction.magnitude;

        

        rb.velocity = new Vector2(Horizontal * this.speed, Vertical * this.speed);
    }

    private void Animate()
    {
        animator.SetFloat("X", Direction.x);
        animator.SetFloat("Y", Direction.y);
        animator.SetFloat("AnimMoveMagnitude", magnitude);
        if(magnitude > 0){
            /*
            if(isIdling){
                StartCoroutine(pause());
            }
            */
            isRunning = true;
            isIdling = false;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdling", isIdling);
        }
        else{
            /*
            if(isRunning){
                StartCoroutine(pause());
            }
            */
            isRunning = false;
            isIdling = true;
            animator.SetBool("isIdling", isIdling);
            animator.SetBool("isRunning", isRunning);
        }
        animator.SetFloat("isShooting", gun.gameObject.GetComponent<Shoot>().isShooting);
    }
    /*
    public IEnumerator pause(){
        GameObject gun = GameObject.Find("Gun");
        gun.gameObject.GetComponent<Shoot>().canFire = false;
        yield return new WaitForSeconds(0.1f);
        gun.gameObject.GetComponent<Shoot>().canFire = true;
    }
    */

    private void ClampToScreen()
    {
        // Get the bounds of the camera view in world space
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float xMin = mainCamera.transform.position.x - cameraWidth / 2f;
        float xMax = mainCamera.transform.position.x + cameraWidth / 2f;
        float yMin = mainCamera.transform.position.y - cameraHeight / 2f;
        float yMax = mainCamera.transform.position.y + cameraHeight / 2f;

        // Clamp the sprite's position to the camera bounds
        float clampedX = Mathf.Clamp(transform.position.x, xMin + spriteHalfWidth, xMax - spriteHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, yMin + spriteHalfHeight, yMax - spriteHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
