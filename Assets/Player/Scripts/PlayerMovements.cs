using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Animator)), RequireComponent(typeof(PlayerManager))]
public class PlayerMovements : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator _animator;
    private PlayerManager _pm;

    public bool lookOnRight = true;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _pm = GetComponent<PlayerManager>();
    }

    private void FixedUpdate()
    {
        /** --- Move player along 2D axis ----------------------------------*/
        float moveH = Input.GetAxis("Horizontal") * _pm.Speed * Time.deltaTime;
        float moveV = Input.GetAxis("Vertical") * _pm.Speed * Time.deltaTime;

        transform.Translate(new Vector2(moveH, moveV));

        /** --- Rotate sprite ----------------------------------------------*/
        if (moveH < 0)
        {
            _sr.flipX = true;
            lookOnRight = false;
        } else if (moveH > 0)
        {
            _sr.flipX = false;
            lookOnRight = true;
        }

        /** --- update animator -------------------------------------------*/
        _animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(moveH), Mathf.Abs(moveV)));
    }
}
