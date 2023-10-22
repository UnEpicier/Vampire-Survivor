using System;
using UnityEngine;

[RequireComponent (typeof(PlayerMovements))]
public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject laser;

    [SerializeField] private GameObject arrow;

    private PlayerMovements _pm;
    private PlayerManager _manager;

    private float oldBeamFrequency;
    private bool oldBeamX;
    private bool oldBeamY;

    private float oldArrowFrequency;

    private void Start()
    {
        _pm = GetComponent<PlayerMovements>();
        _manager = GetComponent<PlayerManager>();

        oldBeamFrequency = _manager.BeamFrequency;
        oldBeamX = Convert.ToBoolean(_manager.laserOnX);
        oldBeamY = Convert.ToBoolean(_manager.laserOnY);
        oldArrowFrequency = _manager.ArrowFrequency;

        InvokeRepeating(nameof(ShootBeam), _manager.BeamFrequency, _manager.BeamFrequency);
        InvokeRepeating(nameof(ShootArrow), _manager.ArrowFrequency, _manager.ArrowFrequency);
    }

    private void FixedUpdate()
    {
        if (_manager.BeamFrequency != oldBeamFrequency)
        {
            if (_manager.BeamFrequency > 0f)
            {
                CancelInvoke(nameof(ShootBeam));
                InvokeRepeating(nameof(ShootBeam), _manager.BeamFrequency, _manager.BeamFrequency);
            }
            else
            {
                CancelInvoke(nameof(ShootBeam));
                ShootBeam();
            }
            oldBeamFrequency = _manager.BeamFrequency;
        }

        if (_manager.BeamFrequency == 0)
        {
            if (oldBeamX != Convert.ToBoolean(_manager.laserOnX) || oldBeamY != Convert.ToBoolean(_manager.laserOnY))
            {
                foreach (var beam in GameObject.FindGameObjectsWithTag("Beam"))
                {
                    Destroy(beam);
                }
                oldBeamX = Convert.ToBoolean(_manager.laserOnX);
                oldBeamY = Convert.ToBoolean(_manager.laserOnY);
                ShootBeam();
            }
        }

        if (_manager.ArrowFrequency != oldArrowFrequency)
        {
            CancelInvoke(nameof(ShootArrow));
            InvokeRepeating(nameof(ShootArrow), _manager.ArrowFrequency, _manager.ArrowFrequency);
        }
    }

    private void ShootBeam()
    {
        Instantiate(laser, transform.position, Quaternion.identity, transform);

        if (_manager.laserOnX > 0)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 180), transform);
        }

        if (_manager.laserOnY > 0)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 90), transform);
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 270), transform);
        }

    }

    private void ShootArrow()
    {
        // Get all enemies in given radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _manager.ArrowRange, LayerMask.GetMask("Enemy"));

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
}
