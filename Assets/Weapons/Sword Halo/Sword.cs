using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Sword : MonoBehaviour
{
    private PlayerManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        RotateSword();
    }

    private void RotateSword()
    {
        // LookAt 2D
        Vector3 target = transform.parent.position;
        // get the angle
        Vector3 norTar = (target - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        // rotate to angle
        Quaternion rotation = new();
        rotation.eulerAngles = new Vector3(0, 0, angle - 180);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject != null)
        {
            collision.GetComponent<Enemy>().Life -= _manager.SwordsDamages;
            collision.gameObject.GetComponent<Enemy>().AnimateHit();
        }
    }
}
