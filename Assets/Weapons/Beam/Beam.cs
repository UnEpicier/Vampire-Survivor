using System.Collections;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Beam : MonoBehaviour
{
    private PlayerManager _manager;
    private PlayerMovements _movements;

    public void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        _movements = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>();
        if (_manager.BeamFrequency > 0)
        {
            StartCoroutine(nameof(EndLifeAfterXSeconds));
        }
    }

    private void Update()
    {
        if (_manager.HorizontalBeam == 0)
        {
            if (!_movements.LookOnRight)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            } else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<Ennemy>().AnimateHit();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<Ennemy>().Life -= (int)(_manager.BeamDamages * Time.deltaTime);
        }
    }

    private IEnumerator EndLifeAfterXSeconds()
    {
        yield return new WaitForSeconds(_manager.BeamLife);
        Destroy(gameObject);
    }
}
