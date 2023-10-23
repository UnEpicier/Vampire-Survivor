using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UpgradesData))]
public class UpgradeScreen : MonoBehaviour
{
    [SerializeField]
    private List<Button> _buttons = new();

    private Upgrade[] _upgrades;
    private PlayerManager _manager;

    private void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        _upgrades = GetComponent<UpgradesData>().upgrades.upgrades;

        GenerateChoice();
    }

    public void GenerateChoice()
    {
        Upgrade[] choosen = new Upgrade[_buttons.Count];

        while (choosen.Length != choosen.Distinct().Count())
        {
            for(int i = 0; i < choosen.Length; i++)
            {
                Upgrade choosenUpgrade = _upgrades[Random.Range(0, _upgrades.Length)];

                bool beamCondition = (choosenUpgrade.paramName == "HorizontalBeam" && _manager.HorizontalBeam >= 1) || (choosenUpgrade.paramName == "VerticalBeam" && _manager.VerticalBeam >= 1);
                bool beamFrequencyCondition = (choosenUpgrade.paramName == "BeamFrequency" && _manager.BeamFrequency <= 0);
                bool arrowFrequency = (choosenUpgrade.paramName == "ArrowFrequency" && _manager.ArrowFrequency <= 0.5f);

                while (beamCondition || beamFrequencyCondition || arrowFrequency)
                {
                    choosenUpgrade = _upgrades[Random.Range(0, _upgrades.Length)];

                    beamCondition = (choosenUpgrade.paramName == "HorizontalBeam" && _manager.HorizontalBeam >= 1) || (choosenUpgrade.paramName == "VerticalBeam" && _manager.VerticalBeam >= 1);
                    beamFrequencyCondition = (choosenUpgrade.paramName == "BeamFrequency" && _manager.BeamFrequency <= 0);
                    arrowFrequency = (choosenUpgrade.paramName == "ArrowFrequency" && _manager.ArrowFrequency <= 0.5f);
                };

                choosen[i] = choosenUpgrade;
            }
        }

        foreach (var button in _buttons.Select((value, i) => new {i, value}))
        {
            button.value.GetComponentInChildren<TMP_Text>().SetText(choosen[button.i].name);
            button.value.GetComponent<Button>().onClick.AddListener(() => { UpgradePlayer(button.value, _upgrades, _manager); });
        }
    }

    public void UpgradePlayer(Button button, Upgrade[] upgrades, PlayerManager manager)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (upgrades[i].name == button.GetComponentInChildren<TMP_Text>().text)
            {
                FieldInfo field = manager.GetType().GetField(upgrades[i].paramName);
                float fieldValue = (float)field.GetValue(manager);

                field.SetValue(manager, fieldValue + upgrades[i].value);

                if (
                    (upgrades[i].maxValue > 0 && fieldValue + upgrades[i].value >= upgrades[i].maxValue) ||
                    (upgrades[i].maxValue < 0 && fieldValue + upgrades[i].value <= upgrades[i].maxValue)
                )
                {
                    List<Upgrade> upgradesList = new(_upgrades);
                    upgradesList.RemoveAt(i);
                    _upgrades = upgradesList.ToArray();
                }

                Time.timeScale = 1f;
                button.transform.parent.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}
