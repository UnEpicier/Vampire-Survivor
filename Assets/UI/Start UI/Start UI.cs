using System.Collections;
using TMPro;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        StartCoroutine(nameof(Settlement));
    }

    private IEnumerator Settlement()
    {
        yield return new WaitForSecondsRealtime(1f);
        _text.SetText("2");
        yield return new WaitForSecondsRealtime(1f);
        _text.SetText("1");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        GameManager.GameStarted = true;
        Destroy(gameObject);
    }
}
