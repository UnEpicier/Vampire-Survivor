using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class DeathScreen : MonoBehaviour
{
    private CanvasGroup _cg;
    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void ShowScreen()
    {
        StartCoroutine(IShowScreen());
    }

    private IEnumerator IShowScreen()
    {
        for(float i = 0; i < 1; i += 0.01f)
        {
            _cg.alpha += i;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("End");
    }
}
