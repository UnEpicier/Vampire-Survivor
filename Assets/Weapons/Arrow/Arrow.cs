using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject Target;

    private PlayerManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 20f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Life -= _manager.ArrowDamages;
            collision.gameObject.GetComponent<Enemy>().AnimateHit();
            Destroy(gameObject);
        }
        
    }
}
