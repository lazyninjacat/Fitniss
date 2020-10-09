using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

public class CircuitDetailsCellView : EnhancedScrollerCellView
{
    public TextMeshProUGUI orderIDText;
    public TextMeshProUGUI exerciseNameText;
    public TextMeshProUGUI exerciseAmountText;

    public void SetData(CircuitDetailsData data)
    {
        orderIDText.text = data.orderID.ToString();
        exerciseNameText.text = data.exerciseName;
        exerciseAmountText.text = data.exerciseAmount.ToString();
    }
}
