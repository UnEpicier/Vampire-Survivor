using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] private TMP_Text _heal;
    [SerializeField] private TMP_Text _movementsSpeed;

    [Header("Weapons Stats")]
    [SerializeField] private TMP_Text _beamDamages;
    [SerializeField] private TMP_Text _beamFrequency;
    [SerializeField] private TMP_Text _beamLife;
    [SerializeField] private TMP_Text _horizontalBeam;
    [SerializeField] private TMP_Text _verticalBeam;

    [SerializeField] private TMP_Text _swordDamages;
    [SerializeField] private TMP_Text _swordQuantity;
    [SerializeField] private TMP_Text _swordHaloRadius;

    [SerializeField] private TMP_Text _arrowDamages;
    [SerializeField] private TMP_Text _arrowRange;
    [SerializeField] private TMP_Text _arrowFrequency;

    private PlayerManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        // Update Stats Texts
        _heal.SetText($"Heal: {_manager.HealValue}/s");
        _movementsSpeed.SetText($"Movements Speed: {_manager.Speed}");
        _beamDamages.SetText($"Beam Damages: {_manager.BeamDamages}");
        _beamFrequency.SetText($"Beam Frequency: {_manager.BeamFrequency}s");
        _beamLife.SetText($"Beam Life: {_manager.BeamLife}s");
        _horizontalBeam.SetText($"Horizontal Beam: {(Convert.ToBoolean(_manager.HorizontalBeam) ? "Yes" : "No")}");
        _verticalBeam.SetText($"Vertical Beam: {(Convert.ToBoolean(_manager.VerticalBeam) ? "Yes": "No")}");
        _swordDamages.SetText($"Sword Damages: {_manager.SwordsDamages}");
        _swordQuantity.SetText($"Swords Quantity: {_manager.SwordsQuantity}");
        _swordHaloRadius.SetText($"Sword Halo Radius: {_manager.SwordsHaloRadius * 1.5}m");
        _arrowDamages.SetText($"Arrow Damages: {_manager.ArrowDamages}");
        _arrowRange.SetText($"Arrow Damages: {_manager.ArrowRange}m");
        _arrowFrequency.SetText($"Arrow Frequency: {_manager.ArrowFrequency}s");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
