using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

/// <summary>
/// This is the view of our cell which handles how the cell looks.
/// </summary>
public class WeightChartCellView : EnhancedScrollerCellView
{
    /// <summary>
    /// A reference to the UI Text element to display the cell data
    /// </summary>
    public Image weightBar;

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(WeightChartCellData data)
    {
        // update the UI text with the cell data
        weightBar.fillAmount = data.weightFloat;
    }
}