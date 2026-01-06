using Mirror;
using UnityEngine;

public class PlayerAnimationState : NetworkBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    PlayerGroundCheck ground;
    PlayerHealth health;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<PlayerGroundCheck>();
        health = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool("Grounded", ground.IsGrounded);
        anim.SetBool("Dead", health.IsDead);
    }
}
