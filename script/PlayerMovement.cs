using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator ani; 
    [SerializeField] private LayerMask jumpableGround;

    private float dirx = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum movementState {idel, running, jumping, falling}
  
    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Isgrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {   
        movementState state;

        if(dirx > 0f){
            state = movementState.running;
            sprite.flipX = false;
        }
        else if(dirx < 0f){
             state = movementState.running;
             sprite.flipX = true;
        }
        else{
             state = movementState.idel;
        }

        if(rb.velocity.y >.1f){
            state = movementState.jumping;
        }
        else if(rb.velocity.y < -.1f){
            state = movementState.falling;
        }
        ani.SetInteger("state", (int)state);
    }
    
    private bool Isgrounded(){
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
