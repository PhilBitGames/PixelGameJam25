using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitSelectionPanel : MonoBehaviour
{
    [SerializeField] private Image selectedImage;
    [SerializeField] private TMP_Text unitCountText;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public void SetSelected(bool isSelected)
    {
        selectedImage.enabled = isSelected;
    }

    public void UpdateUnitCount(int count)
    {
        unitCountText.text = count.ToString();
    }

    public void Initialize(Sprite unitDefinitionIcon, UnitDefinition unitDefinition, UnityAction onClickAction)
    {
        icon.sprite = unitDefinitionIcon;
        button.onClick.AddListener(onClickAction);
    }
}