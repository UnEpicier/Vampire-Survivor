using System.Collections;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class LaserBeam : MonoBehaviour
{
    public int Damages = 50;

    public float lifeSeconds = .2f;

    public void Start()
    {
        StartCoroutine(nameof(EndLifeAfterXSeconds));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            collision.gameObject.GetComponent<Ennemy>().Life -= Damages;
            collision.gameObject.GetComponent<Ennemy>().AnimateHit();
        }
    }

    private IEnumerator EndLifeAfterXSeconds()
    {
        yield return new WaitForSeconds(lifeSeconds);
        Destroy(gameObject);
    }
}
