using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Oyunu durdurmak istersen:
        Time.timeScale = 0f;
    }
}
