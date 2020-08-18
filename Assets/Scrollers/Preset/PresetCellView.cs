using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

public class PresetCellView : EnhancedScrollerCellView 
{
    public TextMeshProUGUI presetNameText;

    public void SetData(PresetScrollerData data)
    {
        presetNameText.text = data.presetName;
    }
}
