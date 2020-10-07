using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;


/// <summary>
/// This is the sub cell of the row cell
/// </summary>
public class DategridRowCellView : MonoBehaviour
{
    public GameObject container;
    public Image dateBackgroundImage;
    public Text cellText;

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(DategridData data)
    {
        // this cell was outside the range of the data, so we disable the container.
        // Note: We could have disable the cell gameobject instead of a child container,
        // but that can cause problems if you are trying to get components (disabled objects are ignored).
        container.SetActive(data != null);

        if (data != null)
        {
            if (data.session == true)
            {
                dateBackgroundImage.color = new Color(0, 255, 0);
                cellText.text = data.date.Day.ToString();
            }
            else
            {
                dateBackgroundImage.color = new Color(10, 10, 10, 100);
                cellText.text = data.date.Day.ToString();
            }

            if (data.future == true)
            {
                dateBackgroundImage.color = new Color(1, 1, 1, 0);            
                cellText.text = "";
            }

            if (data.isMonthCell == true)
            {
                dateBackgroundImage.color = new Color(1, 1, 1, 0);
                if (data.isFirst == "1")
                {
                    cellText.text = GetMonthString(data.date.Month.ToString());
                }
                else
                {
                    cellText.text = "";
                }               
            }                       
        }
    }

    private string GetMonthString(string monthInt)
    {
        string tempStr = "";
        if (monthInt == "1")
        {
            tempStr = "January";
        }
        else if (monthInt == "2")
        {
            tempStr = "February";
        }
        else if (monthInt == "3")
        {
            tempStr = "March";
        }
        else if (monthInt == "4")
        {
            tempStr = "April";
        }
        else if (monthInt == "5")
        {
            tempStr = "May";
        }
        else if (monthInt == "6")
        {
            tempStr = "June";
        }
        else if (monthInt == "7")
        {
            tempStr = "July";
        }
        else if (monthInt == "8")
        {
            tempStr = "August";
        }
        else if (monthInt == "9")
        {
            tempStr = "September";
        }
        else if (monthInt == "10")
        {
            tempStr = "October";
        }
        else if (monthInt == "11")
        {
            tempStr = "November";
        }
        else if (monthInt == "12")
        {
            tempStr = "December";
        }
        return tempStr;
    }

}
