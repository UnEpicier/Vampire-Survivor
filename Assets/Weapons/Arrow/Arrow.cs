using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damages = 10;

    public GameObject target;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 20f * Time.deltaTime);
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<Ennemy>().Life -= Damages;
            Destroy(gameObject);
        }
        
    }
}
