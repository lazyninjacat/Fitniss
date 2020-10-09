using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using TMPro;


public class WaistChartCellView : EnhancedScrollerCellView
{
    public Image waistBar;
    public TextMeshProUGUI date;
    public TextMeshProUGUI month;
    public TextMeshProUGUI waistText;

    public void SetData(WaistChartCellData data)
    {
        waistBar.fillAmount = data.fillBar;
        date.text = data.date.ToString();
        month.text = MonthString(data.month);

        if (data.waist == 0)
        {
            waistText.text = "";
        }
        else
        {
            waistText.text = data.waist.ToString();
        }

        //Debug.Log("Waist = " + data.waist);
    }

    private string MonthString(int monthInt)
    {
        string monthStr;
        switch (monthInt)
        {
            case 0:
                monthStr = "";
                break;
            case 1:
                monthStr = "January";
                break;
            case 2:
                monthStr = "February";
                break;
            case 3:
                monthStr = "March";
                break;
            case 4:
                monthStr = "April";
                break;
            case 5:
                monthStr = "May";
                break;
            case 6:
                monthStr = "June";
                break;
            case 7:
                monthStr = "July";
                break;
            case 8:
                monthStr = "August";
                break;
            case 9:
                monthStr = "September";
                break;
            case 10:
                monthStr = "October";
                break;
            case 11:
                monthStr = "November";
                break;
            case 12:
                monthStr = "December";
                break;
            default:
                monthStr = "";
                break;
        }
        return monthStr;
    }
}