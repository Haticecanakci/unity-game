using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyChase : MonoBehaviour
{
    [Header("Target (Player)")]
    public Transform player;
    public string playerTag = "Player";

    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float turnSpeed = 8f;

    [Header("Detection")]
    public float detectRadius = 20f;
    public float chaseRadius = 15f;
    public float stopDistance = 1.2f;

    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(playerTag);
            if (go != null) player = go.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        float dist = toPlayer.magnitude;

        if (dist > detectRadius)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        if (dist <= chaseRadius)
        {
            Vector3 dir = new Vector3(toPlayer.x, 0, toPlayer.z);

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
            }

            if (dist > stopDistance)
            {
                Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
                controller.Move(move);
                animator.SetFloat("Speed", moveSpeed); // 🏃‍♂️ Run animasyonu
            }
            else
            {
                animator.SetFloat("Speed", 0); // 🧍 Idle animasyonu
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
