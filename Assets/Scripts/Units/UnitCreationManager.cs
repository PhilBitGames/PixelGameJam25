using System.Collections.Generic;
using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitCreationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text soldierCountText;
    [SerializeField] private TMP_Text archerCountText;
    [SerializeField] private GameObject undeadSoldierPrefab;
    [SerializeField] private GameObject undeadArcherPrefab;
    [SerializeField] private Image soliderSelectedImage;
    [SerializeField] private Image archerSelectedImage;

    private UnitType currentTypeSelected;
    
    private readonly int archerSoulsCount = 3;
    private readonly int soldierSoulsCount = 3;

    private Dictionary<UnitType, UnitTypeProperties> unitTypeToPrefab = new();

    private void Awake()
    {
        unitTypeToPrefab = new Dictionary<UnitType, UnitTypeProperties>
        {
            { UnitType.Soldier, new UnitTypeProperties { prefab = undeadSoldierPrefab, text = soldierCountText } },
            { UnitType.Archer, new UnitTypeProperties { prefab = undeadArcherPrefab, text = archerCountText } }
        };

        unitTypeToPrefab[UnitType.Soldier].SoulsCount = soldierSoulsCount;
        unitTypeToPrefab[UnitType.Archer].SoulsCount = archerSoulsCount;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            SetCurrentTypeSelected(currentTypeSelected == UnitType.Soldier ? UnitType.Archer : UnitType.Soldier);
    }

    public void SetCurrentTypeSelectedWrapper(int unitTypeIndex)
    {
        SetCurrentTypeSelected((UnitType)unitTypeIndex);
    }


    public void CreateUnit(Vector2 mousePosition)
    {
        var unitTypeProperties = unitTypeToPrefab[currentTypeSelected];

        if (unitTypeProperties.SoulsCount <= 0) return;

        Instantiate(unitTypeProperties.prefab, mousePosition, Quaternion.identity);
        unitTypeProperties.SoulsCount--;
        GetComponent<AudioSource>().Play();
    }

    public void AddSoul(UnitType selectableUnitUnitType)
    {
        unitTypeToPrefab[selectableUnitUnitType].SoulsCount++;
    }

    private void SetCurrentTypeSelected(UnitType unitType)
    {
        currentTypeSelected = unitType;
        switch (currentTypeSelected)
        {
            case UnitType.Soldier:
                soliderSelectedImage.enabled = true;
                archerSelectedImage.enabled = false;
                break;
            case UnitType.Archer:
                archerSelectedImage.enabled = true;
                soliderSelectedImage.enabled = false;
                break;
        }
    }

    private class UnitTypeProperties
    {
        public GameObject prefab;
        private int soulsCount;
        public TMP_Text text;

        public int SoulsCount
        {
            get => soulsCount;
            set
            {
                soulsCount = value;
                if (text != null) text.text = soulsCount.ToString();
            }
        }
    }
}