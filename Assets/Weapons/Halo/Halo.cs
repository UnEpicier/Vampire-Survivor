using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    [SerializeField]
    private GameObject _sword;

    private PlayerManager _manager;
    private List<GameObject> swords = new();

    private float _oldRadius;
    private int _oldSwordsCount;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        _oldRadius = _manager.SwordsHaloRadius;
        _oldSwordsCount = (int)_manager.SwordsQuantity;

        float angle = Mathf.PI*2f / _manager.SwordsQuantity;
        for(int i = 0; i < _manager.SwordsQuantity; i++)
        {
            GameObject sword = Instantiate(
                _sword,
                transform.position + new Vector3(Mathf.Cos(i * angle) * _manager.SwordsHaloRadius, Mathf.Sin(i * angle) * _manager.SwordsHaloRadius, 0),
                Quaternion.identity,
                transform
            );
            swords.Add(sword);
        }
    }

    private void Update()
    {
        if (_oldRadius != _manager.SwordsHaloRadius)
        {
            _oldRadius = _manager.SwordsHaloRadius;
            ResizeHalo();
        }

        if (_oldSwordsCount != _manager.SwordsQuantity)
        {
            AddSword();
            _oldSwordsCount = (int)_manager.SwordsQuantity;
        }

        transform.Rotate(Vector3.forward, 200f * Time.deltaTime, Space.Self);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _manager ? _manager.SwordsHaloRadius : 1f);
    }

    private void AddSword()
    {
        for (int i = 0; i < _oldSwordsCount; i++)
        {
            Destroy(swords[i]);
        }
        swords.Clear();

        float angle = Mathf.PI * 2f / _manager.SwordsQuantity;
        for (int i = 0; i < _manager.SwordsQuantity; i++)
        {
            GameObject sword = Instantiate(
                _sword,
                transform.position + new Vector3(Mathf.Cos(i * angle) * _manager.SwordsHaloRadius, Mathf.Sin(i * angle) * _manager.SwordsHaloRadius, 0),
                Quaternion.identity,
                transform
            );
            swords.Add(sword);
        }
    }

    private void ResizeHalo()
    {
        for (int i = 0; i < _manager.SwordsQuantity; i++)
        {
            DestroyImmediate(swords[i]);
        }
        swords.Clear();

        float angle = Mathf.PI * 2f / _manager.SwordsQuantity;
        for (int i = 0; i < _manager.SwordsQuantity; i++)
        {
            GameObject sword = Instantiate(
                _sword,
                transform.position + new Vector3(Mathf.Cos(i * angle) * _manager.SwordsHaloRadius, Mathf.Sin(i * angle) * _manager.SwordsHaloRadius, 0),
                Quaternion.identity,
                transform
            );
            swords.Add(sword);
        }
    }
}
