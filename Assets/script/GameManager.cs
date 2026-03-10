using UnityEngine;

public class GameManager : MonoBehaviour
{
    static bool _hasPlayerWon = false;
    public static bool HasPlayerWon => _hasPlayerWon;

    [Header("Win UI")]
    public GameObject winPanel; // baðlayýn (Canvas içindeki WinPanel)

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
    }

    // Çaðrýlan fonksiyon — helikopter kapý trigger'ýndan veya baþka yerden çaðrýlacak
    public void WinPlayer()
    {
        if (_hasPlayerWon) return;

        _hasPlayerWon = true;
        Debug.Log("GameManager: Player has won!");

        if (winPanel != null)
            winPanel.SetActive(true);

        // Oyunu durdur
        Time.timeScale = 0f;
    }
}
