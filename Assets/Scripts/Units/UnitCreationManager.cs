using System;
using System.Collections.Generic;
using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitCreationManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text soldierCountText;
    [SerializeField] private TMPro.TMP_Text archerCountText;
    [SerializeField] private GameObject undeadSoldierPrefab;
    [SerializeField] private GameObject undeadArcherPrefab;
    [SerializeField] private Image soliderSelectedImage;
    [SerializeField] private Image archerSelectedImage;

    private UnitType currentTypeSelected;
    private class UnitTypeProperties
    {
        private int soulsCount;
        public TMP_Text text;
        public int SoulsCount
        {
            get
            {
                return soulsCount;
            }
            set
            {
                soulsCount = value;
                if (text != null) text.text = soulsCount.ToString();
            }
        }
        public GameObject prefab;
    }

    private Dictionary<UnitType, UnitTypeProperties> unitTypeToPrefab = new ();

    private int soldierSoulsCount = 3;
    private int archerSoulsCount = 3;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (currentTypeSelected == UnitType.Soldier)
            {
                SetCurrentTypeSelected(UnitType.Archer);
            }
            else
            {
                SetCurrentTypeSelected(UnitType.Soldier);
            }
        }
    }

    public void SetCurrentTypeSelectedWrapper(int unitTypeIndex)
    {
        SetCurrentTypeSelected((UnitType)unitTypeIndex);
    }
    
    public void SetCurrentTypeSelected(UnitType unitType)
    {
        currentTypeSelected = unitType;
        if(currentTypeSelected == UnitType.Soldier)
        {
            soliderSelectedImage.enabled = (true);
            archerSelectedImage.enabled = (false);
        }
        else if(currentTypeSelected == UnitType.Archer)
        {
            archerSelectedImage.enabled = (true);
            soliderSelectedImage.enabled = (false);
        }
    }

    void Awake()
    {
        unitTypeToPrefab = new Dictionary<UnitType, UnitTypeProperties>()
        {
            { UnitType.Soldier, new UnitTypeProperties() { prefab = undeadSoldierPrefab, text = soldierCountText} },
            { UnitType.Archer, new UnitTypeProperties() { prefab = undeadArcherPrefab, text = archerCountText} }
        };
        
        unitTypeToPrefab[UnitType.Soldier].SoulsCount = soldierSoulsCount;
        unitTypeToPrefab[UnitType.Archer].SoulsCount = archerSoulsCount;
        //unitTypeToPrefab[UnitType.Soldier].SoulsCount++;
    }


    public void CreateUnit(Vector2 mousePosition)
    {   
        UnitTypeProperties unitTypeProperties = unitTypeToPrefab[currentTypeSelected];
        if(unitTypeProperties.SoulsCount > 0)
        { 
            Instantiate(unitTypeProperties.prefab, mousePosition, Quaternion.identity);
            unitTypeProperties.SoulsCount--;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.Log("Not enough souls to create a unit.");
        }
    }

    public void AddSoul(UnitType selectableUnitUnitType)
    {
        unitTypeToPrefab[selectableUnitUnitType].SoulsCount++;
    }
}
