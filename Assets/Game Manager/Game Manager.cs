using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Kills stats
    public static int MayorKills = 0;
    public static int MinautorKills = 0;
    public static int BringerOfDeathKills = 0;

    // Can pause menu
    public static bool GameStarted = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
