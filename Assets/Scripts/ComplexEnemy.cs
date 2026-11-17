using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float stoppingDistance = 10f;

    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public bool IsMoving;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector3 localScale;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        float distance = Vector2.Distance(transform.position, target.position);

        Vector2 movement = Vector2.zero;

        // Persigue al jugador solo si está en rango y hay suelo firme debajo
        if (distance < stoppingDistance && isGrounded)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            if (movement.x != 0)
            {
                transform.localScale = new Vector3(-Mathf.Sign(movement.x) * Mathf.Abs(localScale.x), localScale.y, localScale.z);
            }
            IsMoving = true;
            if (animator != null) animator.SetBool("IsMoving", IsMoving);
        }
        else
        {
            IsMoving = false;
            if (animator != null) animator.SetBool("IsMoving", IsMoving);
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
