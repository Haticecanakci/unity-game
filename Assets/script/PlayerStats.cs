using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int zombiesKilled = 0;

    public int ZombiesKilled
    {
        get => zombiesKilled;
        set => zombiesKilled = value;

    }
    

}
