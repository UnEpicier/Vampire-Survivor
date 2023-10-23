using System;
using UnityEngine;

[RequireComponent (typeof(PlayerMovements))]
public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject _beam;

    [SerializeField] private GameObject _arrow;

    private PlayerMovements _pm;
    private PlayerManager _manager;

    private float _oldBeamFrequency;
    private bool _oldHorizontalBeam;
    private bool _oldVerticalBeam;

    private float _oldArrowFrequency;

    private void Start()
    {
        _pm = GetComponent<PlayerMovements>();
        _manager = GetComponent<PlayerManager>();

        _oldArrowFrequency = _manager.BeamFrequency;
        _oldHorizontalBeam = Convert.ToBoolean(_manager.HorizontalBeam);
        _oldVerticalBeam = Convert.ToBoolean(_manager.VerticalBeam);

        InvokeRepeating(nameof(ShootBeam), _manager.BeamFrequency, _manager.BeamFrequency);
        InvokeRepeating(nameof(ShootArrow), _manager.ArrowFrequency, _manager.ArrowFrequency);
    }

    private void FixedUpdate()
    {
        if (_manager.BeamFrequency != _oldBeamFrequency)
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
            _oldBeamFrequency = _manager.BeamFrequency;
        }

        if (_manager.BeamFrequency == 0)
        {
            if (_oldHorizontalBeam != Convert.ToBoolean(_manager.HorizontalBeam) || _oldVerticalBeam != Convert.ToBoolean(_manager.VerticalBeam))
            {
                foreach (var beam in GameObject.FindGameObjectsWithTag("Beam"))
                {
                    Destroy(beam);
                }
                _oldVerticalBeam = Convert.ToBoolean(_manager.VerticalBeam);
                _oldVerticalBeam = Convert.ToBoolean(_manager.VerticalBeam);
                ShootBeam();
            }
        }

        if (_manager.ArrowFrequency != _oldBeamFrequency)
        {
            CancelInvoke(nameof(ShootArrow));
            InvokeRepeating(nameof(ShootArrow), _manager.ArrowFrequency, _manager.ArrowFrequency);
        }
    }

    private void ShootBeam()
    {
        Instantiate(_beam, transform.position, Quaternion.identity, transform);

        if (_manager.HorizontalBeam > 0)
        {
            Instantiate(_beam, transform.position, Quaternion.Euler(0, 0, 180), transform);
        }

        if (_manager.VerticalBeam > 0)
        {
            Instantiate(_beam, transform.position, Quaternion.Euler(0, 0, 90), transform);
            Instantiate(_beam, transform.position, Quaternion.Euler(0, 0, 270), transform);
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
            GameObject spawnedArrow = Instantiate(_arrow, transform.position, Quaternion.identity);
            spawnedArrow.GetComponent<Arrow>().Target = target;
        }
    }
}
