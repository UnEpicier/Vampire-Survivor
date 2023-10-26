using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _mayorsText, _minautorsText, _bringerOfDeathText;

    private void Start()
    {
        _mayorsText.SetText($"Mayors killed: {GameManager.MayorKills}");
        _minautorsText.SetText($"Minautors killed: {GameManager.MinautorKills}");
        _bringerOfDeathText.SetText($"Bringers of Death killed: {GameManager.BringerOfDeathKills}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        GameManager.MayorKills = 0;
        GameManager.MinautorKills = 0;
        GameManager.BringerOfDeathKills = 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
