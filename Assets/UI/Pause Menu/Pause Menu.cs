using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && transform.parent.gameObject.activeSelf)
        {
            Time.timeScale = 1f;
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        transform.parent.gameObject.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
