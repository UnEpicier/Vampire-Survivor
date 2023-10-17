using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerMovements))]
public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] private bool laserOnX;
    [SerializeField] private bool laserOnY;

    [SerializeField] private GameObject arrow;

    private PlayerMovements _pm;

    private void Start()
    {
        _pm = GetComponent<PlayerMovements>();
        InvokeRepeating(nameof(ShootBeam), 5f, 5f);
        InvokeRepeating(nameof(ShootArrow), 2f, 2f);
    }

    private void ShootBeam()
    {
        Instantiate(laser, transform.position, _pm.lookOnRight ? Quaternion.identity : Quaternion.Euler(0, 0, 180), transform);

        if (laserOnX)
        {
            Instantiate(laser, transform.position, _pm.lookOnRight ? Quaternion.Euler(0, 0, 180) : Quaternion.identity, transform);
        }

        if (laserOnY)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 90), transform);
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 270), transform);
        }

    }

    private void ShootArrow()
    {
        // Get all enemies in given radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 20f, LayerMask.GetMask("Enemy"));

        if (hits.Length <= 0)
        {
            return;
        }

        GameObject target = null;
        float shortestDistance = Mathf.Infinity;

        for(int i = 0; i < hits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, hits[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                target = hits[i].gameObject;
            }
        }

        // Shoot arrow
        if (target != null)
        {
            GameObject spawnedArrow = Instantiate(arrow, transform.position, Quaternion.identity);
            spawnedArrow.GetComponent<Arrow>().target = target;
        }
    }

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
