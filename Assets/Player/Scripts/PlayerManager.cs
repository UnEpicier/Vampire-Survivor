using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Character stats
    public int Health = 100;
    public int MaxHealth = 100;
    public int HealValue = 1;
    public float Speed = 2f;
    public int Exp = 0;
    public int Level = 1;

    // Weapons stats
    public int BeamDamages = 50;
    public int SwordsDamages = 25;
    public int ArrowDamages = 15;

    // Kills stats
    public int MayorKills = 0;
    public int MinautorKills = 0;
    public int BringerOfDeathKills = 0;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject deathScreen;

    private void Start()
    {
        InvokeRepeating(nameof(Heal), 1f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void AddExperience(int value)
    {
        Exp += value;

        if (Exp >= Level * 10)
        {
            Exp -= Level * 10;
            Level++;
        }
    }

    // --- CHARACTER STATS ---------------------------------------------------------------------
    public void TakeDamage(int value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Health = 0;

            // Death animation + menu
            Time.timeScale = 0f;
            deathScreen.GetComponent<DeathScreen>().ShowScreen();
        } else
        {
            // Damage animation
        }
    }

    public void Heal()
    {
        Health += HealValue;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}
