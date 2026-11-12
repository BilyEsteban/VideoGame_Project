using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    private bool direction; // true = derecha, false = izquierda

    public Transform groundEnemy;
    public float checkRadius;
    public float checkAheadDistance = 0.2f;
    public LayerMask groundLayer;

    public bool IsMoving;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();       
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundEnemy.position, Vector2.down, checkRadius);

        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        IsMoving = true;
        if (hit == false)
        {
            Girar();
        }

        animator.SetBool("IsMoving", IsMoving);
    }

    private void Girar()
    {
        direction = !direction;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *=-1;
        IsMoving = false;
        animator.SetBool("IsMoving", IsMoving);
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundEnemy.transform.position, groundEnemy.transform.position + Vector3.down * checkRadius);
        
    }

}
