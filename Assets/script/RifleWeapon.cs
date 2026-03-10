using UnityEngine;

public class RifleWeapon : MonoBehaviour
{
    [SerializeField] int _damageDealt = 25;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Ray mouseRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            Debug.DrawRay(mouseRay.origin, mouseRay.direction * 50, Color.red, 1.5f);

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                Debug.Log("Vurdu: " + hitInfo.transform.name);

                // 🔹 Parent'taki Health'i buluyor
                Health enemyHealth = hitInfo.transform.GetComponentInParent<Health>();

                // 🔹 Vurulan noktayı görselleştir
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = hitInfo.point;
                sphere.transform.localScale = Vector3.one * 0.1f;
                Destroy(sphere, 0.3f);

                if (enemyHealth != null)
                {
                    enemyHealth.Damage(_damageDealt);
                    Debug.Log("Düşmana hasar verildi! Kalan can: " + enemyHealth.CurrentHealth);
                }
            }
        }
    }
}
