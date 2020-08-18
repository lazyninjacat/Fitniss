using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

public class WeightCellView : EnhancedScrollerCellView
{
    public SlicedFilledImage fillBar;
    public TextMeshProUGUI dateText;

    public void SetData(UserDataScrollerData data)
    {
        fillBar.fillAmount = data.weight / 240;
        dateText.text = data.timestamp;
    }
}
