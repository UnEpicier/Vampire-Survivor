using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Character stats
    [Header("Character Stats")]
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float HealValue = 1f;
    public float Speed = 2f;
    public float Exp = 0f;
    public float Level = 1f;
    public float OrbAbsorberRadius = 0.7f;

    // Weapons stats
    [Header("Weapons Stats")]
    public float BeamDamages = 50f;
    public float BeamFrequency = 5f;
    public float BeamLife = 0.5f;

    public float SwordsHaloRadius = 1f;
    public float SwordsDamages = 25f;
    public float SwordsQuantity = 5f;

    public float ArrowDamages = 15f;
    public float ArrowFrequency = 2f;
    public float ArrowRange = 5f;

    public float laserOnX = 0f;
    public float laserOnY = 0f;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject _upgradeScreen;

    private void Start()
    {
        InvokeRepeating(nameof(Heal), 1f, 1f);
    }

    private void Update()
    {
        if (BeamFrequency < 0f)
        {
            BeamFrequency = 0f;
        }

        if (ArrowFrequency < 0.5f)
        {
            ArrowFrequency = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // --- CHARACTER STATS ---------------------------------------------------------------------
    public void TakeDamage(int value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Health = 0;

            Time.timeScale = 0f;
            deathScreen.SetActive(true);
            deathScreen.GetComponent<DeathScreen>().ShowScreen();
        }
    }
    
    public void AddExperience(int value)
    {
        Exp += value;

        if (Exp >= Level * 10)
        {
            Exp -= Level * 10;
            Level++;
            Time.timeScale = 0f;
            _upgradeScreen.SetActive(true);
            _upgradeScreen.GetComponentInChildren<UpgradeScreen>().GenerateChoice();
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
