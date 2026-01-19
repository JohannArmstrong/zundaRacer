using Mirror;
using UnityEngine;

public class PlayerGroundCheck : NetworkBehaviour
{
    [SerializeField] LayerMask floorMask;
    
    private CapsuleCollider2D col;
    private Animator anim;

    [SyncVar]
    public bool IsGrounded;

    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponentInChildren<Animator>();

        if (col == null)
        {
            Debug.LogError("PlayerGroundCheck's CapsuleCollider2D not found in object");
        }
    }

    void FixedUpdate()
    {
        if (!isServer) return;

        int layerMask = LayerMask.GetMask("Floor");

        // Tamaño real del collider
        Vector2 size = col.size * transform.lossyScale;
        Vector2 offset = col.offset * transform.lossyScale;

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
            IsGrounded = false;
            //anim.SetBool("Jump", true);
            //Debug.Log("rayHit = null; inJump:" + inJump);
            return;
        }

        // if on the floor
        if (rayHit.transform.CompareTag("Floor") && !IsGrounded)
        {
            IsGrounded = true;
            //anim.SetBool("Jump", false);
            //Debug.Log("rayHit = Floor; inJump:" + inJump);
        }


        //Debug.LogError("IsGrounded: " + IsGrounded);
    }
}
