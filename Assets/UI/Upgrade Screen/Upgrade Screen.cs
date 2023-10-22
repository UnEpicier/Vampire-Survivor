using System;
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
    private List<Button> buttons = new();

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
        Upgrade[] choosen = new Upgrade[buttons.Count];

        while (choosen.Length != choosen.Distinct().Count())
        {
            for(int i = 0; i < choosen.Length; i++)
            {
                Upgrade choosenUpgrade = _upgrades[Random.Range(0, _upgrades.Length)];

                bool beamCondition = (choosenUpgrade.paramName == "laserOnX" && _manager.laserOnX >= 1) || (choosenUpgrade.paramName == "laserOnY" && _manager.laserOnY >= 1);
                bool beamFrequencyCondition = (choosenUpgrade.paramName == "BeamFrequency" && _manager.BeamFrequency <= 0);
                bool arrowFrequency = (choosenUpgrade.paramName == "ArrowFrequency" && _manager.ArrowFrequency <= 0.5f);

                while (beamCondition || beamFrequencyCondition || arrowFrequency)
                {
                    choosenUpgrade = _upgrades[Random.Range(0, _upgrades.Length)];

                    beamCondition = (choosenUpgrade.paramName == "laserOnX" && _manager.laserOnX >= 1) || (choosenUpgrade.paramName == "laserOnY" && _manager.laserOnY >= 1);
                    beamFrequencyCondition = (choosenUpgrade.paramName == "BeamFrequency" && _manager.BeamFrequency <= 0);
                    arrowFrequency = (choosenUpgrade.paramName == "ArrowFrequency" && _manager.ArrowFrequency <= 0.5f);
                };

                choosen[i] = choosenUpgrade;
            }
        }

        foreach (var button in buttons.Select((value, i) => new {i, value}))
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

                Time.timeScale = 1f;
                button.transform.parent.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}