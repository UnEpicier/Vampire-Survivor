using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(BoxCollider2D))
]
public class Ennemy : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Transform _player;

    public float Speed = .1f;
    public int Damages = 1;
    public int Life = 100;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Life <= 0)
        {
            Destroy(gameObject);
            _player.GetComponent<PlayerManager>().MayorKills++;
        }
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);

            if ((transform.position - _player.transform.position).normalized.x > 0)
            {
                _sr.flipX = true;
            }
            else if ((transform.position - _player.transform.position).normalized.x < 0)
            {
                _sr.flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(AttackPlayer), 0, 1f);
            _player.GetComponent<PlayerCollisions>().AnimateHit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke(nameof(AttackPlayer));
        }
    }

    private void AttackPlayer()
    {
        _player.GetComponent<PlayerManager>().TakeDamage(Damages);
    }

    public void AnimateHit()
    {
        StartCoroutine(IAnimateHit());
    }

    private IEnumerator IAnimateHit()
    {
        for(int i = 0; i < 6; i++)
        {
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0f);
            yield return new WaitForSeconds(.1f);
            _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1f);
            yield return new WaitForSeconds(.1f);
        }
    }
}
