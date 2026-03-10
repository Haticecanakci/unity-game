using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float gravity = 20f;

    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // 🎯 Animator alındı
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Kamera yönüne göre hareket yönünü belirle
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // 🏃‍♂️ Koşma tuşu
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? moveSpeed * 2f : moveSpeed;

        // Hareket yönü
        Vector3 moveDirection = forward * vertical + right * horizontal;
        moveDirection *= currentSpeed;

        // 🎞️ Animator parametresi (yürüyüş/koşu)
        float speed = new Vector2(horizontal, vertical).magnitude * currentSpeed;
        animator.SetFloat("Speed", speed);

        // 🟢 Zıplama işlemi
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
                animator.SetBool("IsJumping", true);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // Karakteri hareket ettir
        controller.Move(moveDirection * Time.deltaTime);

        // 🟣 Yere indiğinde zıplama durumunu sıfırla
        if (controller.isGrounded && animator.GetBool("IsJumping"))
            animator.SetBool("IsJumping", false);

        // 🔄 Karakterin yönünü çevir
        Vector3 lookDir = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (lookDir.magnitude > 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
        }
    }
}
