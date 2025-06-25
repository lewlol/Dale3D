using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float x;

    public GameObject groundCheck;
    public LayerMask groundLayer;

    [Header("Shown Stats")]
    public float speed;
    public float jumpForce;
    [Space]

    [Header("Hidden Stats")]
    public float fallMultiplier;
    public float lowJumpMultipler;

    bool isGrounded;
    bool canJump;

    float groundCheckRadius = 0.45f;

    private void Awake()
    {
        canJump = true;
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            StartCoroutine(Jump());
        }

        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }

        isGrounded = Grounded();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, 0);
    }

    IEnumerator Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        canJump = false;
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }

    private bool Grounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);

        if(colliders.Length > 0) return true;
        else return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}
