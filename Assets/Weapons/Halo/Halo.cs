using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    [SerializeField]
    private int radius = 1;
    [SerializeField]
    private int initialSwordsCount = 5;
    [SerializeField]
    private GameObject _sword;

    private List<GameObject> swords = new();

    private void Start()
    {
        float angle = Mathf.PI*2f / initialSwordsCount;
        for(int i = 0; i < initialSwordsCount; i++)
        {
            GameObject sword = Instantiate(
                _sword,
                transform.position + new Vector3(Mathf.Cos(i * angle), Mathf.Sin(i * angle), 0),
                Quaternion.identity,
                transform
            );
            swords.Add(sword);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 200f * Time.deltaTime, Space.Self);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void AddSword()
    {
        for(int i = 0; i < swords.Count; i++)
        {
            Destroy(swords[i]);
        }
        swords.Clear();

        float angle = Mathf.PI * 2f / initialSwordsCount;
        for (int i = 0; i < initialSwordsCount; i++)
        {
            GameObject sword = Instantiate(
                _sword,
                transform.position + new Vector3(Mathf.Cos(i * angle), Mathf.Sin(i * angle), 0),
                Quaternion.identity,
                transform
            );
            swords.Add(sword);
        }
    }
}
