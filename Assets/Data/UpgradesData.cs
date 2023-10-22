using UnityEngine;

public class UpgradesData : MonoBehaviour
{
    [SerializeField] private TextAsset upgradesJSON;

    public Upgrades upgrades;

    private void Awake()
    {
        upgrades = JsonUtility.FromJson<Upgrades>(upgradesJSON.text);
    }
}

[System.Serializable]
public class Upgrade
{
    public string name;
    public string paramName;
    public float value;
}

[System.Serializable]
public class Upgrades
{
    public Upgrade[] upgrades;
}