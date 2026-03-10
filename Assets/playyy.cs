using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class playyy : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 6f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    Rigidbody rb;
    Vector2 moveInput;
    bool jumpPressed;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        GroundCheck();
    }

    void FixedUpdate()
    {
        Move();
        if (jumpPressed && isGrounded)
        {
            Jump();
            jumpPressed = false;
        }
    }

    // Player Input (Send Messages) ile çalýþacak metot isimleri:
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) jumpPressed = true;
        if (context.canceled) jumpPressed = false;
    }

    void Move()
    {
        Vector3 dir = new Vector3(moveInput.x, 0f, moveInput.y);
        if (dir.sqrMagnitude > 1f) dir.Normalize();
        Vector3 move = transform.TransformDirection(dir) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        float dist = GetComponent<Collider>().bounds.extents.y + groundCheckDistance;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, dist, groundMask);
        Debug.DrawRay(transform.position, Vector3.down * dist, isGrounded ? Color.green : Color.red);
    }
}
