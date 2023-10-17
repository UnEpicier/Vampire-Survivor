using System.Collections;
using UnityEngine;

[
    RequireComponent(typeof(PlayerManager)),
    RequireComponent(typeof(SpriteRenderer))
]
public class PlayerCollisions : MonoBehaviour
{
    private PlayerManager _pm;
    private SpriteRenderer _sr;

    private void Start()
    {
        _pm = GetComponent<PlayerManager>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /** --- Orb Absorber ------------------------------- */
        if (collision.gameObject.CompareTag("Orb"))
        {
            GameObject orb = collision.gameObject;
            orb.transform.position = Vector3.MoveTowards(orb.transform.position, transform.position, 1f * Time.deltaTime);

            if (orb.transform.position == transform.position)
            {
                Destroy(orb);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            GameObject orb = collision.gameObject;

            if (orb.transform.position == transform.position)
            {
                _pm.AddExperience((int)orb.GetComponent<OrbStats>().OrbValue);
            }
        }
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
