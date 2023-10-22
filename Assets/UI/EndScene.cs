using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text mayorsText, minautorsText, bringerOfDeathText;

    private void Start()
    {
        mayorsText.SetText($"Mayors killed: {GameManager.MayorKills}");
        minautorsText.SetText($"Minautors killed: {GameManager.MinautorKills}");
        bringerOfDeathText.SetText($"Bringers of Death killed: {GameManager.BringerOfDeathKills}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
