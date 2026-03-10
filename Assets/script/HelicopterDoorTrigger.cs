using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HelicopterDoorTrigger : MonoBehaviour
{
    private void Awake()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true; // garanti
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("HelicopterDoorTrigger: Player entered door trigger.");
        // GameManager'ý bulup WinPlayer çaðýr
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.WinPlayer();
    }
}
