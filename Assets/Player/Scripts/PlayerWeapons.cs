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
    private PlayerManager _manager;

    private void Start()
    {
        _pm = GetComponent<PlayerMovements>();
        _manager = GetComponent<PlayerManager>();

        InvokeRepeating(nameof(ShootBeam), 5f, 5f);
        InvokeRepeating(nameof(ShootArrow), 2f, 2f);
    }

    private void ShootBeam()
    {
        GameObject defaultBeam = Instantiate(laser, transform.position, _pm.lookOnRight ? Quaternion.identity : Quaternion.Euler(0, 0, 180), transform);
        defaultBeam.GetComponent<LaserBeam>().Damages = _manager.BeamDamages;

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
            spawnedArrow.GetComponent<Arrow>().Damages = _manager.ArrowDamages;
        }
    }
}
