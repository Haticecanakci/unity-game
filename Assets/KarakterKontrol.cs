using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public Animator animator;
    public float yuruSpeed = 5f;
    public float kosuSpeed = 10f;
    public float jumpForce = 5f;
    public Rigidbody rb;

    private bool isGrounded = true;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? kosuSpeed : yuruSpeed;

        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);

            // Hýzý gerçekten uygula
            Vector3 move = direction * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + move);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Zýplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
