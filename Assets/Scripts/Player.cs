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

    private int berrycount;
    private int meatCount;
    private SpriteRenderer spriteRenderer;

    public TMP_Text Text_berry;
    public TMP_Text Text_meat;
    public TMP_Text gameOverText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        // Rb2D.linearVelocity = new Vector2(move * speed, Rb2D.linearVelocity.y);

        
        // if(move != 0)
        // {
        //     transform.localScale = new Vector3(Mathf.Sign(move) * localScale.x, localScale.y, localScale.z);          
        // }
        Rb2D.linearVelocity = new Vector2(
            move * speed,
            Rb2D.linearVelocity.y
        );

        // Girar el personaje
        if (move != 0)
        {
            spriteRenderer.flipX = move < 0;
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
            berrycount++;
            Text_berry.text = berrycount.ToString();
        }
        
        if(collision.transform.CompareTag("meat"))
        {
            Destroy(collision.gameObject);
            meatCount++;
            Text_meat.text = meatCount.ToString();
        }

        if(collision.transform.CompareTag("end"))
        {
            if(GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }

        if(collision.transform.CompareTag("hyena"))
        {
            if(GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            
        }

        if(collision.transform.CompareTag("snake"))
        {
            if(GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }

        if(collision.transform.CompareTag("barril"))
        {
            Vector2 knockbackDir= (Rb2D.position - (Vector2)collision.transform.position).normalized;
            Rb2D.linearVelocity = Vector2.zero;
            Rb2D.AddForce(knockbackDir * 3, ForceMode2D.Impulse);

            BoxCollider2D[] colliders = collision.gameObject.GetComponents<BoxCollider2D>();

            foreach(BoxCollider2D col in colliders){
                col.enabled = false;
            }

            collision.GetComponent<Animator>().enabled = true;
            Destroy(collision.gameObject, 0.5f);
        }
    }


}
