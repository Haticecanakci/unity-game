using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;       // Karakter
    public Vector3 offset = new Vector3(0, 2, -4);
    public float rotationSpeed = 3f;
    public float followSmoothness = 10f;

    float yaw; // Y ekseni etrafýnda dönme açýsý

    void LateUpdate()
    {
        if (!target) return;

        // Fare hareketiyle kamerayý döndür
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;

        // Kamera rotasyonunu hesapla
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Kamerayý yumuþak þekilde hedef pozisyona taþý
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmoothness * Time.deltaTime);

        // Kamerayý karaktere çevir
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
