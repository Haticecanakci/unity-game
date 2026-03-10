using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject winPanel;

    void Update()
    {
        if (GameManager.HasPlayerWon)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    

}
