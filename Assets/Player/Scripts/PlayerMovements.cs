using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Animator))]
public class PlayerMovements : MonoBehaviour
{
    private readonly float _speed = .15f;

    private SpriteRenderer _sr;
    private Animator _animator;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        /** --- Move player along 2D axis ----------------------------------*/
        float moveH = Input.GetAxis("Horizontal") * _speed;
        float moveV = Input.GetAxis("Vertical") * _speed;

        transform.Translate(new Vector2(moveH, moveV));

        /** --- Rotate sprite ----------------------------------------------*/
        if (moveH < 0)
        {
            _sr.flipX = true;
        } else if (moveH > 0)
        {
            _sr.flipX = false;
        }

        /** --- update animator -------------------------------------------*/
        _animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(moveH), Mathf.Abs(moveV)));
    }
}
