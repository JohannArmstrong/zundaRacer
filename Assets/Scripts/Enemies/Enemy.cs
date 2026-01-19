using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("Movement")]
    private float moveSpeed;

    [SerializeField, Header("Attack")]
    private int attackPower;

    private Rigidbody2D rigid;
    private Vector2 moveDirection;
    private BoxCollider2D coraida;

    private Animator anim;
    private bool bFloor;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.left;
        coraida = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        bFloor = true;
    }


    void Update()
    {
        Move();
        ChangeMoveDirection();
        LookMoveDirection();
        HitFloor();
    }


    private void Move()
    {
        if (!bFloor) return;
        rigid.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigid.linearVelocityY);
    }

    // OverlapBox version
    private void ChangeMoveDirection()
    {
        int mask = LayerMask.GetMask("Floor");

        float dir = Mathf.Sign(moveDirection.x);
        if (dir == 0) return;

        Bounds b = coraida.bounds;

        // Posición del sensor delante del personaje
        Vector2 checkPos = new Vector2(
            b.center.x + (b.extents.x + 0.05f) * dir,
            b.center.y
        );

        // Tamaño del sensor (línea vertical)
        Vector2 boxSize = new Vector2(
            0.05f,
            b.size.y * 0.8f
        );

        // Debug visual
        Gizmos.color = Color.red;
        Debug.DrawLine(
            checkPos + Vector2.up * boxSize.y * 0.5f,
            checkPos - Vector2.up * boxSize.y * 0.5f
        );

        if (Physics2D.OverlapBox(checkPos, boxSize, 0f, mask))
        {
            moveDirection.x *= -1;
        }
    }


    private void LookMoveDirection()
    {
        if (moveDirection.x < 0.0f)
            transform.eulerAngles = Vector3.zero;
        else if (moveDirection.x > 0.0f)
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }


    private void HitFloor()
    {
        int layerMask = LayerMask.GetMask("Floor");

        // Tamaño real del collider
        Vector2 size = coraida.size * transform.lossyScale;
        Vector2 offset = coraida.offset * transform.lossyScale;

        // Calculamos dónde están exactamente los pies
        float footY = offset.y - size.y * 0.5f;

        // Ubicación del BoxCast: un poco debajo
        Vector2 rayPos = transform.position + new Vector3(0, footY - 0.05f, 0);

         // Ancho del raycast: levemente menor que el ancho del collider
        Vector2 raySize = new Vector2(size.x * 0.9f, 0.1f);

        // Debug visual
        Debug.DrawLine(
            rayPos - new Vector2(raySize.x / 2, 0),
            rayPos + new Vector2(raySize.x / 2, 0),
            Color.red
        );

        RaycastHit2D rayHit = Physics2D.BoxCast(rayPos, raySize, 0, Vector2.zero, 0, layerMask);

        // if in mid air
        if (rayHit.transform == null)
        {
            bFloor = false;
            anim.SetBool("IsIdle", !bFloor);
            //Debug.Log("rayHit = null; bFloor:" + bFloor);
            return;
        }

        // if on the floor
        if (rayHit.transform.CompareTag("Floor") && !bFloor)
        {
            bFloor = true;
            anim.SetBool("IsIdle", !bFloor);
            //Debug.Log("rayHit = Floor; bFloor:" + bFloor);
        }
    }


    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
    }
        
}
