using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject Mayor;
    public GameObject Minautor;
    public GameObject BringerOfDeath;

    private float _radius = 5f;

    [SerializeField] private int spawnedMayors = 0;
    [SerializeField] private int spawnedMinautor = 0;
    [SerializeField] private int spawnedBringerOfDeath = 0;

    private readonly int mayorsLimit = 300;
    private readonly int minautorsLimit = 15;
    private readonly int bringerOfDeathLimit = 5;

    private void Awake()
    {
        _radius = Camera.main.orthographicSize * Camera.main.aspect + 2f;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 5f, 5f);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, Camera.main.orthographicSize * Camera.main.aspect + 2f);
    }

    private void Update()
    {
        spawnedMayors = Array.FindAll(GameObject.FindGameObjectsWithTag("Ennemy"), (value) => value.name.Contains("Mayor")).Length;
        spawnedMinautor = Array.FindAll(GameObject.FindGameObjectsWithTag("Ennemy"), (value) => value.name.Contains("Minautor")).Length;
        spawnedBringerOfDeath = Array.FindAll(GameObject.FindGameObjectsWithTag("Ennemy"), (value) => value.name.Contains("Bringer of Death")).Length;
    }

    private void SpawnWave()
    {
        // Determine how much enemies will spawn on this wave
        int enemiesAmount = Random.Range(20, 30);

        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject enemyToSpawn = ChooseEnemy();
            string enemyName = enemyToSpawn.name.Split(' ')[0];

            int retries = 0;

            while (
                (enemyName == "Mayor" && spawnedMayors >= mayorsLimit) ||
                (enemyName == "Minautor" && spawnedMinautor >= minautorsLimit) ||
                (enemyName == "Bringer of Death" && spawnedBringerOfDeath >= bringerOfDeathLimit)
            )
            {
                enemyToSpawn = ChooseEnemy();
                enemyName = enemyToSpawn.name.Split(' ')[0];

                retries++;
                if (retries >= 20)
                {
                    return;
                }
            }

            switch (enemyName)
            {
                case "Mayor":
                    spawnedMayors++;
                    break;
                case "Minautor":
                    spawnedMinautor++;
                    break;
                case "Bringer":
                    spawnedBringerOfDeath++;
                    break;
            }

            // Determine the spawn position on a circle around the player
            float angle = Mathf.PI * 2f / Random.Range(0f, 40f);
            Instantiate(
                enemyToSpawn,
                transform.position + new Vector3(Mathf.Cos(Random.Range(0f, 10f) * angle) * _radius, Mathf.Sin(Random.Range(0f, 10f) * angle) * _radius, 0),
                Quaternion.identity,
                transform
            );

        }
    }

    // Determine which enemy to spawn based on chance percentage
    private GameObject ChooseEnemy()
    {
        int enemyChance = Random.Range(0, 101);
        if (enemyChance > 80 && enemyChance <= 98)
        {
            return Minautor;
        }
        else if (enemyChance > 98 && enemyChance <= 100)
        {
            return BringerOfDeath;
        }
        else
        {
            return Mayor;
        }
    }
}
