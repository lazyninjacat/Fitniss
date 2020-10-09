using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;
using TMPro;

/// <summary>
/// This is the sub cell of the row cell
/// </summary>
public class DategridRowCellView : MonoBehaviour
{
    public GameObject container;
    public Image dateBackgroundImage;
    public TextMeshProUGUI cellText;

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
                switch (data.score)
                {
                    case 0:
                        //Debug.Log("case 0: " + data.date);
                        break;
                    case 1:
                        //Debug.Log("case 1: " + data.date);
                        dateBackgroundImage.color = new Color(0.6f, 1, 0.6f, 1);
                        break;
                    case 2:
                        //Debug.Log("case 2 " + data.date);
                        dateBackgroundImage.color = new Color(0, 1, 0, 1);
                        break;
                    case 3:
                        //Debug.Log("case 3 " + data.date);
                        dateBackgroundImage.color = new Color(0, 0.7f, 0, 1);
                        break;
                    case 4:
                        //Debug.Log("case 4 " + data.date);
                        dateBackgroundImage.color = new Color(0, 0.4f, 0, 1);
                        break;
                    default:
                        //Debug.Log("case 5 " + data.date);
                        dateBackgroundImage.color = new Color(0, 0.4f, 0, 1);
                        break;
                }

                cellText.text = data.date.Day.ToString();
            }
            else
            {
                dateBackgroundImage.color = new Color(0.8f, 0.8f, 0.8f, 0.4f);
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
