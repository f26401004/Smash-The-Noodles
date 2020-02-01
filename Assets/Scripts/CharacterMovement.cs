using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Animator anim;

    [Space]
    [Header("Stats")]
    public float speed = 7;
    public float jumpForce = 12;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        anim.SetFloat("HorizontalAxis", Mathf.Abs(x));

        Walk(dir);

        if(Input.GetButtonDown("Jump")){
            Jump(Vector2.up);
        }
    }
    
    private void Walk(Vector2 dir) {
        if(dir.x > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }else if(dir.x < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }
    
    private void Jump(Vector2 dir) {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }
}
