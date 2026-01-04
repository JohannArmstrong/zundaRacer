using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField, Header("Character movement")]
    private float moveSpeed;

    [SerializeField, Header("Jump")]
    private float jumpSpeed;

    [SerializeField, Header("Character")]
    private int hp;

    [SerializeField, Header("無敵時間-flash repetitions-")]
    private int damageTime;

    [SerializeField, Header("点滅時間-flash interval-")]
    private float flashTime;


    
    private Vector2 inputDirection;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool inJump; // _bJump
    private CapsuleCollider2D coraida;



    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        inJump = false;
        coraida = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Move();
        LookMoveDirection();
        //Debug.Log(hp);
        HitFloor();
    }

    private void Move()
    {
        rigid.linearVelocity = new Vector2(inputDirection.x * moveSpeed, rigid.linearVelocityY);
        anim.SetBool("Walk", inputDirection.x != 0.0f);
    }

    private void LookMoveDirection()
    {
        if (inputDirection.x > 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else if (inputDirection.x < 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HitEnemy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Muteki_Enemy"))
        {
            HitMuteki(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            FindFirstObjectByType<MainManager>().ShowGameClearUI();
            this.enabled = false;
            GetComponent<PlayerInput>().enabled = false;
        }
    }

    private void HitFloor()
    {
        int layerMask = LayerMask.GetMask("Floor");

        // Tamaño real del collider
        Vector2 size = coraida.size * transform.lossyScale;
        Vector2 offset = coraida.offset * transform.lossyScale;

        // Calculamos dónde están exactamente los pies
        float footY = (offset.y - size.y * 0.5f);

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
            inJump = true;
            anim.SetBool("Jump", inJump);
            Debug.Log("rayHit = null; inJump:" + inJump);
            return;
        }

        // if on the floor
        if (rayHit.transform.CompareTag("Floor") && inJump)
        {
            inJump = false;
            anim.SetBool("Jump", inJump);
            Debug.Log("rayHit = Floor; inJump:" + inJump);
        }
    }

    private void HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 4.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;

        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy);
            rigid.AddForce(Vector2.up * (jumpSpeed / 2), ForceMode2D.Impulse);
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(Damage());
        }
    }

    private void HitMuteki(GameObject enemy)
    {
        enemy.GetComponent<KillerKoyori>().PlayerDamage(this);
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        Color color = spriteRenderer.color;
        for(int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Dead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || inJump ) return;

        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        // inJump = true;
        // anim.SetBool("Jump", inJump);
    }

    public void Damage(int damage)
    {
        hp = Mathf.Max(hp - damage, 0);
        Dead();
    }

    public int GetHP()
    {
        return hp;
    }
}