using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float Health = 100f;
    public float Speed = 10f;
    public int Exp = 0;
    public int Level = 1;

    public void AddExperience(int value)
    {
        Exp += value;

        if (Exp >= Level * 10)
        {
            Exp -= Level * 10;
            Level++;
        }
    }
}
