using UnityEngine;

[RequireComponent (typeof(PlayerMovements))]
public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [Header("If all are false: only on 1 side")]
    [SerializeField] private bool laserOnX;
    [SerializeField] private bool laserOnY;

    private PlayerMovements _pm;

    private void Start()
    {
        _pm = GetComponent<PlayerMovements>();
        InvokeRepeating(nameof(ShootBeam), 5f, 5f);
    }

    private void ShootBeam()
    {
        Instantiate(laser, transform.position, _pm.lookOnRight ? Quaternion.identity : Quaternion.Euler(0, 0, 180), transform);

        if (laserOnX)
        {
            Instantiate(laser, transform.position, _pm.lookOnRight ? Quaternion.Euler(0, 0, 180) : Quaternion.identity, transform);
        }

        if (laserOnY)
        {
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 90), transform);
            Instantiate(laser, transform.position, Quaternion.Euler(0, 0, 270), transform);
        }

    }
}
