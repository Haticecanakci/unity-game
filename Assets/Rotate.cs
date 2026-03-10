using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 45f;

    void Update()
    {
        transform.Rotate(_rotateSpeed * Time.deltaTime, 0f, 0f);
    }
}
