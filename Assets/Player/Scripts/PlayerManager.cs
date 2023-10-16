using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int Health = 100;
    public int MaxHealth = 100;
    public float Speed = 2f;
    public int Exp = 0;
    public int Level = 1;

    [SerializeField]
    private GameObject pauseMenu;

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

    public void TakeDamage(int value)
    {
        Health -= value;
        if (Health < 0)
        {
            Health = 0;

            // Death animation + menu
        } else
        {
            // Damage animation
        }
    }

    public void Heal(int value)
    {
        Health += value;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}
