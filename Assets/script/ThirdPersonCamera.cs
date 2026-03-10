using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;         // Takip edilecek hedef (Y-Bot)
    public Vector3 offset = new Vector3(0f, 2f, -4f);
    public float sensitivity = 3f;   // Fare hassasiyeti
    public float distance = 4f;      // Kamera uzaklýðý
    public float minYAngle = -30f;   // Aþaðý bakýþ limiti
    public float maxYAngle = 60f;    // Yukarý bakýþ limiti
    public float smoothSpeed = 10f;  // Kamera hareket yumuþaklýðý

    private float rotX;
    private float rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi kilitle
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Fare hareketlerini oku
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotX += mouseX;
        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY, minYAngle, maxYAngle);

        // Kamerayý döndür
        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
        if (Input.GetMouseButtonDown(1)) Cursor.lockState = CursorLockMode.None;

    }
}
