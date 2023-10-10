using UnityEngine;
public enum OrbValues
{
    One = 1,
    Ten = 10,
    Hundred = 100
};

[RequireComponent(typeof(SpriteRenderer))]
public class OrbStats : MonoBehaviour
{
    public OrbValues OrbValue = OrbValues.One;

    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch (OrbValue)
        {
            case OrbValues.One:
                _sr.color = new Color(0.25f, 1f, 0.25f);
                break;
            case OrbValues.Ten:
                _sr.color = new Color(0.23f, 0.73f, 1f);
                break;
            case OrbValues.Hundred:
                _sr.color = new Color(1f, 0.23f, 0.71f);
                break;
        }
    }
}
