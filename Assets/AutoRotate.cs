using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
