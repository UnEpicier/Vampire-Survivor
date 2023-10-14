using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer)),
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(BoxCollider2D))
]
public class Enemy : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Transform _player;

    public float Speed = .1f;
    public int Damages = 1;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
}
