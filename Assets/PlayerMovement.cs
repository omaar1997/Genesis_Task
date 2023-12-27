using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerInput = 0;

    [SerializeField] private float speed = 200;

    private Rigidbody2D rb;

    public Animator animator;

    private bool isIdle;

    public AudioSource collectAudio;
    public AudioSource WinAudio;

    private float counter = 6;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {  
        playerInput = Input.GetAxisRaw("Horizontal");

        //animator.SetFloat("Speed", Mathf.Abs(rb.velocity));
        if(isIdle == true){
            animator.SetBool("IdleState", false);
        }
        else if(isIdle == false){
            animator.SetBool("IdleState", true);
        }

        FaceDirection();
        Winner();
    }
    void FaceDirection()
    {
        if (playerInput > 0)
        {
            transform.localScale = new Vector3( Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z );
            isIdle = true;
        }

        else if (playerInput < 0)
        {
            transform.localScale = new Vector3( -1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z );
            isIdle = true;
        }
        else if (playerInput == 0){
            isIdle = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerInput * speed * Time.fixedDeltaTime, 0f);
        
        /*
        if(rb.velocity){
            animator.SetBool("IdleState", false);
        }
        else{
            animator.SetBool("IdleState", true);
        }
        */
        
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "CoinTag"){
            collectAudio.Play();
            Debug.Log("Collided");
            Destroy(collision.gameObject);
            counter--;
            Debug.Log(counter);
        }
    }

    void Winner(){
        if(counter == 0  && !WinAudio.isPlaying){
            WinAudio.Play();
        }
    }
}