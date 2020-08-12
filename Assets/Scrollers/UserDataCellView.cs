using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;

public class UserDataCellView : EnhancedScrollerCellView
{
    public TextMeshProUGUI userDataTimestamp;
    public TextMeshProUGUI userDataWeight;
    public TextMeshProUGUI userDataWaist;


    public void SetData(UserDataScrollerData data)
    {
        userDataTimestamp.text = data.timestamp;
        userDataWeight.text = data.weight.ToString();
        userDataWaist.text = data.waist.ToString();

    }
}
