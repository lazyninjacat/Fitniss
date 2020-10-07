using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using TMPro;


public class WeightChartCellView : EnhancedScrollerCellView
{
    public Image weightBar;
    public TextMeshProUGUI date;
    public TextMeshProUGUI month;
    public TextMeshProUGUI weightText;

    public void SetData(WeightChartCellData data)
    {
        // update the UI text with the cell data
        weightBar.fillAmount = data.fillBar;
        date.text = data.date.ToString();
        month.text = data.month.ToString();
        weightText.text = data.weight.ToString();
        Debug.Log("Weight = " + data.weight);
    }
}