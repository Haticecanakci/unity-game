using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static int kills = 0;
    private static KillCounter instance;

    [SerializeField] TextMeshProUGUI killText;

    void Awake()
    {
        instance = this;
        UpdateText();
    }

    public static void AddKill()
    {
        kills++;
        if (instance != null)
            instance.UpdateText();
    }

    void UpdateText()
    {
        if (killText != null)
            killText.text = "Kills: " + kills;
    }
}
