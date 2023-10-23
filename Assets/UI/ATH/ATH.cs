using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ATH : MonoBehaviour
{
    [Header("Level")]

    [SerializeField] private RawImage _levelBackground;
    [SerializeField] private RawImage _level;
    [SerializeField] private TMP_Text _levelText;

    [Header("Health")]
    [SerializeField] private RawImage _healthBackground;
    [SerializeField] private RawImage _health;
    [SerializeField] private TMP_Text _healthText;

    private PlayerManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        // Level
        _levelText.SetText($"{_manager.Exp} / {_manager.Level * 10}");
        _level.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _manager.Exp * _levelBackground.rectTransform.rect.width / (_manager.Level * 10));

        // Health
        _healthText.SetText($"{_manager.Health} / {_manager.MaxHealth}");
        _health.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _manager.Health * _healthBackground.rectTransform.rect.width / _manager.MaxHealth);
    }
}
