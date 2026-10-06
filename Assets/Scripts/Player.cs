using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D Rb2D;
    private float move;

    public float jumpForce = 4f;
    private bool isGrounded;
    public Transform grounCheck;
    public float checkRadius = 0.1f;
    public LayerMask whatIsGround;
    private Vector3 localScale;
    
    private Animator animator;

    private int coins;
    public TMP_Text coinText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        Rb2D.linearVelocity = new Vector2(move * speed, Rb2D.linearVelocity.y);

        if(move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move) * localScale.x, localScale.y, localScale.z);          
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Rb2D.linearVelocity = new Vector2(Rb2D.linearVelocity.x, jumpForce);
        }
        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", Rb2D.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }
    
    private void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(grounCheck.position, checkRadius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("berry"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinText.text = coins.ToString();
        }

        if(collision.transform.CompareTag("end"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
