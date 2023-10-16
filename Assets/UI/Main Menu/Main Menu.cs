using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
