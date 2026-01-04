using UnityEngine;

public class KillerKoyori : MonoBehaviour
{
    [SerializeField, Header("Movement")]
    private float moveSpeed;

    [SerializeField, Header("Attack")]
    private int attackPower;

    [SerializeField] private Transform mawaruPart;
    [SerializeField] private float mawaruSpeed;

    private Rigidbody2D rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        rigid.linearVelocity = new Vector2(Vector2.left.x * moveSpeed, rigid.linearVelocityY);
        mawaruPart.Rotate(0, 0, mawaruSpeed * Time.deltaTime);
    }

    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
    }
        
}