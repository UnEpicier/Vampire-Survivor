using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerCollisions : MonoBehaviour
{
    private PlayerManager _pm;

    private void Start()
    {
        _pm = GetComponent<PlayerManager>();
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
}
